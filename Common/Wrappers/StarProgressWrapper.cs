using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(StarProgress))]
	internal class StarProgressWrapper {
		public readonly StarProgress starProgress;

		public int LastScore => (int)lastScoreField.GetValue(starProgress);
		[WrapperField("\u0310\u0318\u0311\u030F\u030F\u030F\u0315\u0313\u0316\u030E\u0317")]
		private static FieldInfo lastScoreField;

		public int BaseScore => (int)baseScoreField.GetValue(starProgress);
		[WrapperField("\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313")]
		private static FieldInfo baseScoreField;

		// This appears to be the score required for the previous star acquired.
		public int ScoreToRemove => (int)scoreToRemoveField.GetValue(starProgress);
		[WrapperField("\u0315\u0312\u0311\u030D\u0318\u030F\u0313\u030F\u0310\u0316\u0313")]
		private static FieldInfo scoreToRemoveField;

		// This appears to be 0 when you have zero stars, then the difference between the score from the last star to
		// the next star.
		public int ScoreToCompare => (int)scoreToCompareField.GetValue(starProgress);
		[WrapperField("\u0319\u0311\u0313\u031A\u030F\u0319\u031A\u030D\u030E\u0316\u031B")]
		private static FieldInfo scoreToCompareField;

		// Always 7, maybe I could...tweak this?
		public int MaxDisplayStar => (int)maxDisplayStarField.GetValue(starProgress);
		[WrapperField("\u0319\u0318\u0316\u0316\u030E\u0316\u031A\u0318\u030E\u031C\u030F")]
		private static FieldInfo maxDisplayStarField;

		public int CurrentStar => (int)currentStarField.GetValue(starProgress);
		[WrapperField("\u031C\u030E\u0316\u0312\u0317\u030E\u0310\u031C\u031B\u0310\u031A")]
		private static FieldInfo currentStarField;

		public int[] StarScores => (int[])starScoresField.GetValue(starProgress);
		[WrapperField("\u031A\u0312\u0318\u0314\u0318\u0318\u031C\u0317\u0311\u031C\u031A")]
		private static FieldInfo starScoresField;

		public StarProgressWrapper(StarProgress starProgress) {
			this.starProgress = starProgress;
			// TODO: Test what happens with this! It could be interesting!
			//maxDisplayStarField.SetValue(starProgress, 9);
		}
	}
}
