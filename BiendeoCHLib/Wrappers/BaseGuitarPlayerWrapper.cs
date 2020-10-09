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
	[Wrapper(typeof(BaseGuitarPlayer))]
	public struct BaseGuitarPlayerWrapper {
		public BaseGuitarPlayer BaseGuitarPlayer { get; private set; }

		public static BaseGuitarPlayerWrapper Wrap(BaseGuitarPlayer baseGuitarPlayer) => new BaseGuitarPlayerWrapper {
			BaseGuitarPlayer = baseGuitarPlayer
		};

		public override bool Equals(object obj) => BaseGuitarPlayer.Equals(obj);

		public override int GetHashCode() => BaseGuitarPlayer.GetHashCode();

		public bool IsNull() => BaseGuitarPlayer == null;

		#region Casts

		public BasePlayerWrapper CastToBasePlayer() => BasePlayerWrapper.Wrap(BaseGuitarPlayer);

		#endregion

		#region Fields

		public float WhammyTimer
		{
			get => whammyTimerField(BaseGuitarPlayer);
			set => whammyTimerField(BaseGuitarPlayer) = value;
		}
		[WrapperField("\u0318\u030D\u0311\u0316\u0319\u0316\u0319\u030D\u0316\u0313\u0311")]
		private static readonly AccessTools.FieldRef<object, float> whammyTimerField;

		public float CurrentWhammy
		{
			get => currentWhammyField(BaseGuitarPlayer);
			set => currentWhammyField(BaseGuitarPlayer) = value;
		}
		[WrapperField("\u030D\u031A\u031A\u031B\u0311\u030F\u031B\u0319\u030F\u0319\u030D")]
		private static readonly AccessTools.FieldRef<object, float> currentWhammyField;

		public float StrumLenienceTimer
		{
			get => strumLenienceTimerField(BaseGuitarPlayer);
			set => strumLenienceTimerField(BaseGuitarPlayer) = value;
		}
		[WrapperField("\u031A\u0315\u0313\u0317\u030D\u0311\u0317\u030E\u0311\u030D\u0310")]
		private static readonly AccessTools.FieldRef<object, float> strumLenienceTimerField;

		public float HopoLenienceTimer
		{
			get => hopoLenienceTimerField(BaseGuitarPlayer);
			set => hopoLenienceTimerField(BaseGuitarPlayer) = value;
		}
		[WrapperField("\u031A\u030E\u0315\u0319\u030F\u0310\u030F\u0312\u0315\u0315\u0318")]
		private static readonly AccessTools.FieldRef<object, float> hopoLenienceTimerField;

		public bool WasHOPOStrummed
		{
			get => hopoStrummedField(BaseGuitarPlayer);
			set => hopoStrummedField(BaseGuitarPlayer) = value;
		}
		[WrapperField("\u0311\u0311\u0314\u0313\u031B\u030D\u0310\u031B\u030D\u031A\u0314")]
		private static readonly AccessTools.FieldRef<object, bool> hopoStrummedField;

		#endregion

		#region Methods

		public void Update() => updateMethod.Invoke(BaseGuitarPlayer, null);
		[WrapperMethod("Update")]
		private static readonly FastInvokeHandler updateMethod;

		public void HitNote(NoteWrapper hitNote) => hitNoteMethod.Invoke(BaseGuitarPlayer, new object[] { hitNote.Note });
		[WrapperMethod("\u0314\u0313\u031C\u0315\u0316\u0318\u0318\u031C\u030D\u0319\u0315")]
		private static readonly FastInvokeHandler hitNoteMethod;

		public bool WasNoteHit(NoteWrapper note) => (bool)wasNoteHitMethod.Invoke(BaseGuitarPlayer, new object[] { note.Note });
		[WrapperMethod("\u031B\u0319\u031B\u0318\u0319\u0315\u031A\u0314\u0315\u030D\u0311")]
		private static readonly FastInvokeHandler wasNoteHitMethod;

		public void OverStrum(bool strummed) => overstrumMethod.Invoke(BaseGuitarPlayer, new object[] { strummed });
		[WrapperMethod("\u0311\u0314\u0311\u031B\u030D\u0310\u031B\u0318\u0316\u030D\u0312")]
		private static readonly FastInvokeHandler overstrumMethod;

		public void CheckForHitNotes() => checkForHitNotesMethod.Invoke(BaseGuitarPlayer, null);
		[WrapperMethod("\u0316\u0314\u030E\u0318\u0314\u0312\u030D\u0315\u030D\u0311\u030F")]
		private static readonly FastInvokeHandler checkForHitNotesMethod;

		public void UpdateSustains() => updateSustainsMethod.Invoke(BaseGuitarPlayer, null);
		[WrapperMethod("\u0319\u0313\u0319\u030E\u0318\u0315\u030D\u0317\u0317\u0315\u0313")]
		private static readonly FastInvokeHandler updateSustainsMethod;

		#endregion
	}
}
