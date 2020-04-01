using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper(typeof(SPBar))]
	internal class SPBarWrapper {
		public readonly SPBar spBar;

		public SPBarWrapper(SPBar spBar) {
			this.spBar = spBar;
		}

		#region Fields

		public float someFloat => (float)someFloatField.GetValue(spBar);
		[WrapperField("\u0319\u031C\u031A\u030E\u0314\u030E\u0312\u0311\u0317\u0318\u0316")]
		private static readonly FieldInfo someFloatField;

		#endregion
	}
}
