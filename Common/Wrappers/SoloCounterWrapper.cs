using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers {
	internal class SoloCounterWrapper : WrapperBase {
		public readonly SoloCounter soloCounter;

		public GameManager GameManager => (GameManager)gameManagerField.GetValue(soloCounter);
		private static FieldInfo gameManagerField;
		private const string gameManagerFieldName = "\u030D\u0317\u0319\u031A\u031A\u030D\u0316\u030D\u0310\u030E\u0313";

		public bool AnimateText => (bool)animateTextField.GetValue(soloCounter);
		private static FieldInfo animateTextField;
		private const string animateTextFieldName = "\u030D\u0311\u031B\u0312\u0319\u0317\u0315\u031C\u031B\u0319\u031C";

		public bool Bool2 => (bool)bool2Field.GetValue(soloCounter);
		private static FieldInfo bool2Field;
		private const string bool2FieldName = "\u030F\u0316\u0317\u031C\u0315\u031B\u031A\u0313\u030F\u0311\u0310";

		public bool Bool3 => (bool)bool3Field.GetValue(soloCounter);
		private static FieldInfo bool3Field;
		private const string bool3FieldName = "\u031A\u031A\u030D\u0315\u0317\u0318\u0319\u0317\u0319\u031C\u0317";

		public SoloCounterWrapper(SoloCounter soloCounter) {
			this.soloCounter = soloCounter;
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref gameManagerField, typeof(SoloCounter), gameManagerFieldName);
			RegisterField(ref animateTextField, typeof(SoloCounter), animateTextFieldName);
			RegisterField(ref bool2Field, typeof(SoloCounter), bool2FieldName);
			RegisterField(ref bool3Field, typeof(SoloCounter), bool3FieldName);
		}
	}
}
