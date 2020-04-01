using Common.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(BasePlayer))]
	internal class BasePlayerWrapper {
		public readonly BasePlayer basePlayer;

		public bool IsStreakNotActive => (bool)isStreakNotActiveField.GetValue(basePlayer);
		[WrapperField("\u030D\u0311\u0317\u0317\u0318\u030D\u030D\u0314\u031B\u0312\u031B")]
		private static readonly FieldInfo isStreakNotActiveField;

		public bool CanOverstrum => (bool)canOverstrumField.GetValue(basePlayer);
		[WrapperField("\u0312\u030E\u0312\u0319\u0312\u031A\u0319\u0316\u0316\u031B\u0319")]
		private static readonly FieldInfo canOverstrumField;

		public bool UnknownBool2 => (bool)unknownBool2Field.GetValue(basePlayer);
		[WrapperField("\u0312\u0311\u031C\u0315\u0317\u0317\u030F\u031A\u030E\u030E\u0313")]
		private static readonly FieldInfo unknownBool2Field; // bool 3 Initially true, literally not used otherwise. It is public though.

		public bool UnknownBool4 => (bool)unknownBool4Field.GetValue(basePlayer);
		[WrapperField("\u0313\u0319\u0310\u0318\u0316\u0311\u031A\u0312\u0313\u0311\u0310")]
		private static readonly FieldInfo unknownBool4Field; // bool 4 Only used in Update.

		public bool IsSoloActive => (bool)isSoloActiveField.GetValue(basePlayer);
		[WrapperField("\u031B\u031A\u0310\u0319\u0315\u030F\u0318\u0315\u031C\u030D\u0315")]
		private static readonly FieldInfo isSoloActiveField;

		public bool IsEarningStarPower => (bool)isEarningStarPowerField.GetValue(basePlayer);
		[WrapperField("\u0315\u0318\u031C\u0314\u030D\u031C\u0315\u0310\u0314\u0319\u030F")]
		private static readonly FieldInfo isEarningStarPowerField;

		public bool UnknownBool1 => (bool)unknownBool1Field.GetValue(basePlayer);
		[WrapperField("\u0317\u0311\u0312\u0317\u0318\u031B\u031C\u0314\u0310\u0317\u030E")]
		private static readonly FieldInfo unknownBool1Field; // bool 7

		public bool IsSPActive => (bool)isSPActiveField.GetValue(basePlayer);
		[WrapperField("\u0314\u0316\u0316\u0314\u031B\u031B\u0310\u0312\u0317\u031A\u031C")]
		private static readonly FieldInfo isSPActiveField;

		public bool FirstNoteMissed => (bool)firstNoteMissedField.GetValue(basePlayer);
		[WrapperField("\u031C\u0318\u0316\u030D\u030D\u030E\u031C\u030F\u0314\u031A\u0316")]
		private static readonly FieldInfo firstNoteMissedField;

		public int HighestCombo => (int)highestComboField.GetValue(basePlayer);
		[WrapperField("\u030E\u0310\u0310\u031C\u0314\u0311\u0318\u0315\u0313\u0313\u0317")]
		private static readonly FieldInfo highestComboField;

		public int SoloIndex => (int)soloIndexField.GetValue(basePlayer);
		[WrapperField("\u0310\u0310\u0311\u0317\u0312\u030D\u0310\u0317\u030F\u030D\u031C")]
		private static readonly FieldInfo soloIndexField;

		public int NotesSeen => (int)notesSeenField.GetValue(basePlayer);
		[WrapperField("\u0310\u0310\u0315\u0312\u030F\u0318\u0315\u0310\u0318\u0311\u0318")]
		private static readonly FieldInfo notesSeenField;

		public int Score => (int)scoreField.GetValue(basePlayer);
		[WrapperField("\u0311\u0315\u0318\u0319\u0316\u0314\u0311\u0312\u0311\u030E\u0310")]
		private static readonly FieldInfo scoreField;

		public int HittableNotesThisFrame => (int)hittableNotesThisFrameName.GetValue(basePlayer);
		[WrapperField("\u0312\u0310\u031B\u0315\u031A\u0313\u030D\u031C\u0314\u031B\u0312")]
		private static readonly FieldInfo hittableNotesThisFrameName;

		public int UnknownInt6 => (int)unknownInt6Field.GetValue(basePlayer);
		[WrapperField("\u0315\u030E\u0318\u031C\u030D\u030E\u030F\u031C\u031C\u0310\u030E")]
		private static readonly FieldInfo unknownInt6Field; //? Always 0?

		public int StarPowersHit => (int)starPowersHitField.GetValue(basePlayer);
		[WrapperField("\u0315\u0311\u031C\u0314\u031C\u031C\u031C\u0318\u0318\u0314\u0310")]
		private static readonly FieldInfo starPowersHitField;

		public static int BasePointsPerNote => (int)basePointsPerNoteField.GetValue(null);
		[WrapperField("\u0315\u031C\u0317\u0311\u030E\u031A\u0319\u0313\u0315\u0311\u030F")]
		private static readonly FieldInfo basePointsPerNoteField;

		public int HitNotes => (int)hitNotesField.GetValue(basePlayer);
		[WrapperField("\u0316\u030F\u0311\u0317\u0310\u0318\u0315\u0311\u030E\u0310\u0311")]
		private static readonly FieldInfo hitNotesField;

		public int Multiplier => (int)multiplierField.GetValue(basePlayer);
		[WrapperField("\u0316\u0317\u0312\u031B\u031B\u031A\u030F\u031C\u0315\u031B\u0313")]
		private static readonly FieldInfo multiplierField;

		public int Combo => (int)comboField.GetValue(basePlayer);
		[WrapperField("\u0317\u0310\u0312\u030D\u030E\u0316\u0318\u031C\u0317\u0313\u0314")]
		private static readonly FieldInfo comboField;

		public CHPlayerWrapper Player => new CHPlayerWrapper(playerField.GetValue(basePlayer));
		[WrapperField("\u0317\u0319\u0316\u030E\u031A\u030E\u031A\u031A\u0319\u0311\u0318")]
		private static readonly FieldInfo playerField;

		// This seems to always be of size 20, but will definitely contain nulls. Filter the nulls out before operating.
		public NoteWrapper[] HittableNotes => ((object[])hittableNotesField.GetValue(basePlayer)).Select(o => new NoteWrapper(o)).ToArray();
		[WrapperField("\u0315\u0318\u0315\u030F\u0310\u031A\u0310\u031B\u0316\u0312\u0315")]
		private static readonly FieldInfo hittableNotesField;

		public List<NoteWrapper> Notes => ((ICollection)notesField.GetValue(basePlayer))?.Cast<object>().Select(o => new NoteWrapper(o)).ToList();
		[WrapperField("\u031A\u0316\u0315\u0318\u0319\u0315\u0316\u0313\u0315\u0315\u0312")]
		private static readonly FieldInfo notesField;

		public SoloCounterWrapper SoloCounter => new SoloCounterWrapper((SoloCounter)soloCounterField.GetValue(basePlayer));
		[WrapperField("\u0315\u031B\u030E\u0319\u0310\u030E\u030F\u031B\u031A\u030E\u030F")]
		private static readonly FieldInfo soloCounterField;

		public SPBarWrapper spBar => new SPBarWrapper((SPBar)spBarField.GetValue(basePlayer));
		[WrapperField("spBar")]
		private static readonly FieldInfo spBarField;

		public BasePlayerWrapper(BasePlayer basePlayer) {
			this.basePlayer = basePlayer;
		}
	}
}
