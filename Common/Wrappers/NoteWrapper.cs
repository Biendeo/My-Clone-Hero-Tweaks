using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	internal class NoteWrapper : WrapperBase {
		public readonly object note;
		public static Type NoteType;

		public bool WasHit => (bool)wasHitField.GetValue(note);
		private static FieldInfo wasHitField;
		private const string wasHitFieldName = "\u031B\u030F\u0318\u030F\u0312\u0315\u031B\u0310\u0310\u0314\u0315";

		public bool WasMissed => (bool)wasMissedField.GetValue(note);
		private static FieldInfo wasMissedField;
		private const string wasMissedFieldName = "\u0312\u030E\u0319\u030F\u0310\u0314\u0312\u0311\u0319\u0316\u0312";

		public bool IsSustaining => (bool)isSustainingField.GetValue(note);
		private static FieldInfo isSustainingField;
		private const string isSustainingFieldName = "\u0316\u031C\u030E\u031A\u0316\u0314\u0316\u0318\u030D\u030F\u030D";

		public float Time => (float)float1Field.GetValue(note);
		private static FieldInfo float1Field;
		private const string float1FieldName = "\u0310\u0310\u030F\u0310\u0313\u0313\u030F\u0318\u0312\u031A\u030E";

		public float Length => (float)float2Field.GetValue(note);
		private static FieldInfo float2Field;
		private const string float2FieldName = "\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A";

		public byte NoteMask => (byte)noteMaskField.GetValue(note);
		private static FieldInfo noteMaskField;
		private const string noteMaskFieldName = "\u0318\u0316\u0315\u031A\u0313\u0310\u0316\u030E\u0310\u031A\u0318";

		public uint TickPosition => (uint)tickPositionField.GetValue(note);
		private static FieldInfo tickPositionField;
		private const string tickPositionFieldName = "\u031B\u0310\u0316\u0316\u0314\u0318\u0313\u030E\u0315\u0316\u031C";

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note1 => new NoteWrapper(note1Field.GetValue(note));
		private static FieldInfo note1Field;
		private const string note1FieldName = "\u0310\u0318\u0319\u0310\u0316\u0316\u0319\u0317\u031C\u0316\u0318";

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note2 => new NoteWrapper(note2Field.GetValue(note));
		private static FieldInfo note2Field;
		private const string note2FieldName = "\u030F\u0314\u030D\u0314\u030D\u031B\u0316\u0314\u0316\u0318\u031A";

		public IEnumerable<NoteWrapper> UnknownNoteThingy => ((IEnumerable)unknownNoteThingyProperty.GetValue(note)).Cast<object>().Select(o => new NoteWrapper(o));
		private static PropertyInfo unknownNoteThingyProperty;
		private const string unknownNoteThingyPropertyName = "\u0319\u0315\u0315\u0319\u0313\u0313\u0311\u0313\u0310\u0314\u0317";

		public bool IsHopo => (bool)isHopoProperty.GetValue(note);
		private static PropertyInfo isHopoProperty;
		private const string isHopoPropertyName = "\u0310\u030F\u030E\u0310\u031A\u0318\u031A\u031A\u0311\u0311\u0313";

		public bool IsDisjoint => (bool)isDisjointProperty.GetValue(note);
		private static PropertyInfo isDisjointProperty;
		private const string isDisjointPropertyName = "\u0311\u031A\u030D\u0315\u030F\u0315\u0310\u0314\u0314\u031A\u0315";

		public bool IsSoloBegin => (bool)isSoloBeginProperty.GetValue(note);
		private static PropertyInfo isSoloBeginProperty;
		private const string isSoloBeginPropertyName = "\u0313\u0310\u031B\u030F\u0311\u0318\u030F\u0313\u0316\u0311\u031C";

		public bool IsSoloEnd => (bool)isSoloEndProperty.GetValue(note);
		private static PropertyInfo isSoloEndProperty;
		private const string isSoloEndPropertyName = "\u0315\u0313\u031B\u0312\u0312\u0312\u0312\u031B\u0318\u031C\u0319";

		public bool IsSlave => (bool)isSlaveProperty.GetValue(note);
		private static PropertyInfo isSlaveProperty;
		private const string isSlavePropertyName = "\u0316\u031C\u030D\u0318\u0319\u0318\u0316\u0314\u0318\u0313\u0316";

		public bool IsStarPower => (bool)isStarPowerProperty.GetValue(note);
		private static PropertyInfo isStarPowerProperty;
		private const string isStarPowerPropertyName = "\u0317\u0311\u031A\u030F\u030D\u0311\u030D\u0314\u030E\u0311\u0314";

		public bool IsChord => (bool)isChordProperty.GetValue(note);
		private static PropertyInfo isChordProperty;
		private const string isChordPropertyName = "\u0317\u031A\u031B\u0316\u031A\u0310\u0313\u0315\u0316\u0318\u0317";

		public bool IsStarPowerEnd => (bool)isStarPowerEndProperty.GetValue(note);
		private static PropertyInfo isStarPowerEndProperty;
		private const string isStarPowerEndPropertyName = "\u0318\u0315\u031C\u030F\u031C\u0316\u0310\u0319\u0314\u030E\u030D";

		public bool IsTap => (bool)isTapProperty.GetValue(note);
		private static PropertyInfo isTapProperty;
		private const string isTapPropertyName = "\u031A\u030F\u0311\u030F\u031B\u0311\u031C\u0310\u0317\u0313\u0317";

		public bool IsExtendedSustain => (bool)isExtendedSustainProperty.GetValue(note);
		private static PropertyInfo isExtendedSustainProperty;
		private const string isExtendedSustainPropertyName = "\u031C\u030D\u0315\u031A\u0318\u0319\u030F\u0313\u031B\u031A\u0310";

		public NoteWrapper(object note) {
			this.note = note;
		}

		public static void InitializeSingletonFields() {
			NoteType = Assembly.Load("Assembly-CSharp.dll").GetType("\u030E\u0314\u030E\u0311\u0311\u0314\u030D\u030F\u0314\u0315\u0310");
			RegisterField(ref wasHitField, NoteType, wasHitFieldName);
			RegisterField(ref wasMissedField, NoteType, wasMissedFieldName);
			RegisterField(ref isSustainingField, NoteType, isSustainingFieldName);
			RegisterField(ref float1Field, NoteType, float1FieldName);
			RegisterField(ref float2Field, NoteType, float2FieldName);
			RegisterField(ref noteMaskField, NoteType, noteMaskFieldName);
			RegisterField(ref tickPositionField, NoteType, tickPositionFieldName);
			RegisterField(ref note1Field, NoteType, note1FieldName);
			RegisterField(ref note2Field, NoteType, note2FieldName);
			RegisterProperty(ref unknownNoteThingyProperty, NoteType, unknownNoteThingyPropertyName);
			RegisterProperty(ref isHopoProperty, NoteType, isHopoPropertyName);
			RegisterProperty(ref isDisjointProperty, NoteType, isDisjointPropertyName);
			RegisterProperty(ref isSoloBeginProperty, NoteType, isSoloBeginPropertyName);
			RegisterProperty(ref isSoloEndProperty, NoteType, isSoloEndPropertyName);
			RegisterProperty(ref isSlaveProperty, NoteType, isSlavePropertyName);
			RegisterProperty(ref isStarPowerProperty, NoteType, isStarPowerPropertyName);
			RegisterProperty(ref isChordProperty, NoteType, isChordPropertyName);
			RegisterProperty(ref isStarPowerEndProperty, NoteType, isStarPowerEndPropertyName);
			RegisterProperty(ref isTapProperty, NoteType, isTapPropertyName);
			RegisterProperty(ref isExtendedSustainProperty, NoteType, isExtendedSustainPropertyName);
		}
	}
}
