using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	//TODO: This inherits ChartObject so when that's done, make sure this inherits that.
	[Wrapper("\u0311\u0311\u0314\u0317\u0319\u0316\u0312\u030F\u0311\u0315\u0312")]
	public struct StarPowerWrapper {
		public object StarPower { get; private set; }

		public static StarPowerWrapper Wrap(object starPower) => new StarPowerWrapper {
			StarPower = starPower
		};

		public override bool Equals(object obj) => StarPower.Equals(obj);

		public override int GetHashCode() => StarPower.GetHashCode();

		public bool IsNull() => StarPower == null;

		#region Fields

		public uint Length {
			get => lengthField(StarPower);
			set => lengthField(StarPower) = value;
		}
		[WrapperField("\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A")]
		private static readonly AccessTools.FieldRef<object, uint> lengthField;

		#endregion
	}
}
