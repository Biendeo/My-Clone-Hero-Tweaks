using System;
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

		public bool WasHit => (bool)wasHitField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo wasHitField;
		private const string wasHitFieldName = "\u031B\u030F\u0318\u030F\u0312\u0315\u031B\u0310\u0310\u0314\u0315";

		public bool WasMissed => (bool)wasMissedField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo wasMissedField;
		private const string wasMissedFieldName = "\u0312\u030E\u0319\u030F\u0310\u0314\u0312\u0311\u0319\u0316\u0312";

		public bool ThirdBool => (bool)thirdBoolField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo thirdBoolField;
		private const string thirdBoolFieldName = "\u0316\u031C\u030E\u031A\u0316\u0314\u0316\u0318\u030D\u030F\u030D";

		public float Time => (float)float1Field.GetValue(note);
		private static FieldInfo float1Field;
		private const string float1FieldName = "\u0310\u0310\u030F\u0310\u0313\u0313\u030F\u0318\u0312\u031A\u030E";

		public float Length => (float)float2Field.GetValue(note);
		private static FieldInfo float2Field;
		private const string float2FieldName = "\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A";

		public byte NoteMask => (byte)noteMaskField.GetValue(note);
		private static FieldInfo noteMaskField;
		private const string noteMaskFieldName = "\u0318\u0316\u0315\u031A\u0313\u0310\u0316\u030E\u0310\u031A\u0318";

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note1 => new NoteWrapper(note1Field.GetValue(note));
		private static FieldInfo note1Field;
		private const string note1FieldName = "\u0310\u0318\u0319\u0310\u0316\u0316\u0319\u0317\u031C\u0316\u0318";

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note2 => new NoteWrapper(note2Field.GetValue(note));
		private static FieldInfo note2Field;
		private const string note2FieldName = "\u030F\u0314\u030D\u0314\u030D\u031B\u0316\u0314\u0316\u0318\u031A";

		public NoteWrapper(object note) {
			this.note = note;
		}

		public static void InitializeSingletonFields() {
			NoteType = Assembly.Load("Assembly-CSharp.dll").GetType("\u030E\u0314\u030E\u0311\u0311\u0314\u030D\u030F\u0314\u0315\u0310");
			RegisterField(ref wasHitField, NoteType, wasHitFieldName);
			RegisterField(ref wasMissedField, NoteType, wasMissedFieldName);
			RegisterField(ref thirdBoolField, NoteType, thirdBoolFieldName);
			RegisterField(ref float1Field, NoteType, float1FieldName);
			RegisterField(ref float2Field, NoteType, float2FieldName);
			RegisterField(ref noteMaskField, NoteType, noteMaskFieldName);
			RegisterField(ref note1Field, NoteType, note1FieldName);
			RegisterField(ref note2Field, NoteType, note2FieldName);

		}
	}
}
