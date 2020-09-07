using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(ScoreManager))]
	public struct ScoreManagerWrapper {
		public ScoreManager ScoreManager { get; private set; }

		public static ScoreManagerWrapper Wrap(ScoreManager scoreManager) => new ScoreManagerWrapper {
			ScoreManager = scoreManager
		};

		public override bool Equals(object obj) => ScoreManager.Equals(obj);

		public override int GetHashCode() => ScoreManager.GetHashCode();

		public bool IsNull() => ScoreManager == null;

		#region Fields

		public StarProgressWrapper StarProgress {
			get => StarProgressWrapper.Wrap(starProgressField(ScoreManager));
			set => starProgressField(ScoreManager) = value.StarProgress;
		}
		[WrapperField("\u0312\u0318\u0310\u031A\u031C\u031C\u031C\u031B\u030D\u0313\u031B")]
		private static readonly AccessTools.FieldRef<ScoreManager, StarProgress> starProgressField;

		public int UnknownInt1 {
			get => unknownInt1Field(ScoreManager);
			set => unknownInt1Field(ScoreManager) = value;
		} //? Initially 1?
		[WrapperField("\u030F\u0317\u031C\u031A\u031A\u031A\u0311\u0317\u030D\u0314\u031C")]
		private static readonly AccessTools.FieldRef<ScoreManager, int> unknownInt1Field;

		public int OverallCombo {
			get => overallComboField(ScoreManager);
			set => overallComboField(ScoreManager) = value;
		}
		[WrapperField("\u0314\u0316\u0314\u0316\u0316\u0319\u0319\u0319\u0315\u0318\u031A")]
		private static readonly AccessTools.FieldRef<ScoreManager, int> overallComboField;

		public int UnknownInt3 {
			get => unknownInt3Field(ScoreManager);
			set => unknownInt3Field(ScoreManager) = value;
		}
		[WrapperField("\u031C\u031A\u031C\u0314\u030E\u031B\u030E\u030E\u0314\u031A\u031C")]
		private static readonly AccessTools.FieldRef<ScoreManager, int> unknownInt3Field;

		#endregion
	}
}
