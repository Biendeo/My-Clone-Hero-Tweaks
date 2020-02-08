using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace ExtraSongUI.Wrappers {
	internal class FrameRateWrapper : WrapperBase {
		public readonly FrameRate frameRate;

		public TextMeshProUGUI Text => (TextMeshProUGUI)textField.GetValue(frameRate);
		private static FieldInfo textField;
		private const string textFieldName = "\u0318\u030F\u0319\u0310\u0312\u0310\u030E\u0314\u0310\u031A\u0313";

		public FrameRateWrapper(FrameRate frameRate) {
			this.frameRate = frameRate;
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref textField, typeof(FrameRate), textFieldName);
		}
	}
}
