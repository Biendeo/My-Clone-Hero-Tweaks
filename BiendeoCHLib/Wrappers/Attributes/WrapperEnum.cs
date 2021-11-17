using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers.Attributes {
	public class WrapperEnum : Attribute {
		public readonly string ObfuscatedName;

		public WrapperEnum(string obfuscatedName) {
			ObfuscatedName = obfuscatedName;
		}
	}
}
