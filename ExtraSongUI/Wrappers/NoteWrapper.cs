using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class NoteWrapper : WrapperBase {
		//! \u030E\u0314\u030E\u0311\u0311\u0314\u030D\u030F\u0314\u0315\u0310
		public readonly object note;
		public static Type NoteType => Assembly.Load("Assembly-CSharp.dll").GetType("̎̔̎̑̑̔̍̏̔̐̕");

		public bool WasHit => (bool)wasHitField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo wasHitField;
		private const string wasHitFieldName = "̛̛̘̏̏̒̐̐̔̕̕";

		public bool WasMissed => (bool)wasMissedField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo wasMissedField;
		private const string wasMissedFieldName = "̙̙̖̒̎̏̐̔̒̑̒";

		public bool ThirdBool => (bool)thirdBoolField.GetValue(note); //? Doesn't seem to be set?
		private static FieldInfo thirdBoolField;
		private const string thirdBoolFieldName = "̖̜̖̖̘̎̔̍̏̍̚";

		public float Time => (float)float1Field.GetValue(note);
		private static FieldInfo float1Field;
		private const string float1FieldName = "̘̐̐̏̐̓̓̏̒̎̚";

		public bool Length => (bool)float2Field.GetValue(note);
		private static FieldInfo float2Field;
		private const string float2FieldName = "̜̜̙̗̜̜̜̒̔̒̚";

		public NoteWrapper(object note) {
			InitializeSingletonFields();
			this.note = note;
		}

		private static void InitializeSingletonFields() {
			RegisterField(ref wasHitField, NoteType, wasHitFieldName);
			RegisterField(ref wasMissedField, NoteType, wasMissedFieldName);
			RegisterField(ref thirdBoolField, NoteType, thirdBoolFieldName);
			RegisterField(ref float1Field, NoteType, float1FieldName);
			RegisterField(ref float2Field, NoteType, float2FieldName);
		}
	}
}
