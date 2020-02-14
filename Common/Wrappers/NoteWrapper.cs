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
	[Wrapper("\u030E\u0314\u030E\u0311\u0311\u0314\u030D\u030F\u0314\u0315\u0310")]
	internal class NoteWrapper {
		public readonly object note;
		public static Type NoteType;

		public bool WasHit => (bool)wasHitField.GetValue(note);
		[WrapperField("\u031B\u030F\u0318\u030F\u0312\u0315\u031B\u0310\u0310\u0314\u0315")]
		private static readonly FieldInfo wasHitField;

		public bool WasMissed => (bool)wasMissedField.GetValue(note);
		[WrapperField("\u0312\u030E\u0319\u030F\u0310\u0314\u0312\u0311\u0319\u0316\u0312")]
		private static readonly FieldInfo wasMissedField;

		public bool IsSustaining => (bool)isSustainingField.GetValue(note);
		[WrapperField("\u0316\u031C\u030E\u031A\u0316\u0314\u0316\u0318\u030D\u030F\u030D")]
		private static readonly FieldInfo isSustainingField;

		public float Time => (float)float1Field.GetValue(note);
		[WrapperField("\u0310\u0310\u030F\u0310\u0313\u0313\u030F\u0318\u0312\u031A\u030E")]
		private static readonly FieldInfo float1Field;

		public float Length => (float)float2Field.GetValue(note);
		[WrapperField("\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A")]
		private static readonly FieldInfo float2Field;

		public byte NoteMask => (byte)noteMaskField.GetValue(note);
		[WrapperField("\u0318\u0316\u0315\u031A\u0313\u0310\u0316\u030E\u0310\u031A\u0318")]
		private static readonly FieldInfo noteMaskField;

		public uint TickPosition => (uint)tickPositionField.GetValue(note);
		[WrapperField("\u031B\u0310\u0316\u0316\u0314\u0318\u0313\u030E\u0315\u0316\u031C")]
		private static readonly FieldInfo tickPositionField;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note1 => new NoteWrapper(note1Field.GetValue(note));
		[WrapperField("\u0310\u0318\u0319\u0310\u0316\u0316\u0319\u0317\u031C\u0316\u0318")]
		private static readonly FieldInfo note1Field;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note2 => new NoteWrapper(note2Field.GetValue(note));
		[WrapperField("\u030F\u0314\u030D\u0314\u030D\u031B\u0316\u0314\u0316\u0318\u031A")]
		private static readonly FieldInfo note2Field;

		public IEnumerable<NoteWrapper> UnknownNoteThingy => ((IEnumerable)unknownNoteThingyProperty.GetValue(note)).Cast<object>().Select(o => new NoteWrapper(o));
		[WrapperProperty("\u0319\u0315\u0315\u0319\u0313\u0313\u0311\u0313\u0310\u0314\u0317")]
		private static readonly PropertyInfo unknownNoteThingyProperty;

		public bool IsHopo => (bool)isHopoProperty.GetValue(note);
		[WrapperProperty("\u0310\u030F\u030E\u0310\u031A\u0318\u031A\u031A\u0311\u0311\u0313")]
		private static readonly PropertyInfo isHopoProperty;

		public bool IsDisjoint => (bool)isDisjointProperty.GetValue(note);
		[WrapperProperty("\u0311\u031A\u030D\u0315\u030F\u0315\u0310\u0314\u0314\u031A\u0315")]
		private static readonly PropertyInfo isDisjointProperty;

		public bool IsSoloBegin => (bool)isSoloBeginProperty.GetValue(note);
		[WrapperProperty("\u0313\u0310\u031B\u030F\u0311\u0318\u030F\u0313\u0316\u0311\u031C")]
		private static readonly PropertyInfo isSoloBeginProperty;

		public bool IsSoloEnd => (bool)isSoloEndProperty.GetValue(note);
		[WrapperProperty("\u0315\u0313\u031B\u0312\u0312\u0312\u0312\u031B\u0318\u031C\u0319")]
		private static readonly PropertyInfo isSoloEndProperty;

		public bool IsSlave => (bool)isSlaveProperty.GetValue(note);
		[WrapperProperty("\u0316\u031C\u030D\u0318\u0319\u0318\u0316\u0314\u0318\u0313\u0316")]
		private static readonly PropertyInfo isSlaveProperty;

		public bool IsStarPower => (bool)isStarPowerProperty.GetValue(note);
		[WrapperProperty("\u0317\u0311\u031A\u030F\u030D\u0311\u030D\u0314\u030E\u0311\u0314")]
		private static readonly PropertyInfo isStarPowerProperty;

		public bool IsChord => (bool)isChordProperty.GetValue(note);
		[WrapperProperty("\u0317\u031A\u031B\u0316\u031A\u0310\u0313\u0315\u0316\u0318\u0317")]
		private static readonly PropertyInfo isChordProperty;

		public bool IsStarPowerEnd => (bool)isStarPowerEndProperty.GetValue(note);
		[WrapperProperty("\u0318\u0315\u031C\u030F\u031C\u0316\u0310\u0319\u0314\u030E\u030D")]
		private static readonly PropertyInfo isStarPowerEndProperty;

		public bool IsTap => (bool)isTapProperty.GetValue(note);
		[WrapperProperty("\u031A\u030F\u0311\u030F\u031B\u0311\u031C\u0310\u0317\u0313\u0317")]
		private static readonly PropertyInfo isTapProperty;

		public bool IsExtendedSustain => (bool)isExtendedSustainProperty.GetValue(note);
		[WrapperProperty("\u031C\u030D\u0315\u031A\u0318\u0319\u030F\u0313\u031B\u031A\u0310")]
		private static readonly PropertyInfo isExtendedSustainProperty;

		public NoteWrapper(object note) {
			this.note = note;
		}
	}
}
