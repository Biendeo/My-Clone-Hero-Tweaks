using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(SongEntry))]
	internal class SongEntryWrapper {
		public readonly SongEntry songEntry;

		public SongEntryWrapper(SongEntry songEntry) {
			this.songEntry = songEntry;
		}

		#region Fields

		#endregion
	}
}
