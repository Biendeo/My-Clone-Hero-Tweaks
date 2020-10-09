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
	[Wrapper(typeof(BassAudioManager))]
	public struct BassAudioManagerWrapper {
		public BassAudioManager BassAudioManager { get; private set; }

		public static BassAudioManagerWrapper Wrap(BassAudioManager bassAudioManager) => new BassAudioManagerWrapper {
			BassAudioManager = bassAudioManager
		};

		public override bool Equals(object obj) => BassAudioManager.Equals(obj);

		public override int GetHashCode() => BassAudioManager.GetHashCode();

		public bool IsNull() => BassAudioManager == null;

		#region Fields

		//TODO: Statics are a little iffy with FieldRef, migrate this when it works.
		public static BassAudioManagerWrapper Instance {
			get => Wrap((BassAudioManager)instanceField.GetValue(null));
			set => instanceField.SetValue(null, value.BassAudioManager);
		}
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public SongEntryWrapper MenuSong {
			get => SongEntryWrapper.Wrap(menuSongField(BassAudioManager));
			set => menuSongField(BassAudioManager) = value.SongEntry;
		}
		[WrapperField("\u030F\u030F\u031B\u030D\u0316\u0319\u030F\u0314\u0316\u031A\u0316")]
		private static readonly AccessTools.FieldRef<BassAudioManager, SongEntry> menuSongField;

        #endregion
    }
}
