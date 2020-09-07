using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(SoloCounter))]
	public struct SoloCounterWrapper {
		public SoloCounter SoloCounter { get; private set; }

		public static SoloCounterWrapper Wrap(SoloCounter soloCounter) => new SoloCounterWrapper {
			SoloCounter = soloCounter
		};

		public override bool Equals(object obj) => SoloCounter.Equals(obj);

		public override int GetHashCode() => SoloCounter.GetHashCode();

		public bool IsNull() => SoloCounter == null;

		#region Fields

		public GameManagerWrapper GameManager {
			get => GameManagerWrapper.Wrap(gameManagerField(SoloCounter));
			set => gameManagerField(SoloCounter) = value.GameManager;
		}
		[WrapperField("\u030D\u0317\u0319\u031A\u031A\u030D\u0316\u030D\u0310\u030E\u0313")]
		private static readonly AccessTools.FieldRef<SoloCounter, GameManager> gameManagerField;

		public bool AnimateText {
			get => animateTextField(SoloCounter);
			set => animateTextField(SoloCounter) = value;
		}
		[WrapperField("\u030D\u0311\u031B\u0312\u0319\u0317\u0315\u031C\u031B\u0319\u031C")]
		private static readonly AccessTools.FieldRef<SoloCounter, bool> animateTextField;

		public bool Bool2 {
			get => bool2Field(SoloCounter);
			set => bool2Field(SoloCounter) = value;
		}
		[WrapperField("\u030F\u0316\u0317\u031C\u0315\u031B\u031A\u0313\u030F\u0311\u0310")]
		private static readonly AccessTools.FieldRef<SoloCounter, bool> bool2Field;

		public bool Bool3 {
			get => bool3Field(SoloCounter);
			set => bool3Field(SoloCounter) = value;
		}
		[WrapperField("\u031A\u031A\u030D\u0315\u0317\u0318\u0319\u0317\u0319\u031C\u0317")]
		private static readonly AccessTools.FieldRef<SoloCounter, bool> bool3Field;

		#endregion
	}
}
