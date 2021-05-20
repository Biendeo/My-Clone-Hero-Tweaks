using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(SongEntry))]
	public struct SongEntryWrapper {
		public SongEntry SongEntry { get; private set; }

		public static SongEntryWrapper Wrap(SongEntry songEntry) => new SongEntryWrapper {
			SongEntry = songEntry
		};

		#region Fields

		#endregion

		#region Properties

		public SongEntryPropertyWrapper Artist => SongEntryPropertyWrapper.Wrap(artistProperty.GetValue(SongEntry));
		[WrapperProperty("Artist")]
		private static readonly PropertyInfo artistProperty;

		#endregion

		#region Methods

		public bool ReadMetadataForSong() => (bool)readMetadataForSongMethod(SongEntry);
		[WrapperMethod("\u0319\u031B\u0312\u0317\u0316\u0319\u0316\u0318\u030F\u0314\u0314")]
		private static readonly FastInvokeHandler readMetadataForSongMethod;

		public SongWrapper GetSongObject(bool someBool = false) => SongWrapper.Wrap(getSongObjectMethod(SongEntry, someBool));
		[WrapperMethod("\u0310\u0314\u0316\u0318\u031A\u0317\u0312\u0311\u0315\u0310\u0311")]
		private static readonly FastInvokeHandler getSongObjectMethod;


		#endregion

	}
}
