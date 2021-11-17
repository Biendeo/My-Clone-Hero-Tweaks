using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper("\u031C\u0314\u030D\u0313\u0311\u0312\u0311\u0319\u0314\u031A\u030F")]
	public struct SoundEffectsManagerWrapper {

		#region Fields

		public static int SomeBool {
			get => (int)someBoolField.GetValue(null);
			set => someBoolField.SetValue(null, value);
		}
		[WrapperField("\u030D\u031C\u0313\u030D\u0313\u031B\u0310\u030F\u031A\u0311\u0313")]
		private static readonly FieldInfo someBoolField;

		#endregion

	}
}
