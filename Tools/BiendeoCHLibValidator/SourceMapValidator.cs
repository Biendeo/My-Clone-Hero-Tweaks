using BepInEx.Logging;
using BiendeoCHLib.Wrappers;
using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiendeoCHLibValidator {
	/// <summary>
	/// Determines whether the names in a sourcemap file match a wrapper. If there's any extra fields in either, this
	/// would note it.
	/// </summary>
	class SourceMapValidator : IValidator {
		private ManualLogSource logger;
		private bool valid;


		public SourceMapValidator(ManualLogSource logger) {
			this.logger = logger;
			valid = true;
		}

		public bool AssertWorkingDirectory() {
			string[] filesToLookFor = new[] {
				"Assembly-CSharp.xml"
			};
			foreach (string file in filesToLookFor) {
				if (!new FileInfo(Path.Combine(Environment.CurrentDirectory, file)).Exists) {
					logger.LogError($"{file} cannot be found from current working directory!");
					return false;
				}
			}
			return true;
		}

		public bool Validate() {
			var wrappers = GetWrappers();
			var mappings = GetSourceMap();

			var foundMappings = new HashSet<string>();

			foreach (var wrapperType in wrappers.Values) {
				var wrapperAttribute = wrapperType.GetCustomAttribute<Wrapper>();
				if (wrapperAttribute.WrappedType.Name.HasObfuscation() && !mappings.ContainsKey(wrapperAttribute.WrappedType.Name)) {
					logger.LogError($"Type {wrapperType.Name}'s wrapped type {wrapperAttribute.WrappedType.Name.DecodeUnicode()} does not have an entry in the sourcemap!");
					valid = false;
				} else {
					foundMappings.Add(wrapperAttribute.WrappedType.Name);
				}
				foreach (var field in wrapperType.GetFields(BindingFlags.NonPublic | BindingFlags.Static)) {
					if (!field.Name.ToLower().Contains("duplicate")) { // Just for testing, duplicates should be removed from the wrappers.
						if (GetWrapperFieldName(field, out string name, out string wrappedCategory)) {
							if (name.HasObfuscation()) {
								if (!mappings.ContainsKey(name)) {
									logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have an entry in the sourcemap!");
									valid = false;
								} else {
									foundMappings.Add(name);
									if (mappings[name].ToLower() != field.Name.Substring(0, field.Name.Length - wrappedCategory.Length).ToLower()) {
										logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not match the sourcemap's name {mappings[name]}!");
										valid = false;
									}
									if (wrappedCategory == "field" || wrappedCategory == "property") {
										var matchingProperty = wrapperType.GetProperty(mappings[name]);
										if (matchingProperty == null) {
											logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have a correctly named property {mappings[name]}!");
											valid = false;
										}
									} else if (wrappedCategory == "method") {
										if (!wrapperType.GetMethods().Where(m => m.Name == mappings[name]).Any()) {
											logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have a correctly named method {mappings[name]}!");
											valid = false;
										}
									}
								}
							}
						}
					}
				}
			}

			foreach (var mapping in mappings) {
				if (!foundMappings.Contains(mapping.Key)) {
					logger.LogError($"No wrapper for mapping {mapping.Key.DecodeUnicode()} to {mapping.Value}!");
					valid = false;
				}
			}

			return valid;
		}

		private Dictionary<string, Type> GetWrappers() {
			var wrappers = new Dictionary<string, Type>();
			foreach (var t in typeof(GameManagerWrapper).Assembly.GetTypes()) {
				var wrapper = t.GetCustomAttribute<Wrapper>();
				if (wrapper != null) {
					wrappers.Add(wrapper.WrappedType.Name, t);
				}
			}

			return wrappers;
		}

		private Dictionary<string, string> GetSourceMap() {
			var mappings = new Dictionary<string, string>();
			var regex = new Regex("original=\"(.*)\" mapped=\"(.*)\"");
			foreach (string mapping in File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Assembly-CSharp.xml"))) {
				var match = regex.Match(mapping);
				if (match.Groups.Count == 3) {
					if (!mappings.ContainsKey(match.Groups[1].Value.Split(':')[match.Groups[1].Value.Split(':').Length - 1].Split('(')[0])) {
						mappings.Add(match.Groups[1].Value.Split(':')[match.Groups[1].Value.Split(':').Length - 1].Split('(')[0], match.Groups[2].Value);
					}
				}
			}
			return mappings;
		}

		private void ValidateWrappersContainMappings(Dictionary<string, Type> wrappers, Dictionary<string, string> mappings) {
			foreach (var wrapperType in wrappers.Values) {
				var wrapperAttribute = wrapperType.GetCustomAttribute<Wrapper>();
				if (wrapperAttribute.WrappedType.Name.HasObfuscation() && !mappings.ContainsKey(wrapperAttribute.WrappedType.Name)) {
					logger.LogError($"Type {wrapperType.Name}'s wrapped type {wrapperAttribute.WrappedType.Name.DecodeUnicode()} does not have an entry in the sourcemap!");
				}
				foreach (var field in wrapperType.GetFields(BindingFlags.NonPublic | BindingFlags.Static)) {
					if (!field.Name.Contains("Duplicate")) { // Just for testing, duplicates should be removed from the wrappers.
						if (GetWrapperFieldName(field, out string name, out string wrappedCategory)) {
							if (name.HasObfuscation()) {
								if (!mappings.ContainsKey(name)) {
									logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have an entry in the sourcemap!");
								} else {
									if (mappings[name].ToLower() != field.Name.Substring(0, field.Name.Length - wrappedCategory.Length).ToLower()) {
										logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not match the sourcemap's name {mappings[name]}!");
									}
									if (wrappedCategory == "field" || wrappedCategory == "property") {
										var matchingProperty = wrapperType.GetProperty(mappings[name]);
										if (matchingProperty == null) {
											logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have a correctly named property {mappings[name]}!");
										}
									} else if (wrappedCategory == "method") {
										if (!wrapperType.GetMethods().Where(m => m.Name == mappings[name]).Any()) {
											logger.LogError($"Type {wrapperType.Name}'s field {field.Name} does not have a correctly named method {mappings[name]}!");
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void ValidateMappingsContainWrappers(Dictionary<string, Type> wrappers, Dictionary<string, string> mappings) {
			foreach (var mapping in mappings) { 
				
			}
		}

		private bool GetWrapperFieldName(FieldInfo wrapperField, out string name, out string wrappedCategory) {
			var fieldAttribute = wrapperField.GetCustomAttribute<WrapperField>();
			if (fieldAttribute != null) {
				name = fieldAttribute.ObfuscatedName;
				wrappedCategory = "field";
				return true;
			}
			var propertyAttribute = wrapperField.GetCustomAttribute<WrapperProperty>();
			if (propertyAttribute != null) {
				name = propertyAttribute.ObfuscatedName;
				wrappedCategory = "property";
				return true;
			}
			var methodAttribute = wrapperField.GetCustomAttribute<WrapperMethod>();
			if (methodAttribute != null) {
				name = methodAttribute.ObfuscatedName;
				wrappedCategory = "method";
				return true;
			}
			var enumAttribute = wrapperField.GetCustomAttribute<WrapperEnum>();
			if (enumAttribute != null) {
				name = enumAttribute.ObfuscatedName;
				wrappedCategory = "enum";
				return true;
			}

			name = string.Empty;
			wrappedCategory = string.Empty;
			return false;
		}
	}
}

/*
 * What's the goal?
 * 
 */