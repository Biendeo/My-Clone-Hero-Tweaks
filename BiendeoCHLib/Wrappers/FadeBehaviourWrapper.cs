using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.UI;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(FadeBehaviour))]
	public struct FadeBehaviourWrapper {
		public FadeBehaviour FadeBehaviour { get; private set; }

		public static FadeBehaviourWrapper Wrap(FadeBehaviour fadeBehaviour) => new FadeBehaviourWrapper {
			FadeBehaviour = fadeBehaviour
		};

		public override bool Equals(object obj) => FadeBehaviour.Equals(obj);

		public override int GetHashCode() => FadeBehaviour.GetHashCode();

		public bool IsNull() => FadeBehaviour == null;

		#region Fields

		public static FadeBehaviourWrapper Instance {
			get => Wrap((FadeBehaviour)instanceField.GetValue(null));
			set => instanceField.SetValue(null, value.FadeBehaviour);
		}
		[WrapperField("\u0312\u0313\u0310\u0315\u030E\u0319\u030D\u0318\u0313\u030E\u031A")]
		private static readonly FieldInfo instanceField;

		public float TimeToFade {
			get => timeToFadeField(FadeBehaviour);
			set => timeToFadeField(FadeBehaviour) = value;
		}
		[WrapperField("\u030F\u031B\u0314\u0311\u0310\u0313\u0314\u0314\u0317\u0318\u0312")]
		private static readonly AccessTools.FieldRef<FadeBehaviour, float> timeToFadeField; //? Always 0.4?

		public bool IsFadingOut {
			get => isFadingOutField(FadeBehaviour);
			set => isFadingOutField(FadeBehaviour) = value;
		}
		[WrapperField("\u0311\u0310\u031B\u0312\u0315\u030D\u030D\u031B\u0315\u031C\u0314")]
		private static readonly AccessTools.FieldRef<FadeBehaviour, bool> isFadingOutField;

		public Image FadeGraphic {
			get => fadeGraphicField(FadeBehaviour);
			set => fadeGraphicField(FadeBehaviour) = value;
		}
		[WrapperField("fadeGraphic")]
		private static readonly AccessTools.FieldRef<FadeBehaviour, Image> fadeGraphicField;

		#endregion

		#region Methods

		public IEnumerator InvokeSceneChange(string sceneName) => (IEnumerator)invokeSceneChangeMethod(FadeBehaviour, sceneName);
		[WrapperMethod("\u0318\u031C\u0315\u0318\u030E\u031B\u0315\u0310\u030D\u0314\u0314")]
		private static readonly FastInvokeHandler invokeSceneChangeMethod;

		#endregion
	}
}
