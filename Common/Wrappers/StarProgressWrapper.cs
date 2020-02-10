using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	internal class StarProgressWrapper : WrapperBase {
		public readonly StarProgress starProgress;

		public int LastScore => (int)lastScoreField.GetValue(starProgress);
		private static FieldInfo lastScoreField;
		private const string lastScoreFieldName = "\u0310\u0318\u0311\u030F\u030F\u030F\u0315\u0313\u0316\u030E\u0317";

		public int BaseScore => (int)baseScoreField.GetValue(starProgress);
		private static FieldInfo baseScoreField;
		private const string baseScoreFieldName = "\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313";

		// This appears to be the score required for the previous star acquired.
		public int ScoreToRemove => (int)scoreToRemoveField.GetValue(starProgress);
		private static FieldInfo scoreToRemoveField;
		private const string scoreToRemoveFieldName = "\u0315\u0312\u0311\u030D\u0318\u030F\u0313\u030F\u0310\u0316\u0313";

		// This appears to be 0 when you have zero stars, then the difference between the score from the last star to
		// the next star.
		public int ScoreToCompare => (int)scoreToCompareField.GetValue(starProgress);
		private static FieldInfo scoreToCompareField;
		private const string scoreToCompareFieldName = "\u0319\u0311\u0313\u031A\u030F\u0319\u031A\u030D\u030E\u0316\u031B";

		// Always 7, maybe I could...tweak this?
		public int MaxDisplayStar => (int)maxDisplayStarField.GetValue(starProgress);
		private static FieldInfo maxDisplayStarField;
		private const string maxDisplayStarFieldName = "\u0319\u0318\u0316\u0316\u030E\u0316\u031A\u0318\u030E\u031C\u030F";

		public int CurrentStar => (int)currentStarField.GetValue(starProgress);
		private static FieldInfo currentStarField;
		private const string currentStarFieldName = "\u031C\u030E\u0316\u0312\u0317\u030E\u0310\u031C\u031B\u0310\u031A";

		public int[] StarScores => (int[])starScoresField.GetValue(starProgress);
		private static FieldInfo starScoresField;
		private const string starScoresFieldName = "\u031A\u0312\u0318\u0314\u0318\u0318\u031C\u0317\u0311\u031C\u031A";

		public StarProgressWrapper(StarProgress starProgress) {
			this.starProgress = starProgress;
			// TODO: Test what happens with this! It could be interesting!
			//maxDisplayStarField.SetValue(starProgress, 9);
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref lastScoreField, typeof(StarProgress), lastScoreFieldName);
			RegisterField(ref baseScoreField, typeof(StarProgress), baseScoreFieldName);
			RegisterField(ref scoreToRemoveField, typeof(StarProgress), scoreToRemoveFieldName);
			RegisterField(ref scoreToCompareField, typeof(StarProgress), scoreToCompareFieldName);
			RegisterField(ref maxDisplayStarField, typeof(StarProgress), maxDisplayStarFieldName);
			RegisterField(ref currentStarField, typeof(StarProgress), currentStarFieldName);
			RegisterField(ref starScoresField, typeof(StarProgress), starScoresFieldName);
		}
	}
}
