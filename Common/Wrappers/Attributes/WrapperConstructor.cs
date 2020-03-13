using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers.Attributes {
	public sealed class WrapperConstructor : Attribute {
		public readonly Type[] Types;

		public WrapperConstructor(Type[] types) {
			Types = types;
		}

		public WrapperConstructor() {
			Types = Array.Empty<Type>();
		}
	}
}
