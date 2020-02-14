using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Wrappers {
	[Wrapper(typeof(SoloCounter))]
	internal class SoloCounterWrapper {
		public readonly SoloCounter soloCounter;

		public GameManager GameManager => (GameManager)gameManagerField.GetValue(soloCounter);
		[WrapperField("\u030D\u0317\u0319\u031A\u031A\u030D\u0316\u030D\u0310\u030E\u0313")]
		private static readonly FieldInfo gameManagerField;

		public bool AnimateText => (bool)animateTextField.GetValue(soloCounter);
		[WrapperField("\u030D\u0311\u031B\u0312\u0319\u0317\u0315\u031C\u031B\u0319\u031C")]
		private static readonly FieldInfo animateTextField;

		public bool Bool2 => (bool)bool2Field.GetValue(soloCounter);
		[WrapperField("\u030F\u0316\u0317\u031C\u0315\u031B\u031A\u0313\u030F\u0311\u0310")]
		private static readonly FieldInfo bool2Field;

		public bool Bool3 => (bool)bool3Field.GetValue(soloCounter);
		[WrapperField("\u031A\u031A\u030D\u0315\u0317\u0318\u0319\u0317\u0319\u031C\u0317")]
		private static readonly FieldInfo bool3Field;

		public SoloCounterWrapper(SoloCounter soloCounter) {
			this.soloCounter = soloCounter;
		}
	}
}
