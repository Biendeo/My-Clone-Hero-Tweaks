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
	[Wrapper(typeof(BassAudioManager))]
	internal struct BassAudioManagerWrapper {
		public readonly BassAudioManager bassAudioManager;

		public BassAudioManagerWrapper(BassAudioManager bassAudioManager) {
			this.bassAudioManager = bassAudioManager;
		}

		#region Fields

		public static BassAudioManagerWrapper instance => new BassAudioManagerWrapper((BassAudioManager)instanceField.GetValue(null));
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public SongEntryWrapper menuSong => new SongEntryWrapper((SongEntry)menuSongField.GetValue(bassAudioManager));
		[WrapperField("\u030F\u030F\u031B\u030D\u0316\u0319\u030F\u0314\u0316\u031A\u0316")]
		private static readonly FieldInfo menuSongField;

		#endregion

	}
}
