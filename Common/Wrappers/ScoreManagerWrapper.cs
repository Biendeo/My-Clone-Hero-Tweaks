using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(ScoreManager))]
	internal class ScoreManagerWrapper {
		public readonly ScoreManager scoreManager;

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(scoreManager));
		[WrapperField("\u0312\u0318\u0310\u031A\u031C\u031C\u031C\u031B\u030D\u0313\u031B")]
		private static readonly FieldInfo starProgressField;

		public int UnknownInt1 => (int)unknownInt1Field.GetValue(scoreManager); //? Initially 1?
		[WrapperField("\u030F\u0317\u031C\u031A\u031A\u031A\u0311\u0317\u030D\u0314\u031C")]
		private static readonly FieldInfo unknownInt1Field;

		public int OverallCombo => (int)overallComboField.GetValue(scoreManager);
		[WrapperField("\u0314\u0316\u0314\u0316\u0316\u0319\u0319\u0319\u0315\u0318\u031A")]
		private static readonly FieldInfo overallComboField;

		public int UnknownInt3 => (int)unknownInt3Field.GetValue(scoreManager);
		[WrapperField("\u031C\u031A\u031C\u0314\u030E\u031B\u030E\u030E\u0314\u031A\u031C")]
		private static readonly FieldInfo unknownInt3Field;

		public ScoreManagerWrapper(ScoreManager scoreManager) {
			this.scoreManager = scoreManager;
		}
	}
}
