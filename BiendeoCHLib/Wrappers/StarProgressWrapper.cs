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
	[Wrapper(typeof(StarProgress))]
	public struct StarProgressWrapper {
		public StarProgress StarProgress { get; private set; }

		public static StarProgressWrapper Wrap(StarProgress starProgress) => new StarProgressWrapper {
			StarProgress = starProgress
		};

		public override bool Equals(object obj) => StarProgress.Equals(obj);

		public override int GetHashCode() => StarProgress.GetHashCode();

		public bool IsNull() => StarProgress == null;

		#region Fields

		public int LastScore {
			get => lastScoreField(StarProgress);
			set => lastScoreField(StarProgress) = value;
		}
		[WrapperField("\u0310\u0318\u0311\u030F\u030F\u030F\u0315\u0313\u0316\u030E\u0317")]
		private static readonly AccessTools.FieldRef<StarProgress, int> lastScoreField;

		public int BaseScore {
			get => baseScoreField(StarProgress);
			set => baseScoreField(StarProgress) = value;
		}
		[WrapperField("\u0315\u030F\u0313\u0318\u0318\u0316\u0318\u031B\u030D\u0319\u0313")]
		private static readonly AccessTools.FieldRef<StarProgress, int> baseScoreField;

		// This appears to be the score required for the previous star acquired.
		public int ScoreToRemove {
			get => scoreToRemoveField(StarProgress);
			set => scoreToRemoveField(StarProgress) = value;
		}
		[WrapperField("\u0315\u0312\u0311\u030D\u0318\u030F\u0313\u030F\u0310\u0316\u0313")]
		private static readonly AccessTools.FieldRef<StarProgress, int> scoreToRemoveField;

		// This appears to be 0 when you have zero stars, then the difference between the score from the last star to
		// the next star.
		public int ScoreToCompare {
			get => scoreToCompareField(StarProgress);
			set => scoreToCompareField(StarProgress) = value;
		}
		[WrapperField("\u0319\u0311\u0313\u031A\u030F\u0319\u031A\u030D\u030E\u0316\u031B")]
		private static readonly AccessTools.FieldRef<StarProgress, int> scoreToCompareField;

		// Always 7, maybe I could...tweak this?
		public int MaxDisplayStar {
			get => maxDisplayStarField(StarProgress);
			set => maxDisplayStarField(StarProgress) = value;
		}
		[WrapperField("\u0319\u0318\u0316\u0316\u030E\u0316\u031A\u0318\u030E\u031C\u030F")]
		private static readonly AccessTools.FieldRef<StarProgress, int> maxDisplayStarField;

		public int CurrentStar {
			get => currentStarField(StarProgress);
			set => currentStarField(StarProgress) = value;
		}
		[WrapperField("\u031C\u030E\u0316\u0312\u0317\u030E\u0310\u031C\u031B\u0310\u031A")]
		private static readonly AccessTools.FieldRef<StarProgress, int> currentStarField;

		public int[] StarScores {
			get => starScoresField(StarProgress);
			set => starScoresField(StarProgress) = value;
		}
		[WrapperField("\u031A\u0312\u0318\u0314\u0318\u0318\u031C\u0317\u0311\u031C\u031A")]
		private static readonly AccessTools.FieldRef<StarProgress, int[]> starScoresField;

		public float EndBeginning {
			get => endBeginningField(StarProgress);
			set => endBeginningField(StarProgress) = value;
		}
		[WrapperField("\u030D\u030D\u0311\u031A\u030E\u0319\u0314\u030F\u031A\u0311\u031A")]
		private static readonly AccessTools.FieldRef<StarProgress, float> endBeginningField;

		public float BarMaxScale {
			get => barMaxScaleField(StarProgress);
			set => barMaxScaleField(StarProgress) = value;
		}
		[WrapperField("\u030E\u0314\u0313\u0319\u0314\u0315\u0319\u0317\u031C\u031C\u031A")]
		private static readonly AccessTools.FieldRef<StarProgress, float> barMaxScaleField;

		public float EndEnd {
			get => endEndField(StarProgress);
			set => endEndField(StarProgress) = value;
		}
		[WrapperField("\u0315\u031C\u0318\u0310\u0319\u030D\u0310\u0310\u030F\u0318\u0317")]
		private static readonly AccessTools.FieldRef<StarProgress, float> endEndField;

		public float EndY {
			get => endYField(StarProgress);
			set => endYField(StarProgress) = value;
		}
		[WrapperField("\u031C\u0318\u030D\u030D\u0319\u0316\u030E\u0317\u030E\u0311\u0319")]
		private static readonly AccessTools.FieldRef<StarProgress, float> endYField;

		public static float[] StarMultipliers {
			get => (float[])starMultipliersField.GetValue(null);
			set => starMultipliersField.SetValue(null, value);
		}
		[WrapperField("\u0313\u030D\u0310\u030E\u0316\u030E\u030E\u0312\u0310\u0310\u0310")]
		private static readonly FieldInfo starMultipliersField;

		public Transform Transform1 {
			get => transform1Field(StarProgress);
			set => transform1Field(StarProgress) = value;
		}
		[WrapperField("\u0311\u0316\u0310\u0311\u031C\u0317\u0317\u031C\u0311\u031A\u0312")]
		private static readonly AccessTools.FieldRef<StarProgress, Transform> transform1Field;

		public Transform Transform2 {
			get => transform2Field(StarProgress);
			set => transform2Field(StarProgress) = value;
		}
		[WrapperField("\u0317\u0318\u031B\u0311\u0312\u030D\u030F\u031B\u030F\u031B\u0318")]
		private static readonly AccessTools.FieldRef<StarProgress, Transform> transform2Field;

		public SpriteRenderer SpriteRenderer1 {
			get => spriteRenderer1Field(StarProgress);
			set => spriteRenderer1Field(StarProgress) = value;
		}
		[WrapperField("\u0317\u0317\u0317\u0316\u0317\u031A\u0313\u031C\u0316\u031C\u030E")]
		private static readonly AccessTools.FieldRef<StarProgress, SpriteRenderer> spriteRenderer1Field;

		public SpriteRenderer SpriteRenderer2 {
			get => spriteRenderer2Field(StarProgress);
			set => spriteRenderer2Field(StarProgress) = value;
		}
		[WrapperField("\u031B\u031A\u0316\u0318\u0314\u0317\u030D\u030E\u0311\u031A\u031B")]
		private static readonly AccessTools.FieldRef<StarProgress, SpriteRenderer> spriteRenderer2Field;

		public Sprite[] SpriteArray {
			get => spriteArrayField(StarProgress);
			set => spriteArrayField(StarProgress) = value;
		}
		[WrapperField("\u0318\u030F\u0317\u030F\u0316\u0317\u030E\u030E\u0312\u0317\u0312")]
		private static readonly AccessTools.FieldRef<StarProgress, Sprite[]> spriteArrayField;

		#endregion
	}
}
