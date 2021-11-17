using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
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

		public bool IsNull() => Note == null;

		#region Constructors

		#endregion

		#region Fields

		public float Time {
			get => timeField(Note);
			set => timeField(Note) = value;
		}
		[WrapperField("\u0310\u0310\u030F\u0310\u0313\u0313\u030F\u0318\u0312\u031A\u030E")]
		private static readonly AccessTools.FieldRef<object, float> timeField;

		public float Length {
			get => lengthField(Note);
			set => lengthField(Note) = value;
		}
		[WrapperField("\u031C\u031C\u0312\u0319\u0314\u0312\u0317\u031C\u031C\u031C\u031A")]
		private static readonly AccessTools.FieldRef<object, float> lengthField;

		public byte NoteMask {
			get => noteMaskField(Note);
			set => noteMaskField(Note) = value;
		}
		[WrapperField("\u0318\u0316\u0315\u031A\u0313\u0310\u0316\u030E\u0310\u031A\u0318")]
		private static readonly AccessTools.FieldRef<object, byte> noteMaskField;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note1 {
			get => Wrap(note1Field(Note));
			set => note1Field(Note) = value.Note;
		}
		[WrapperField("\u0310\u0318\u0319\u0310\u0316\u0316\u0319\u0317\u031C\u0316\u0318")]
		private static readonly AccessTools.FieldRef<object, object> note1Field;

		// Seems to be always a null field for me 🤷‍
		public NoteWrapper Note2 {
			get => Wrap(note2Field(Note));
			set => note2Field(Note) = value.Note;
		}
		[WrapperField("\u030F\u0314\u030D\u0314\u030D\u031B\u0316\u0314\u0316\u0318\u031A")]
		private static readonly AccessTools.FieldRef<object, object> note2Field;

		public bool WasHit {
			get => wasHitField(Note);
			set => wasHitField(Note) = value;
		}
		[WrapperField("\u031B\u030F\u0318\u030F\u0312\u0315\u031B\u0310\u0310\u0314\u0315")]
		private static readonly AccessTools.FieldRef<object, bool> wasHitField;

		public bool WasMissed {
			get => wasMissedField(Note);
			set => wasMissedField(Note) = value;
		}
		[WrapperField("\u0312\u030E\u0319\u030F\u0310\u0314\u0312\u0311\u0319\u0316\u0312")]
		private static readonly AccessTools.FieldRef<object, bool> wasMissedField;

		public bool IsSustaining {
			get => isSustainingField(Note);
			set => isSustainingField(Note) = value;
		}
		[WrapperField("\u0316\u031C\u030E\u031A\u0316\u0314\u0316\u0318\u030D\u030F\u030D")]
		private static readonly AccessTools.FieldRef<object, bool> isSustainingField;

		public MoonNoteWrapper.NoteType NoteType {
			get => (MoonNoteWrapper.NoteType)noteTypeField(Note);
			set => noteTypeField(Note) = (int)value;
		}
		[WrapperField("\u030F\u031A\u031B\u031B\u031A\u031A\u031C\u0310\u0315\u030E\u0319")]
		private static readonly AccessTools.FieldRef<object, int> noteTypeField;

		public NoteFlags Flags {
			get => (NoteFlags)flagsField(Note);
			set => flagsField(Note) = (byte)value;
		}
		[WrapperField("\u030E\u031B\u0316\u0314\u031C\u0311\u031C\u030D\u0312\u0317\u0316")]
		private static readonly AccessTools.FieldRef<object, byte> flagsField;

		public uint TickPosition {
			get => tickPositionField(Note);
			set => tickPositionField(Note) = value;
		}
		[WrapperField("\u031B\u0310\u0316\u0316\u0314\u0318\u0313\u030E\u0315\u0316\u031C")]
		private static readonly AccessTools.FieldRef<object, uint> tickPositionField;

		public int TickLength {
			get => tickLengthField(Note);
			set => tickLengthField(Note) = value;
		}
		[WrapperField("\u030F\u031A\u030F\u0314\u030F\u0318\u0312\u031C\u0317\u0316\u0317")]
		private static readonly AccessTools.FieldRef<object, int> tickLengthField;

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
		[WrapperEnum("PLACEHOLDER")]
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
		[WrapperEnum("PLACEHOLDER")]
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
		[WrapperEnum("PLACEHOLDER")]
		public enum SomeEnum {
			Lane1 = 1,
			Lane2 = 2,
			Lane3 = 4,
			OpenLane = 8
		}

		[Flags]
		[WrapperEnum("PLACEHOLDER")]
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
