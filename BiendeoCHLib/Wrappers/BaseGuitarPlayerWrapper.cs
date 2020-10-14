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

		#region Methods

		public void HitNote(NoteWrapper hitNote) => hitNoteMethod.Invoke(BaseGuitarPlayer, new object[] { hitNote.Note });
		[WrapperMethod("\u0314\u0313\u031C\u0315\u0316\u0318\u0318\u031C\u030D\u0319\u0315")]
		private static readonly FastInvokeHandler hitNoteMethod;

		#endregion
	}
}
