using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers.Attributes {
	public sealed class WrapperProperty : Attribute {
		public readonly string ObfuscatedName;
		public readonly BindingFlags BindingFlags;

		public WrapperProperty(string obfuscatedName) {
			ObfuscatedName = obfuscatedName;
		}
	}
}
