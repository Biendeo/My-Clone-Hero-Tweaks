using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class BasePlayerWrapper : WrapperBase {
		public readonly BasePlayer basePlayer;

		public bool IsStreakNotActive => (bool)isStreakNotActiveField.GetValue(basePlayer);
		private static FieldInfo isStreakNotActiveField;
		private const string isStreakNotActiveFieldName = "\u030D\u0311\u0317\u0317\u0318\u030D\u030D\u0314\u031B\u0312\u031B";

		public bool CanOverstrum => (bool)canOverstrumField.GetValue(basePlayer);
		private static FieldInfo canOverstrumField;
		private const string canOverstrumFieldName = "\u0312\u030E\u0312\u0319\u0312\u031A\u0319\u0316\u0316\u031B\u0319";

		public bool UnknownBool2 => (bool)unknownBool2Field.GetValue(basePlayer);
		private static FieldInfo unknownBool2Field;
		private const string unknownBool2FieldName = "\u0312\u0311\u031C\u0315\u0317\u0317\u030F\u031A\u030E\u030E\u0313"; // bool 3 Initially true, literally not used otherwise. It is public though.

		public bool UnknownBool4 => (bool)unknownBool4Field.GetValue(basePlayer);
		private static FieldInfo unknownBool4Field;
		private const string unknownBool4FieldName = "\u0313\u0319\u0310\u0318\u0316\u0311\u031A\u0312\u0313\u0311\u0310"; // bool 4 Only used in Update.

		public bool IsSoloActive => (bool)isSoloActiveField.GetValue(basePlayer);
		private static FieldInfo isSoloActiveField;
		private const string isSoloActiveFieldName = "\u031B\u031A\u0310\u0319\u0315\u030F\u0318\u0315\u031C\u030D\u0315";

		public bool IsEarningStarPower => (bool)isEarningStarPowerField.GetValue(basePlayer);
		private static FieldInfo isEarningStarPowerField;
		private const string isEarningStarPowerFieldName = "\u0315\u0318\u031C\u0314\u030D\u031C\u0315\u0310\u0314\u0319\u030F";

		public bool UnknownBool1 => (bool)unknownBool1Field.GetValue(basePlayer);
		private static FieldInfo unknownBool1Field;
		private const string unknownBool1FieldName = "\u0317\u0311\u0312\u0317\u0318\u031B\u031C\u0314\u0310\u0317\u030E"; // bool 7

		public bool IsSPActive => (bool)isSPActiveField.GetValue(basePlayer);
		private static FieldInfo isSPActiveField;
		private const string isSPActiveFieldName = "\u0314\u0316\u0316\u0314\u031B\u031B\u0310\u0312\u0317\u031A\u031C";

		public bool FirstNoteMissed => (bool)firstNoteMissedField.GetValue(basePlayer);
		private static FieldInfo firstNoteMissedField;
		private const string firstNoteMissedFieldName = "\u031C\u0318\u0316\u030D\u030D\u030E\u031C\u030F\u0314\u031A\u0316";

		public int HighestCombo => (int)highestComboField.GetValue(basePlayer);
		private static FieldInfo highestComboField;
		private const string highestComboFieldName = "\u030E\u0310\u0310\u031C\u0314\u0311\u0318\u0315\u0313\u0313\u0317";

		public int SoloIndex => (int)soloIndexField.GetValue(basePlayer);
		private static FieldInfo soloIndexField;
		private const string soloIndexFieldName = "\u0310\u0310\u0311\u0317\u0312\u030D\u0310\u0317\u030F\u030D\u031C";

		public int NotesSeen => (int)notesSeenField.GetValue(basePlayer);
		private static FieldInfo notesSeenField;
		private const string notesSeenFieldName = "\u0310\u0310\u0315\u0312\u030F\u0318\u0315\u0310\u0318\u0311\u0318";

		public int Score => (int)scoreField.GetValue(basePlayer);
		private static FieldInfo scoreField;
		private const string scoreFieldName = "\u0311\u0315\u0318\u0319\u0316\u0314\u0311\u0312\u0311\u030E\u0310";

		public int HittableNotesThisFrame => (int)hittableNotesThisFrameName.GetValue(basePlayer);
		private static FieldInfo hittableNotesThisFrameName;
		private const string hittableNotesThisFrameFieldName = "\u0312\u0310\u031B\u0315\u031A\u0313\u030D\u031C\u0314\u031B\u0312";

		public int UnknownInt6 => (int)unknownInt6Field.GetValue(basePlayer);
		private static FieldInfo unknownInt6Field;
		private const string unknownInt6FieldName = "\u0315\u030E\u0318\u031C\u030D\u030E\u030F\u031C\u031C\u0310\u030E"; //? Always 0?

		public int StarPowersHit => (int)starPowersHitField.GetValue(basePlayer);
		private static FieldInfo starPowersHitField;
		private const string starPowersHitFieldName = "\u0315\u0311\u031C\u0314\u031C\u031C\u031C\u0318\u0318\u0314\u0310";

		public static int BasePointsPerNote => (int)basePointsPerNoteField.GetValue(null);
		private static FieldInfo basePointsPerNoteField;
		private const string basePointsPerNoteFieldName = "\u0315\u031C\u0317\u0311\u030E\u031A\u0319\u0313\u0315\u0311\u030F";

		public int HitNotes => (int)hitNotesField.GetValue(basePlayer);
		private static FieldInfo hitNotesField;
		private const string hitNotesFieldName = "\u0316\u030F\u0311\u0317\u0310\u0318\u0315\u0311\u030E\u0310\u0311";

		public int Multiplier => (int)multiplierField.GetValue(basePlayer);
		private static FieldInfo multiplierField;
		private const string multiplierFieldName = "\u0316\u0317\u0312\u031B\u031B\u031A\u030F\u031C\u0315\u031B\u0313";

		public int Combo => (int)comboField.GetValue(basePlayer);
		private static FieldInfo comboField;
		private const string comboFieldName = "\u0317\u0310\u0312\u030D\u030E\u0316\u0318\u031C\u0317\u0313\u0314";

		public CHPlayerWrapper Player => new CHPlayerWrapper(playerField.GetValue(basePlayer));
		private static FieldInfo playerField;
		private const string playerFieldName = "\u0317\u0319\u0316\u030E\u031A\u030E\u031A\u031A\u0319\u0311\u0318";

		// This seems to always be of size 20, but will definitely contain nulls. Filter the nulls out before operating.
		public NoteWrapper[] HittableNotes => ((object[])hittableNotesField.GetValue(basePlayer)).Select(o => new NoteWrapper(o)).ToArray();
		private static FieldInfo hittableNotesField;
		private const string hittableNotesFieldName = "\u0315\u0318\u0315\u030F\u0310\u031A\u0310\u031B\u0316\u0312\u0315";

		public BasePlayerWrapper(BasePlayer basePlayer) {
			this.basePlayer = basePlayer;
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref isStreakNotActiveField, typeof(BasePlayer), isStreakNotActiveFieldName);
			RegisterField(ref canOverstrumField, typeof(BasePlayer), canOverstrumFieldName);
			RegisterField(ref unknownBool2Field, typeof(BasePlayer), unknownBool2FieldName);
			RegisterField(ref unknownBool4Field, typeof(BasePlayer), unknownBool4FieldName);
			RegisterField(ref isSoloActiveField, typeof(BasePlayer), isSoloActiveFieldName);
			RegisterField(ref isEarningStarPowerField, typeof(BasePlayer), isEarningStarPowerFieldName);
			RegisterField(ref unknownBool1Field, typeof(BasePlayer), unknownBool1FieldName);
			RegisterField(ref isSPActiveField, typeof(BasePlayer), isSPActiveFieldName);
			RegisterField(ref firstNoteMissedField, typeof(BasePlayer), firstNoteMissedFieldName);
			RegisterField(ref highestComboField, typeof(BasePlayer), highestComboFieldName);
			RegisterField(ref soloIndexField, typeof(BasePlayer), soloIndexFieldName);
			RegisterField(ref notesSeenField, typeof(BasePlayer), notesSeenFieldName);
			RegisterField(ref scoreField, typeof(BasePlayer), scoreFieldName);
			RegisterField(ref hittableNotesThisFrameName, typeof(BasePlayer), hittableNotesThisFrameFieldName);
			RegisterField(ref unknownInt6Field, typeof(BasePlayer), unknownInt6FieldName);
			RegisterField(ref starPowersHitField, typeof(BasePlayer), starPowersHitFieldName);
			RegisterStaticField(ref basePointsPerNoteField, typeof(BasePlayer), basePointsPerNoteFieldName);
			RegisterField(ref hitNotesField, typeof(BasePlayer), hitNotesFieldName);
			RegisterField(ref multiplierField, typeof(BasePlayer), multiplierFieldName);
			RegisterField(ref comboField, typeof(BasePlayer), comboFieldName);
			RegisterField(ref playerField, typeof(BasePlayer), playerFieldName);
			RegisterField(ref hittableNotesField, typeof(BasePlayer), hittableNotesFieldName);
		}
	}
}
