using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class StarProgressWrapper : WrapperBase {
		public readonly StarProgress starProgress;

		public int LastScore => (int)lastScoreField.GetValue(starProgress);
		private static FieldInfo lastScoreField;
		private const string lastScoreFieldName = "̘̖̗̐̑̏̏̏̓̎̕";

		public int BaseScore => (int)baseScoreField.GetValue(starProgress);
		private static FieldInfo baseScoreField;
		private const string baseScoreFieldName = "̛̘̘̖̘̙̏̓̍̓̕";

		// This appears to be the score required for the previous star acquired.
		public int ScoreToRemove => (int)scoreToRemoveField.GetValue(starProgress);
		private static FieldInfo scoreToRemoveField;
		private const string scoreToRemoveFieldName = "̘̖̒̑̍̏̓̏̐̓̕";

		// This appears to be 0 when you have zero stars, then the difference between the score from the last star to
		// the next star.
		public int ScoreToCompare => (int)scoreToCompareField.GetValue(starProgress);
		private static FieldInfo scoreToCompareField;
		private const string scoreToCompareFieldName = "̛̙̙̖̑̓̏̍̎̚̚";

		// Always 7, maybe I could...tweak this?
		public int MaxDisplayStar => (int)maxDisplayStarField.GetValue(starProgress);
		private static FieldInfo maxDisplayStarField;
		private const string maxDisplayStarFieldName = "̙̘̖̖̖̘̜̎̎̏̚";

		public int CurrentStar => (int)currentStarField.GetValue(starProgress);
		private static FieldInfo currentStarField;
		private const string currentStarFieldName = "̛̜̖̗̜̎̒̎̐̐̚";

		public int[] StarScores => (int[])starScoresField.GetValue(starProgress);
		private static FieldInfo starScoresField;
		private const string starScoresFieldName = "̘̘̘̜̗̜̒̔̑̚̚";

		public StarProgressWrapper(StarProgress starProgress) {
			InitializeSingletonFields();
			this.starProgress = starProgress;
			// TODO: Test what happens with this! It could be interesting!
			//maxDisplayStarField.SetValue(starProgress, 9);
		}

		private static void InitializeSingletonFields() {
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
