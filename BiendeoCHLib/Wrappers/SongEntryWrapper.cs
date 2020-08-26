using BiendeoCHLib.Wrappers.Attributes;
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
		public object SongEntry { get; private set; }

		public static SongEntryWrapper Wrap(SongEntry songEntry) => new SongEntryWrapper {
			SongEntry = songEntry
		};

		#region Fields

		#endregion

		#region Properties

		public SongEntryPropertyWrapper Artist => new SongEntryPropertyWrapper(artistProperty.GetValue(SongEntry));
		[WrapperProperty("Artist")]
		private static readonly PropertyInfo artistProperty;

		#endregion

	}
}
