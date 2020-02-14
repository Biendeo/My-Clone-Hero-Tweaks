using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Common.Wrappers {
	[Wrapper(typeof(FrameRate))]
	internal class FrameRateWrapper {
		public readonly FrameRate frameRate;

		public TextMeshProUGUI Text => (TextMeshProUGUI)textField.GetValue(frameRate);
		[WrapperField("\u0318\u030F\u0319\u0310\u0312\u0310\u030E\u0314\u0310\u031A\u0313")]
		private static readonly FieldInfo textField;

		public FrameRateWrapper(FrameRate frameRate) {
			this.frameRate = frameRate;
		}
	}
}
