using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	internal class ScoreManagerWrapper : WrapperBase {
		public readonly ScoreManager scoreManager;

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(scoreManager));
		private static FieldInfo starProgressField;
		private const string starProgressFieldName = "\u0312\u0318\u0310\u031A\u031C\u031C\u031C\u031B\u030D\u0313\u031B";

		public int UnknownInt1 => (int)unknownInt1Field.GetValue(scoreManager); //? Initially 1?
		private static FieldInfo unknownInt1Field;
		private const string unknownInt1FieldName = "\u030F\u0317\u031C\u031A\u031A\u031A\u0311\u0317\u030D\u0314\u031C";

		public int OverallCombo => (int)overallComboField.GetValue(scoreManager);
		private static FieldInfo overallComboField;
		private const string overallComboFieldName = "\u0314\u0316\u0314\u0316\u0316\u0319\u0319\u0319\u0315\u0318\u031A";

		public int UnknownInt3 => (int)unknownInt3Field.GetValue(scoreManager);
		private static FieldInfo unknownInt3Field;
		private const string unknownInt3FieldName = "\u031C\u031A\u031C\u0314\u030E\u031B\u030E\u030E\u0314\u031A\u031C";

		public ScoreManagerWrapper(ScoreManager scoreManager) {
			this.scoreManager = scoreManager;
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref starProgressField, typeof(ScoreManager), starProgressFieldName);
			RegisterField(ref unknownInt1Field, typeof(ScoreManager), unknownInt1FieldName);
			RegisterField(ref overallComboField, typeof(ScoreManager), overallComboFieldName);
			RegisterField(ref unknownInt3Field, typeof(ScoreManager), unknownInt3FieldName);

		}
	}
}
