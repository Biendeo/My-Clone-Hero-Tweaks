using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	//TODO: This inherits ChartObject so when that's done, make sure this inherits that.
	internal class StarPowerWrapper : WrapperBase {
		public readonly object starPower;
		public static Type StarPowerType;

		public uint Length => (uint)lengthField.GetValue(starPower);
		private static FieldInfo lengthField;
		private const string lengthFieldName = "\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A";

		public StarPowerWrapper(object starPower) {
			this.starPower = starPower;
		}

		public static void InitializeSingletonFields() {
			StarPowerType = Assembly.Load("Assembly-CSharp.dll").GetType("\u0311\u0311\u0314\u0317\u0319\u0316\u0312\u030F\u0311\u0315\u0312");
			RegisterField(ref lengthField, StarPowerType, lengthFieldName);
		}
	}
}
