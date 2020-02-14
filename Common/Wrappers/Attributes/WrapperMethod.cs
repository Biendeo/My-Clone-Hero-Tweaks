using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers.Attributes {
	public sealed class WrapperMethod : Attribute {
		public readonly string ObfuscatedName;

		public WrapperMethod(string obfuscatedName) {
			ObfuscatedName = obfuscatedName;
		}
	}
}
