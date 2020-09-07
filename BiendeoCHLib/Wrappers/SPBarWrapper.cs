using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(SPBar))]
	public struct SPBarWrapper {
		public SPBar SPBar { get; private set; }

		public static SPBarWrapper Wrap(SPBar spBar) => new SPBarWrapper {
			SPBar = spBar
		};

		public override bool Equals(object obj) => SPBar.Equals(obj);

		public override int GetHashCode() => SPBar.GetHashCode();

		public bool IsNull() => SPBar == null;

		#region Fields

		public float SomeFloat {
			get => someFloatField(SPBar);
			set => someFloatField(SPBar) = value;
		}
		[WrapperField("\u0319\u031C\u031A\u030E\u0314\u030E\u0312\u0311\u0317\u0318\u0316")]
		private static readonly AccessTools.FieldRef<SPBar, float> someFloatField;

		#endregion
	}
}
