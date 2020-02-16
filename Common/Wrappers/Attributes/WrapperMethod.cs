using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers.Attributes {
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
	}
}
