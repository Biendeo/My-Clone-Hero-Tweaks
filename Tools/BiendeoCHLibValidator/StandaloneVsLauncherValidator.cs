using BepInEx.Logging;
using BiendeoCHLib.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiendeoCHLibValidator {
	/// <summary>
	/// Notes how many more methods are in the standalone version compared to the launcher version (which skips the
	/// method duplication). This may be a bit moot by the time v24 comes out.
	/// Writes a file to the working directory called MissingMethodsCount.json that writes the methods in the standalone
	/// version that are not in the launcher version (i.e. they're duplicated methods).
	/// </summary>
	class StandaloneVsLauncherValidator : IValidator {
		private ManualLogSource logger;

		public StandaloneVsLauncherValidator(ManualLogSource logger) {
			this.logger = logger;
		}

		public bool AssertWorkingDirectory() {
			string[] filesToLookFor = new[] {
				Path.Combine("CH-Libraries", "Assembly-CSharp.dll"),
				Path.Combine("CH-Libraries", "Assembly-CSharp-firstpass.dll"),
				Path.Combine("CH-Launcher-Libraries", "Assembly-CSharp.dll"),
				Path.Combine("CH-Launcher-Libraries", "Assembly-CSharp-firstpass.dll")
			};
			foreach (string file in filesToLookFor) {
				if (!new FileInfo(Path.Combine(Environment.CurrentDirectory, file)).Exists) {
					logger.LogError($"{file} cannot be found from current working directory!");
					return false;
				}
			}
			return true;
		}
		private struct MissingMethodCount {
			public int Count;
			public List<string> Names;
		}

		public bool Validate() {
			var commonTypes = GetCommonTypes();


			var missingMethodCounts = new Dictionary<string, MissingMethodCount>();
			foreach ((var launcherType, var standaloneType) in commonTypes) {
				foreach (var i in new[] {
					(launcherType, standaloneType, "launcher", "standalone"),
					(standaloneType, launcherType, "standalone", "launcher")
				}) {
					CompareFields(i.Item1, i.Item2, i.Item3, i.Item4);
					CompareProperties(i.Item1, i.Item2, i.Item3, i.Item4);
					CompareMethods(i.Item1, i.Item2, i.Item3, i.Item4, out int missingMethodCount, out List<string> missingMethodNames);
					if (i.Item3 == "standalone") {
						missingMethodCounts.Add(i.Item1.FullName.DecodeUnicode(), new MissingMethodCount { Count = missingMethodCount, Names = missingMethodNames });
					}
					CompareConstructors(i.Item1, i.Item2, i.Item3, i.Item4);
				}
			}

			File.WriteAllText("MissingMethodCounts.json", JsonConvert.SerializeObject(missingMethodCounts.OrderBy(o => o.Key), Formatting.Indented));

			return true;
		}

		private List<(Type LauncherType, Type StandaloneType)> GetCommonTypes() {
			// There's a few types that only appear in each file:
			// PrivateImplementationDetailsKQPLETG.__BB_OBFUSCATOR_VERSION_2_7_2 (launcher-only)
			// \u031B\u0313\u0312\u0318\u0316\u031C\u0317\u0318\u0313\u0319\u0312 (standalone-only)
			// <PrivateImplementationDetails>{01869bb3-9c8b-4125-b144-c990aa96a297}.ArrayCopy148 (standalone-only)
			// <PrivateImplementationDetails>{01869bb3-9c8b-4125-b144-c990aa96a297}.ArrayCopy148+$ArrayType$151 (standalone-only)
			// <PrivateImplementationDetails>{799FF37D-2237-4D15-9F9D-54D09883141F}.__BB_OBFUSCATOR_VERSION_2_4_2 (standalone-only)

			var standaloneAssembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "CH-Libraries", "Assembly-CSharp.dll"));
			var launcherAssembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "CH-Launcher-Libraries", "Assembly-CSharp.dll"));
			foreach ((Assembly assembly, string directory) in new[] { (standaloneAssembly, "CH-Libraries"), (launcherAssembly, "CH-Launcher-Libraries") }) {
				assembly.GetReferencedAssemblies().Single(a => a.Name == "Assembly-CSharp-firstpass").CodeBase = Path.Combine(Environment.CurrentDirectory, directory, "Assembly-CSharp-firstpass.dll");
			}

			var standaloneTypes = standaloneAssembly.GetTypes().ToList();
			var launcherTypes = launcherAssembly.GetTypes().ToList();
			var commonTypes = new List<(Type LauncherType, Type StandaloneType)>();
			Action<List<Type>, List<Type>, string> compareMethod = (iterateTypes, compareTypes, compareName) => {
				foreach (var type in iterateTypes) {
					if (!compareTypes.Exists(ct => ct.FullName == type.FullName)) {
						logger.LogDebug($"Type {type.FullName.DecodeUnicode()} not in {compareName}!");
					} else if (iterateTypes == launcherTypes) {
						commonTypes.Add((type, standaloneTypes.Single(st => st.FullName == type.FullName)));
					}
				}
			};

			compareMethod(launcherTypes, standaloneTypes, "standalone");
			compareMethod(standaloneTypes, launcherTypes, "launcher");

			return commonTypes;
		}

		private void CompareFields(Type iterateType, Type compareType, string iterateSourceName, string compareSourceName) {
			// It appears every field in the common types exist in both files.

			var iterateFields = iterateType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();
			var compareFields = compareType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();

			int missings = 0;
			int differentTypes = 0;

			foreach (var field in iterateFields) {
				if (!compareFields.Exists(cf => cf.Name == field.Name)) {
					logger.LogDebug($"Field {iterateType.Name.DecodeUnicode()}.{field.Name.DecodeUnicode()} not in {compareSourceName}!");
					++missings;
				} else {
					var matchedField = compareFields.Single(cf => cf.Name == field.Name);
					if (matchedField.FieldType.Name != field.FieldType.Name) {
						logger.LogDebug($"Field {iterateType.Name.DecodeUnicode()}.{field.Name.DecodeUnicode()} different type in {compareSourceName}!");
						++differentTypes;
					}
				}
			}

			if (missings > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {missings} missing field{(missings == 1 ? string.Empty : "s")} in {compareSourceName}");
			}
			if (differentTypes > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {differentTypes} field{(differentTypes == 1 ? string.Empty : "s")} with different types in {compareSourceName}");
			}
		}

		private void CompareProperties(Type iterateType, Type compareType, string iterateSourceName, string compareSourceName) {
			// It appears every property in the common types exist in both files.

			var iterateProperties = iterateType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();
			var compareProperties = compareType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();

			int missings = 0;
			int differentTypes = 0;

			foreach (var property in iterateProperties) {
				if (!compareProperties.Exists(cp => cp.Name == property.Name)) {
					logger.LogDebug($"Property {iterateType.Name.DecodeUnicode()}.{property.Name.DecodeUnicode()} not in {compareSourceName}!");
					++missings;
				} else {
					var matchedProperty = compareProperties.Single(cp => cp.Name == property.Name);
					if (matchedProperty.PropertyType.Name != property.PropertyType.Name) {
						logger.LogDebug($"Property {iterateType.Name.DecodeUnicode()}.{property.Name.DecodeUnicode()} different type in {compareSourceName}!");
						++differentTypes;
					}
				}
			}

			if (missings > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {missings} missing propert{(missings == 1 ? "y" : "ies")} in {compareSourceName}");
			}
			if (differentTypes > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {differentTypes} propert{(differentTypes == 1 ? "y" : "ies")} with different types in {compareSourceName}");
			}
		}

		private void CompareMethods(Type iterateType, Type compareType, string iterateSourceName, string compareSourceName, out int missings, out List<string> missingMethodNames) {
			// There are many methods in the standalone version not in the launcher version. The launcher does not
			// contain any extra methods to the standalone version though.

			var iterateMethods = iterateType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();
			var compareMethods = compareType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();

			missings = 0;
			missingMethodNames = new List<string>();

			foreach (var method in iterateMethods) {
				// Because of some diceyness with dealing with referenced assemblies, this just checks that there's at
				// least a method of the name in the other assembly without checking types.
				if (!compareMethods.Exists(cm => cm.Name == method.Name)) {
					// This writes way too much stuff.
					//logger.LogError($"Method {iterateType.Name.DecodeUnicode()}.{method.Name.DecodeUnicode()} not in {compareSourceName}!");
					++missings;
					missingMethodNames.Add(method.Name.DecodeUnicode());
				} else {
					var matchedMethod = compareMethods.First(cm => cm.Name == method.Name);
					compareMethods.Remove(matchedMethod);
				}
			}

			if (missings > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {missings} missing method{(missings == 1 ? string.Empty : "s")} in {compareSourceName}");
			}
		}

		private void CompareConstructors(Type iterateType, Type compareType, string iterateSourceName, string compareSourceName) {
			// It appears every constructor in the common types exist in both files.

			var iterateConstructors = iterateType.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();
			var compareConstructors = compareType.GetConstructors(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList();

			int missings = 0;

			foreach (var constructor in iterateConstructors) {
				// Because of some diceyness with dealing with referenced assemblies, this just checks that there's at
				// least a method of the name in the other assembly without checking types. This probably seems moot
				// with constructors.
				if (!compareConstructors.Exists(cc => cc.Name == constructor.Name)) {
					logger.LogDebug($"Constructor {iterateType.Name.DecodeUnicode()}.{constructor.Name.DecodeUnicode()} not in {compareSourceName}!");
					++missings;
				} else {
					var matchedConstructor = compareConstructors.First(cc => cc.Name == constructor.Name);
					compareConstructors.Remove(matchedConstructor);
				}
			}

			if (missings > 0) {
				logger.LogInfo($"Type {iterateType.Name.DecodeUnicode()} has {missings} missing constructor{(missings == 1 ? string.Empty : "s")} in {compareSourceName}");
			}
		}
	}
}
