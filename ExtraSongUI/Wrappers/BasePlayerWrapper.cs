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
		private const string isStreakNotActiveFieldName = "̛̛̗̗̘̍̑̍̍̔̒";

		public bool CanOverstrum => (bool)canOverstrumField.GetValue(basePlayer);
		private static FieldInfo canOverstrumField;
		private const string canOverstrumFieldName = "̛̙̙̖̖̙̒̎̒̒̚";

		public bool UnknownBool2 => (bool)unknownBool2Field.GetValue(basePlayer);
		private static FieldInfo unknownBool2Field;
		private const string unknownBool2FieldName = "̜̗̗̒̑̏̎̎̓̕̚"; // bool 3 Initially true, literally not used otherwise. It is public though.

		public bool UnknownBool4 => (bool)unknownBool4Field.GetValue(basePlayer);
		private static FieldInfo unknownBool4Field;
		private const string unknownBool4FieldName = "̙̘̖̓̐̑̒̓̑̐̚"; // bool 4 Only used in Update.

		public bool IsSoloActive => (bool)isSoloActiveField.GetValue(basePlayer);
		private static FieldInfo isSoloActiveField;
		private const string isSoloActiveFieldName = "̛̙̘̜̐̏̍̚̕̕̕";

		public bool IsEarningStarPower => (bool)isEarningStarPowerField.GetValue(basePlayer);
		private static FieldInfo isEarningStarPowerField;
		private const string isEarningStarPowerFieldName = "̘̜̜̙̔̍̐̔̏̕̕";

		public bool UnknownBool1 => (bool)unknownBool1Field.GetValue(basePlayer);
		private static FieldInfo unknownBool1Field;
		private const string unknownBool1FieldName = "̛̗̗̘̜̗̑̒̔̐̎"; // bool 7

		public bool IsSPActive => (bool)isSPActiveField.GetValue(basePlayer);
		private static FieldInfo isSPActiveField;
		private const string isSPActiveFieldName = "̛̛̖̖̗̜̔̔̐̒̚";

		public bool FirstNoteMissed => (bool)firstNoteMissedField.GetValue(basePlayer);
		private static FieldInfo firstNoteMissedField;
		private const string firstNoteMissedFieldName = "̜̘̖̜̖̍̍̎̏̔̚";

		public int HighestCombo => (int)highestComboField.GetValue(basePlayer);
		private static FieldInfo highestComboField;
		private const string highestComboFieldName = "̜̘̗̎̐̐̔̑̓̓̕";

		public int SoloIndex => (int)soloIndexField.GetValue(basePlayer);
		private static FieldInfo soloIndexField;
		private const string soloIndexFieldName = "̗̗̜̐̐̑̒̍̐̏̍";

		public int NotesSeen => (int)notesSeenField.GetValue(basePlayer);
		private static FieldInfo notesSeenField;
		private const string notesSeenFieldName = "̘̘̘̐̐̒̏̐̑̕̕";

		public int Score => (int)scoreField.GetValue(basePlayer);
		private static FieldInfo scoreField;
		private const string scoreFieldName = "̘̙̖̑̔̑̒̑̎̐̕";

		public int HittableNotesThisFrame => (int)hittableNotesThisFrameName.GetValue(basePlayer);
		private static FieldInfo hittableNotesThisFrameName;
		private const string hittableNotesThisFrameFieldName = "̛̛̜̒̐̓̍̔̒̕̚";

		public int UnknownInt6 => (int)unknownInt6Field.GetValue(basePlayer);
		private static FieldInfo unknownInt6Field;
		private const string unknownInt6FieldName = "̘̜̜̜̎̍̎̏̐̎̕"; //? Always 0?

		public int StarPowersHit => (int)starPowersHitField.GetValue(basePlayer);
		private static FieldInfo starPowersHitField;
		private const string starPowersHitFieldName = "̜̜̜̜̘̘̑̔̔̐̕";

		public static int BasePointsPerNote => (int)basePointsPerNoteField.GetValue(null);
		private static FieldInfo basePointsPerNoteField;
		private const string basePointsPerNoteFieldName = "̜̗̙̑̎̓̑̏̕̚̕";

		public int HitNotes => (int)hitNotesField.GetValue(basePlayer);
		private static FieldInfo hitNotesField;
		private const string hitNotesFieldName = "̖̗̘̏̑̐̑̎̐̑̕";

		public int Multiplier => (int)multiplierField.GetValue(basePlayer);
		private static FieldInfo multiplierField;
		private const string multiplierFieldName = "̛̛̛̖̗̜̒̏̓̚̕";

		public int Combo => (int)comboField.GetValue(basePlayer);
		private static FieldInfo comboField;
		private const string comboFieldName = "̗̖̘̜̗̐̒̍̎̓̔";

		public CHPlayerWrapper Player => new CHPlayerWrapper(playerField.GetValue(basePlayer));
		private static FieldInfo playerField;
		private const string playerFieldName = "̗̙̖̙̘̎̎̑̚̚̚";

		public BasePlayerWrapper(BasePlayer basePlayer) {
			InitializeSingletonFields();
			this.basePlayer = basePlayer;
		}

		private static void InitializeSingletonFields() {
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
		}
	}
}
