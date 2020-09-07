using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u0310\u030F\u030F\u0311\u0319\u0317\u0317\u0313\u031B\u0311\u0312")]
	public struct SongEntryPropertyWrapper {
		public object SongEntryProperty { get; private set; }

		public static SongEntryPropertyWrapper Wrap(object songEntryProperty) => new SongEntryPropertyWrapper {
			SongEntryProperty = songEntryProperty
		};

		public override bool Equals(object obj) => SongEntryProperty.Equals(obj);

		public override int GetHashCode() => SongEntryProperty.GetHashCode();

		public bool IsNull() => SongEntryProperty == null;

		#region Fields

		#endregion

		#region Properties

		public string ValueLowerCase => (string)valueLowerCaseProperty.GetValue(SongEntryProperty);
		[WrapperProperty("\u031A\u030D\u0319\u030F\u031A\u0310\u030F\u031B\u0316\u031A\u0319")]
		private static readonly PropertyInfo valueLowerCaseProperty;

		#endregion

	}
}
