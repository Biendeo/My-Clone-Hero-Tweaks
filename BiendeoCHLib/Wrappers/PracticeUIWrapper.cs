using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(PracticeUI))]
	public struct PracticeUIWrapper {
		public PracticeUI PracticeUI { get; private set; }

		public static PracticeUIWrapper Wrap(PracticeUI practiceUI) => new PracticeUIWrapper {
			PracticeUI = practiceUI
		};

		public override bool Equals(object obj) => PracticeUI.Equals(obj);

		public override int GetHashCode() => PracticeUI.GetHashCode();

		public bool IsNull() => PracticeUI == null;

		#region Fields

		public float SomeFloat {
			get => someFloatField(PracticeUI);
			set => someFloatField(PracticeUI) = value;
		}
		[WrapperField("\u030E\u0316\u030F\u0314\u030E\u0312\u0315\u0317\u0315\u031B\u0316")]
		private static readonly AccessTools.FieldRef<PracticeUI, float> someFloatField;

		#endregion
	}
}
