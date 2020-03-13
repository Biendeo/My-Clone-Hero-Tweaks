using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(SongSelect))]
	internal class SongSelectWrapper {
		public readonly SongSelect songSelect;

		public SongSelectWrapper(SongSelect songSelect) {
			this.songSelect = songSelect;
		}

		#region Fields

		#endregion

		#region Methods

		public void ResetIndex() => resetIndexMethod.Invoke(songSelect, Array.Empty<object>());
		[WrapperMethod("\u0311\u031A\u0318\u0317\u0318\u0314\u031A\u030D\u0313\u030E\u031A")]
		private static readonly MethodInfo resetIndexMethod;

		#endregion
	}
}
