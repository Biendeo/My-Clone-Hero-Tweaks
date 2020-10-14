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
	[Wrapper(typeof(SongSelect))]
	public struct SongSelectWrapper {
		public SongSelect SongSelect { get; private set; }

		public static SongSelectWrapper Wrap(SongSelect songSelect) => new SongSelectWrapper {
			SongSelect = songSelect
		};

		public override bool Equals(object obj) => SongSelect.Equals(obj);

		public override int GetHashCode() => SongSelect.GetHashCode();

		public bool IsNull() => SongSelect == null;

		#region Constructors

		public static SongSelectWrapper Construct() => new SongSelectWrapper {
			SongSelect = (SongSelect)defaultConstructor.Invoke(Array.Empty<object>())
		};
		[WrapperConstructor]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		#endregion

		#region Methods

		public void ResetIndex() => resetIndexMethod(SongSelect);
		[WrapperMethod("\u0311\u031A\u0318\u0317\u0318\u0314\u031A\u030D\u0313\u030E\u031A")]
		private static readonly FastInvokeHandler resetIndexMethod;

		#endregion
	}
}
