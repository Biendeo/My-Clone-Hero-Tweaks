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
		private static readonly FieldInfo lastScoreField;

		public int BaseScore => (int)baseScoreField.GetValue(starProgress);
		[WrapperField("\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313")]
		private static readonly FieldInfo baseScoreField;

		// This appears to be the score required for the previous star acquired.
		public int ScoreToRemove => (int)scoreToRemoveField.GetValue(starProgress);
		[WrapperField("\u0315\u0312\u0311\u030D\u0318\u030F\u0313\u030F\u0310\u0316\u0313")]
		private static readonly FieldInfo scoreToRemoveField;

		// This appears to be 0 when you have zero stars, then the difference between the score from the last star to
		// the next star.
		public int ScoreToCompare => (int)scoreToCompareField.GetValue(starProgress);
		[WrapperField("\u0319\u0311\u0313\u031A\u030F\u0319\u031A\u030D\u030E\u0316\u031B")]
		private static readonly FieldInfo scoreToCompareField;

		// Always 7, maybe I could...tweak this?
		public int MaxDisplayStar => (int)maxDisplayStarField.GetValue(starProgress);
		[WrapperField("\u0319\u0318\u0316\u0316\u030E\u0316\u031A\u0318\u030E\u031C\u030F")]
		private static readonly FieldInfo maxDisplayStarField;

		public int CurrentStar => (int)currentStarField.GetValue(starProgress);
		[WrapperField("\u031C\u030E\u0316\u0312\u0317\u030E\u0310\u031C\u031B\u0310\u031A")]
		private static readonly FieldInfo currentStarField;

		public int[] StarScores => (int[])starScoresField.GetValue(starProgress);
		[WrapperField("\u031A\u0312\u0318\u0314\u0318\u0318\u031C\u0317\u0311\u031C\u031A")]
		private static readonly FieldInfo starScoresField;

		public float EndBeginning => (float)endBeginningField.GetValue(starProgress);
		[WrapperField("\u030D\u030D\u0311\u031A\u030E\u0319\u0314\u030F\u031A\u0311\u031A")]
		private static readonly FieldInfo endBeginningField;

		public float BarMaxScale => (float)barMaxScaleField.GetValue(starProgress);
		[WrapperField("\u030E\u0314\u0313\u0319\u0314\u0315\u0319\u0317\u031C\u031C\u031A")]
		private static readonly FieldInfo barMaxScaleField;

		public float EndEnd => (float)endEndField.GetValue(starProgress);
		[WrapperField("\u0315\u031C\u0318\u0310\u0319\u030D\u0310\u0310\u030F\u0318\u0317")]
		private static readonly FieldInfo endEndField;

		public float EndY => (float)endYField.GetValue(starProgress);
		[WrapperField("\u031C\u0318\u030D\u030D\u0319\u0316\u030E\u0317\u030E\u0311\u0319")]
		private static readonly FieldInfo endYField;

		public static float[] StarMultipliers => (float[])starMultipliersField.GetValue(null);
		[WrapperField("\u0313\u030D\u0310\u030E\u0316\u030E\u030E\u0312\u0310\u0310\u0310")]
		private static readonly FieldInfo starMultipliersField;

		public Transform Transform1 => (Transform)transform1Field.GetValue(starProgress);
		[WrapperField("\u0311\u0316\u0310\u0311\u031C\u0317\u0317\u031C\u0311\u031A\u0312")]
		private static readonly FieldInfo transform1Field;

		public Transform Transform2 => (Transform)transform2Field.GetValue(starProgress);
		[WrapperField("\u0317\u0318\u031B\u0311\u0312\u030D\u030F\u031B\u030F\u031B\u0318")]
		private static readonly FieldInfo transform2Field;

		public SpriteRenderer SpriteRenderer1 => (SpriteRenderer)spriteRenderer1Field.GetValue(starProgress);
		[WrapperField("\u0317\u0317\u0317\u0316\u0317\u031A\u0313\u031C\u0316\u031C\u030E")]
		private static readonly FieldInfo spriteRenderer1Field;

		public SpriteRenderer SpriteRenderer2 => (SpriteRenderer)spriteRenderer2Field.GetValue(starProgress);
		[WrapperField("\u031B\u031A\u0316\u0318\u0314\u0317\u030D\u030E\u0311\u031A\u031B")]
		private static readonly FieldInfo spriteRenderer2Field;

		public Sprite[] SpriteArray => (Sprite[])spriteArrayField.GetValue(starProgress);
		[WrapperField("\u0318\u030F\u0317\u030F\u0316\u0317\u030E\u030E\u0312\u0317\u0312")]
		private static readonly FieldInfo spriteArrayField;

		public StarProgressWrapper(StarProgress starProgress) {
			this.starProgress = starProgress;
			// TODO: Test what happens with this! It could be interesting!
			//maxDisplayStarField.SetValue(starProgress, 9);
		}
	}
}
