using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper(typeof(PracticeUI))]
	internal class PracticeUIWrapper {
		public readonly PracticeUI practiceUI;

		public float SomeFloat => (float)someFloatField.GetValue(this);
		[WrapperField("\u030E\u0316\u030F\u0314\u030E\u0312\u0315\u0317\u0315\u031B\u0316")]
		private static readonly FieldInfo someFloatField;

		public PracticeUIWrapper(PracticeUI practiceUI) {
			this.practiceUI = practiceUI;
		}
	}
}
