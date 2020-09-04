using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u030E\u0314\u030E\u0311\u0311\u0314\u030D\u030F\u0314\u0315\u0310")]
	public struct NoteWrapper {
		public object Note { get; private set; }

		public static NoteWrapper Wrap(object note) => new NoteWrapper {
			Note = note
		};

		public override bool Equals(object obj) => Note.Equals(obj);

		public override int GetHashCode() => Note.GetHashCode();

		#region Constructors

		#endregion

		#region Fields

		public float Time {
			get => (float)timeField.GetValue(Note);
			set => timeField.SetValue(Note, value);
		}
		[WrapperField("\u0310\u0310\u030F\u0310\u0313\u0313\u030F\u0318\u0312\u031A\u030E")]
		private static readonly FieldInfo timeField;

		public float Length {
			get => (float)lengthField.GetValue(Note);
			set => lengthField.SetValue(Note, value);
		}
		[WrapperField("\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A")]
		private static readonly FieldInfo lengthField;

		public byte NoteMask {
			get => (byte)noteMaskField.GetValue(Note);
			set => noteMaskField.SetValue(Note, value);
		}
		[WrapperField("\u0318\u0316\u0315\u031A\u0313\u0310\u0316\u030E\u0310\u031A\u0318")]
		private static readonly FieldInfo noteMaskField;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note1 {
			get => NoteWrapper.Wrap(note1Field.GetValue(Note));
			set => note1Field.SetValue(Note, value.Note);
		}
		[WrapperField("\u0310\u0318\u0319\u0310\u0316\u0316\u0319\u0317\u031C\u0316\u0318")]
		private static readonly FieldInfo note1Field;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note2 {
			get => NoteWrapper.Wrap(note2Field.GetValue(Note));
			set => note2Field.SetValue(Note, value.Note);
		}
		[WrapperField("\u030F\u0314\u030D\u0314\u030D\u031B\u0316\u0314\u0316\u0318\u031A")]
		private static readonly FieldInfo note2Field;

		public bool WasHit {
			get => (bool)wasHitField.GetValue(Note);
			set => wasHitField.SetValue(Note, value);
		}
		[WrapperField("\u031B\u030F\u0318\u030F\u0312\u0315\u031B\u0310\u0310\u0314\u0315")]
		private static readonly FieldInfo wasHitField;

		public bool WasMissed {
			get => (bool)wasMissedField.GetValue(Note);
			set => wasMissedField.SetValue(Note, value);
		}
		[WrapperField("\u0312\u030E\u0319\u030F\u0310\u0314\u0312\u0311\u0319\u0316\u0312")]
		private static readonly FieldInfo wasMissedField;

		public bool IsSustaining {
			get => (bool)isSustainingField.GetValue(Note);
			set => isSustainingField.SetValue(Note, value);
		}
		[WrapperField("\u0316\u031C\u030E\u031A\u0316\u0314\u0316\u0318\u030D\u030F\u030D")]
		private static readonly FieldInfo isSustainingField;

		public MoonNoteWrapper.NoteType NoteType {
			get => (MoonNoteWrapper.NoteType)noteTypeField.GetValue(Note);
			set => noteTypeField.SetValue(Note, value);
		}
		[WrapperField("\u030F\u031A\u031B\u031B\u031A\u031A\u031C\u0310\u0315\u030E\u0319")]
		private static readonly FieldInfo noteTypeField;

		public NoteFlags Flags {
			get => (NoteFlags)flagsField.GetValue(Note);
			set => flagsField.SetValue(Note, value);
		}
		[WrapperField("\u030E\u031B\u0316\u0314\u031C\u0311\u031C\u030D\u0312\u0317\u0316")]
		private static readonly FieldInfo flagsField;

		public uint TickPosition {
			get => (uint)tickPositionField.GetValue(Note);
			set => tickPositionField.SetValue(Note, value);
		}
		[WrapperField("\u031B\u0310\u0316\u0316\u0314\u0318\u0313\u030E\u0315\u0316\u031C")]
		private static readonly FieldInfo tickPositionField;

		public int TickLength {
			get => (int)tickLengthField.GetValue(Note);
			set => tickLengthField.SetValue(Note, value);
		}
		[WrapperField("\u030F\u031A\u030F\u0314\u030F\u0318\u0312\u031C\u0317\u0316\u0317")]
		private static readonly FieldInfo tickLengthField;

		#endregion

		#region Properties

		public bool IsChord => (bool)isChordProperty.GetValue(Note);
		[WrapperProperty("\u0317\u031A\u031B\u0316\u031A\u0310\u0313\u0315\u0316\u0318\u0317")]
		private static readonly PropertyInfo isChordProperty;

		public bool IsStarPower => (bool)isStarPowerProperty.GetValue(Note);
		[WrapperProperty("\u0317\u0311\u031A\u030F\u030D\u0311\u030D\u0314\u030E\u0311\u0314")]
		private static readonly PropertyInfo isStarPowerProperty;

		public bool IsStarPowerEnd => (bool)isStarPowerEndProperty.GetValue(Note);
		[WrapperProperty("\u0318\u0315\u031C\u030F\u031C\u0316\u0310\u0319\u0314\u030E\u030D")]
		private static readonly PropertyInfo isStarPowerEndProperty;

		public bool IsExtendedSustain => (bool)isExtendedSustainProperty.GetValue(Note);
		[WrapperProperty("\u031C\u030D\u0315\u031A\u0318\u0319\u030F\u0313\u031B\u031A\u0310")]
		private static readonly PropertyInfo isExtendedSustainProperty;

		public bool IsHOPO => (bool)isHopoProperty.GetValue(Note);
		[WrapperProperty("\u0310\u030F\u030E\u0310\u031A\u0318\u031A\u031A\u0311\u0311\u0313")]
		private static readonly PropertyInfo isHopoProperty;

		public bool IsTap => (bool)isTapProperty.GetValue(Note);
		[WrapperProperty("\u031A\u030F\u0311\u030F\u031B\u0311\u031C\u0310\u0317\u0313\u0317")]
		private static readonly PropertyInfo isTapProperty;

		public bool IsSoloBegin => (bool)isSoloBeginProperty.GetValue(Note);
		[WrapperProperty("\u0313\u0310\u031B\u030F\u0311\u0318\u030F\u0313\u0316\u0311\u031C")]
		private static readonly PropertyInfo isSoloBeginProperty;

		public bool IsSoloEnd => (bool)isSoloEndProperty.GetValue(Note);
		[WrapperProperty("\u0315\u0313\u031B\u0312\u0312\u0312\u0312\u031B\u0318\u031C\u0319")]
		private static readonly PropertyInfo isSoloEndProperty;

		public bool IsDisjoint => (bool)isDisjointProperty.GetValue(Note);
		[WrapperProperty("\u0311\u031A\u030D\u0315\u030F\u0315\u0310\u0314\u0314\u031A\u0315")]
		private static readonly PropertyInfo isDisjointProperty;

		public bool IsSlave => (bool)isSlaveProperty.GetValue(Note);
		[WrapperProperty("\u0316\u031C\u030D\u0318\u0319\u0318\u0316\u0314\u0318\u0313\u0316")]
		private static readonly PropertyInfo isSlaveProperty;

		public IEnumerable<NoteWrapper> UnknownNoteThingy => ((IEnumerable)unknownNoteThingyProperty.GetValue(Note)).Cast<object>().Select(o => NoteWrapper.Wrap(o));
		[WrapperProperty("\u0319\u0315\u0315\u0319\u0313\u0313\u0311\u0313\u0310\u0314\u0317")]
		private static readonly PropertyInfo unknownNoteThingyProperty;

		//? Seems to match the value of Tick Position, so probably an automatic field.
		public uint UnknownUintThingy {
			get => (uint)unknownUintThingyProperty.GetValue(Note);
			set => unknownUintThingyProperty.SetValue(Note, value);
		}
		[WrapperProperty("\u0312\u0315\u0310\u0315\u0317\u0316\u0316\u031A\u031A\u031B\u0310")]
		private static readonly PropertyInfo unknownUintThingyProperty;

		//? Seems to match the value of Note Mask, so probably an automatic field.
		public byte UnknownByteThingy => (byte)unknownByteThingyProperty.GetValue(Note);
		[WrapperProperty("\u0314\u031A\u0313\u030D\u031B\u0314\u0311\u031A\u0318\u030D\u0315")]
		private static readonly PropertyInfo unknownByteThingyProperty;

		//? Seems to match the value of Time, so probably an automatic field.
		public float UnknownFloatThingy {
			get => (float)unknownFloatThingyProperty.GetValue(Note);
			set => unknownFloatThingyProperty.SetValue(Note, value);
		}
		[WrapperProperty("\u031B\u0314\u0311\u0316\u031C\u0316\u0310\u030E\u0312\u0317\u0311")]
		private static readonly PropertyInfo unknownFloatThingyProperty;

		#endregion

		#region Methods

		#endregion

		#region Enumerations

		[Flags]
		public enum NoteFlags : byte {
			None = 0,
			Chord = 1,
			Disjoint = 2,
			Slave = 4,
			ExtendedSustain = 8,
			StarPower = 16,
			StarPowerEnd = 32,
			SoloBegin = 64,
			SoloEnd = 128
		}

		[Flags]
		public enum GHL_NoteType {
			W1 = 1,
			W2 = 2,
			W3 = 4,
			B1 = 8,
			B2 = 16,
			Open = 32,
			B3 = 64
		}

		[Flags]
		public enum SomeEnum {
			Lane1 = 1,
			Lane2 = 2,
			Lane3 = 4,
			OpenLane = 8
		}

		[Flags]
		public enum Modifier {
			None = 1,
			AllStrums = 2,
			AllHOPOs = 4,
			AllTaps = 8,
			AllOpens = 16,
			MirrorMode = 32,
			Shuffle = 64,
			HOPOsToTaps = 128
		}

		#endregion
	}
}
