using Common.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.UI;

namespace Common.Wrappers {
	[Wrapper(typeof(FadeBehaviour))]
	internal struct FadeBehaviourWrapper {
		public FadeBehaviour fadeBehaviour;

		public FadeBehaviourWrapper(FadeBehaviour fadeBehaviour) {
			this.fadeBehaviour = fadeBehaviour;
		}

		#region Fields

		public static FadeBehaviourWrapper instance => new FadeBehaviourWrapper((FadeBehaviour)instanceField.GetValue(null));
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public float timeToFade => (float)timeToFadeField.GetValue(fadeBehaviour);
		[WrapperField("\u030F\u031B\u0314\u0311\u0310\u0313\u0314\u0314\u0317\u0318\u0312")]
		private static readonly FieldInfo timeToFadeField; //? Always 0.4?

		public bool isFadingOut => (bool)isFadingOutField.GetValue(fadeBehaviour);
		[WrapperField("\u0311\u0310\u031B\u0312\u0315\u030D\u030D\u031B\u0315\u031C\u0314")]
		private static readonly FieldInfo isFadingOutField;

		public Image fadeGraphic => (Image)fadeGraphicField.GetValue(fadeBehaviour);
		[WrapperField("fadeGraphic")]
		private static readonly FieldInfo fadeGraphicField;

		#endregion

		#region Methods

		public IEnumerator InvokeSceneChange(string sceneName) => (IEnumerator)invokeSceneChangeMethod.Invoke(fadeBehaviour, new object[] { sceneName });
		[WrapperMethod("\u0318\u031C\u0315\u0318\u030E\u031B\u0315\u0310\u030D\u0314\u0314")]
		private static readonly MethodInfo invokeSceneChangeMethod;

		#endregion
	}
}
