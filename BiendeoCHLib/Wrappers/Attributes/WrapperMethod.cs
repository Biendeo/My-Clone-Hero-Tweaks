using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BiendeoCHLib.Wrappers.Attributes {
	public sealed class WrapperMethod : Attribute {
		public readonly string ObfuscatedName;
		public readonly Type[] Types;

		public WrapperMethod(string obfuscatedName) {
			ObfuscatedName = obfuscatedName;
			Types = Array.Empty<Type>();
		}

		public WrapperMethod(string obfuscatedName, Type[] types) {
			ObfuscatedName = obfuscatedName;
			Types = types;
		}

		public MethodInfo GetMethodInfo(Type type) {
			if (Types.Length == 0) {
				return type.GetMethod(ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			} else {
				return type.GetMethod(ObfuscatedName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Types, Array.Empty<ParameterModifier>());
			}
		}
	}
}
