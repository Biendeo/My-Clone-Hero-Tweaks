using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(HealthContainer))]
	public struct HealthContainerWrapper {
		public HealthContainer HealthContainer { get; private set; }

		public static HealthContainerWrapper Wrap(HealthContainer healthContainer) => new HealthContainerWrapper {
			HealthContainer = healthContainer
		};

		public override bool Equals(object obj) => HealthContainer.Equals(obj);

		public override int GetHashCode() => HealthContainer.GetHashCode();

		public bool IsNull() => HealthContainer == null;

		#region Casts

		public MonoBehaviour CastToMonoBehaviour() => HealthContainer;

		#endregion

		#region Fields

		public GameObject RedBar {
			get => redBarField(HealthContainer);
			set => redBarField(HealthContainer) = value;
		}
		[WrapperField("\u031C\u0312\u0315\u030F\u0317\u0316\u031C\u0310\u0319\u0311\u0319")]
		private static readonly AccessTools.FieldRef<object, GameObject> redBarField;

		public GameObject YellowBar {
			get => yellowBarField(HealthContainer);
			set => yellowBarField(HealthContainer) = value;
		}
		[WrapperField("\u0319\u030D\u0318\u0319\u0316\u031A\u031A\u030E\u0312\u031B\u0313")]
		private static readonly AccessTools.FieldRef<object, GameObject> yellowBarField;

		public GameObject GreenBar {
			get => greenBarField(HealthContainer);
			set => greenBarField(HealthContainer) = value;
		}
		[WrapperField("\u0319\u0314\u031A\u030D\u0317\u031B\u0318\u031C\u0317\u031C\u0313")]
		private static readonly AccessTools.FieldRef<object, GameObject> greenBarField;

		public Transform ArrowTransform {
			get => arrowTransformField(HealthContainer);
			set => arrowTransformField(HealthContainer) = value;
		}
		[WrapperField("\u031A\u0311\u0314\u0311\u0318\u0312\u0316\u0310\u0313\u0319\u031B")]
		private static readonly AccessTools.FieldRef<object, Transform> arrowTransformField;

		public SpriteRenderer GlowRenderer {
			get => glowRendererField(HealthContainer);
			set => glowRendererField(HealthContainer) = value;
		}
		[WrapperField("\u0318\u0314\u0315\u0310\u0317\u0311\u030D\u030F\u0317\u0315\u030F")]
		private static readonly AccessTools.FieldRef<object, SpriteRenderer> glowRendererField;

		// Set to -2.15f, no use.
		public float ArrowEnd {
			get => arrowEndField(HealthContainer);
			set => arrowEndField(HealthContainer) = value;
		}
		[WrapperField("\u031B\u031B\u031B\u031B\u0312\u0315\u031B\u0316\u031C\u0318\u0310")]
		private static readonly AccessTools.FieldRef<object, float> arrowEndField;

		// Set to -1.57f, no use.
		public float ArrowThird {
			get => arrowThirdField(HealthContainer);
			set => arrowThirdField(HealthContainer) = value;
		}
		[WrapperField("\u0312\u031A\u030E\u0310\u0310\u0318\u0313\u031C\u0318\u0313\u031A")]
		private static readonly AccessTools.FieldRef<object, float> arrowThirdField;

		// Set to -1f, no use.
		public float ArrowSecond {
			get => arrowSecondField(HealthContainer);
			set => arrowSecondField(HealthContainer) = value;
		}
		[WrapperField("\u0315\u0313\u030E\u031B\u0316\u0319\u031A\u0316\u0313\u0313\u031C")]
		private static readonly AccessTools.FieldRef<object, float> arrowSecondField;

		// Set to -0.55f, no use.
		public float ArrowTop {
			get => arrowTopField(HealthContainer);
			set => arrowTopField(HealthContainer) = value;
		}
		[WrapperField("\u0319\u031B\u0314\u0312\u0316\u0314\u031B\u0315\u0318\u031B\u0313")]
		private static readonly AccessTools.FieldRef<object, float> arrowTopField;

		public float LastHealth {
			get => lastHealthField(HealthContainer);
			set => lastHealthField(HealthContainer) = value;
		}
		[WrapperField("\u0313\u0312\u0314\u030D\u031B\u0315\u030E\u0310\u0317\u030E\u0315")]
		private static readonly AccessTools.FieldRef<object, float> lastHealthField;

		// 0.33f in Start.
		public float RedYellowThreshold {
			get => redYellowThresholdField(HealthContainer);
			set => redYellowThresholdField(HealthContainer) = value;
		}
		[WrapperField("\u0315\u0318\u031B\u030D\u0311\u0312\u0315\u031C\u031A\u0318\u0316")]
		private static readonly AccessTools.FieldRef<object, float> redYellowThresholdField;

		// 0.72f in Start.
		public float YellowGreenThreshold {
			get => yellowGreenThresholdField(HealthContainer);
			set => yellowGreenThresholdField(HealthContainer) = value;
		}
		[WrapperField("\u0311\u0317\u0317\u0319\u031A\u031A\u031C\u030E\u0310\u030E\u0314")]
		private static readonly AccessTools.FieldRef<object, float> yellowGreenThresholdField;

		public Vector3 ArrowPosition {
			get => arrowPositionField(HealthContainer);
			set => arrowPositionField(HealthContainer) = value;
		}
		[WrapperField("\u0310\u030F\u031A\u0315\u0318\u0314\u030D\u031A\u0316\u0317\u0311")]
		private static readonly AccessTools.FieldRef<object, Vector3> arrowPositionField;

		public Color GreenGlow {
			get => greenGlowField(HealthContainer);
			set => greenGlowField(HealthContainer) = value;
		}
		[WrapperField("\u031B\u0317\u0318\u0311\u0312\u030D\u0316\u0311\u030D\u0315\u031A")]
		private static readonly AccessTools.FieldRef<object, Color> greenGlowField;

		public Color YellowGlow {
			get => yellowGlowField(HealthContainer);
			set => yellowGlowField(HealthContainer) = value;
		}
		[WrapperField("\u0317\u0315\u031A\u0310\u030E\u031A\u0315\u0315\u031C\u030E\u0319")]
		private static readonly AccessTools.FieldRef<object, Color> yellowGlowField;

		public Color RedGlow {
			get => redGlowField(HealthContainer);
			set => redGlowField(HealthContainer) = value;
		}
		[WrapperField("\u0312\u0317\u0318\u0319\u031A\u030E\u031A\u031C\u0316\u0312\u0310")]
		private static readonly AccessTools.FieldRef<object, Color> redGlowField;

		public Vector3 ArrowTopPosition {
			get => arrowTopPositionField(HealthContainer);
			set => arrowTopPositionField(HealthContainer) = value;
		}
		[WrapperField("\u0310\u030E\u0313\u030D\u0319\u0311\u031A\u030F\u031A\u031A\u0316")]
		private static readonly AccessTools.FieldRef<object, Vector3> arrowTopPositionField;

		public Vector3 ArrowMiddlePosition {
			get => arrowMiddlePositionField(HealthContainer);
			set => arrowMiddlePositionField(HealthContainer) = value;
		}
		[WrapperField("\u0314\u0317\u031B\u0316\u0317\u0311\u0311\u0314\u0310\u031B\u031B")]
		private static readonly AccessTools.FieldRef<object, Vector3> arrowMiddlePositionField;

		public Vector3 ArrowBottomPosition {
			get => arrowBottomPositionField(HealthContainer);
			set => arrowBottomPositionField(HealthContainer) = value;
		}
		[WrapperField("\u0319\u030F\u031B\u0314\u0315\u030E\u0316\u031B\u0310\u031C\u0311")]
		private static readonly AccessTools.FieldRef<object, Vector3> arrowBottomPositionField;

		#endregion

		#region Methods

		public void Start() => startMethod(HealthContainer);
		[WrapperMethod("Start")]
		private static readonly FastInvokeHandler startMethod;

		public void SetState(float health) => setStateMethod(HealthContainer, health);
		[WrapperMethod("\u0319\u0315\u0313\u030D\u0317\u0315\u0316\u0316\u030D\u0319\u031B")]
		private static readonly FastInvokeHandler setStateMethod;

		#endregion

		#region Duplicate Methods
#pragma warning disable IDE0051, CS0169 // Remove unused private members

		[WrapperMethod("\u0310\u0313\u030D\u0319\u0312\u0319\u0315\u031A\u030D\u0312\u030D")]
		private static readonly FastInvokeHandler startMethodDuplicate1;

		[WrapperMethod("\u0312\u0319\u0311\u031A\u031B\u0314\u0316\u0315\u030D\u0311\u0312")]
		private static readonly FastInvokeHandler startMethodDuplicate2;

		[WrapperMethod("\u0314\u030D\u031C\u0319\u030E\u0319\u0312\u030D\u0314\u031A\u0314")]
		private static readonly FastInvokeHandler startMethodDuplicate3;

		[WrapperMethod("\u0318\u030F\u030D\u0310\u030D\u0311\u0317\u0317\u030F\u031A\u0317")]
		private static readonly FastInvokeHandler startMethodDuplicate4;

		[WrapperMethod("\u0316\u030D\u0314\u0317\u0314\u031C\u031A\u0319\u0312\u0315\u031A")]
		private static readonly FastInvokeHandler startMethodDuplicate5;

		[WrapperMethod("\u0313\u0316\u0311\u031B\u031B\u030F\u0318\u030F\u0312\u0315\u0310")]
		private static readonly FastInvokeHandler startMethodDuplicate6;

		[WrapperMethod("\u031A\u0312\u0310\u0317\u030F\u030E\u031A\u0312\u0314\u0313\u0316")]
		private static readonly FastInvokeHandler startMethodDuplicate7;

		[WrapperMethod("\u030E\u030D\u031C\u0315\u0318\u0312\u0315\u0314\u0310\u030E\u0310")]
		private static readonly FastInvokeHandler startMethodDuplicate8;

		[WrapperMethod("\u030D\u0315\u0312\u0315\u0319\u0317\u030E\u0315\u0316\u030D\u0312")]
		private static readonly FastInvokeHandler startMethodDuplicate9;

		[WrapperMethod("\u031C\u0313\u0314\u031C\u030F\u0314\u031B\u0314\u0315\u0313\u030F")]
		private static readonly FastInvokeHandler startMethodDuplicate10;

		[WrapperMethod("\u031B\u0319\u030D\u030F\u031C\u030F\u031B\u0318\u030F\u030F\u0314")]
		private static readonly FastInvokeHandler startMethodDuplicate11;

		[WrapperMethod("\u0318\u0316\u030D\u030E\u0319\u0318\u0313\u0315\u0317\u0319\u0312")]
		private static readonly FastInvokeHandler startMethodDuplicate12;

		[WrapperMethod("\u031B\u0311\u0319\u0318\u031B\u0311\u0310\u0314\u030F\u0316\u0319")]
		private static readonly FastInvokeHandler startMethodDuplicate13;

		[WrapperMethod("\u030D\u0315\u0318\u0312\u030D\u0311\u031C\u030D\u0319\u0318\u031C")]
		private static readonly FastInvokeHandler startMethodDuplicate14;

		[WrapperMethod("\u031A\u0313\u031C\u0316\u0315\u031C\u030E\u030E\u031C\u0317\u030F")]
		private static readonly FastInvokeHandler startMethodDuplicate15;

		[WrapperMethod("\u031C\u0318\u031A\u030E\u0316\u0318\u0315\u030D\u0315\u0315\u030E")]
		private static readonly FastInvokeHandler startMethodDuplicate16;

		[WrapperMethod("\u0317\u0311\u0315\u031B\u0315\u030F\u030D\u0314\u0318\u030D\u0315")]
		private static readonly FastInvokeHandler setStateMethodDuplicate1;

		[WrapperMethod("\u0314\u0316\u031A\u030D\u0318\u0315\u0311\u031A\u030D\u0311\u0319")]
		private static readonly FastInvokeHandler setStateMethodDuplicate2;

		[WrapperMethod("\u0316\u0312\u0318\u0312\u030E\u030F\u0315\u0312\u0316\u0317\u0316")]
		private static readonly FastInvokeHandler setStateMethodDuplicate3;

		[WrapperMethod("\u030E\u031C\u031B\u0310\u0319\u0315\u0316\u0317\u030D\u0315\u0316")]
		private static readonly FastInvokeHandler setStateMethodDuplicate4;

		[WrapperMethod("\u031B\u0314\u030F\u030F\u0318\u030D\u0311\u0318\u0311\u0316\u0314")]
		private static readonly FastInvokeHandler setStateMethodDuplicate5;

		[WrapperMethod("\u0312\u0312\u031B\u0312\u0313\u0310\u031B\u0316\u0316\u031A\u031A")]
		private static readonly FastInvokeHandler setStateMethodDuplicate6;

		[WrapperMethod("\u031B\u0315\u0317\u0312\u030F\u031B\u031C\u031A\u030E\u0313\u0310")]
		private static readonly FastInvokeHandler setStateMethodDuplicate7;

		[WrapperMethod("\u0311\u031C\u0314\u0315\u031A\u0319\u0316\u0311\u0311\u0319\u0313")]
		private static readonly FastInvokeHandler setStateMethodDuplicate8;

		[WrapperMethod("\u0314\u030D\u0319\u0310\u031B\u031C\u030D\u0312\u031B\u0314\u031B")]
		private static readonly FastInvokeHandler setStateMethodDuplicate9;

		[WrapperMethod("\u031B\u0319\u0311\u0316\u0316\u0311\u031C\u031A\u0310\u0316\u0317")]
		private static readonly FastInvokeHandler setStateMethodDuplicate10;

		[WrapperMethod("\u031B\u0315\u0315\u0312\u0314\u031C\u0312\u0318\u0310\u0313\u0318")]
		private static readonly FastInvokeHandler setStateMethodDuplicate11;

		[WrapperMethod("\u0312\u030E\u031C\u0313\u0316\u0313\u0316\u0310\u031A\u031A\u0310")]
		private static readonly FastInvokeHandler setStateMethodDuplicate12;

		[WrapperMethod("\u031B\u0317\u0313\u0319\u0311\u0317\u030F\u031A\u031B\u0314\u031A")]
		private static readonly FastInvokeHandler setStateMethodDuplicate13;

		[WrapperMethod("\u031B\u030E\u0310\u030D\u0312\u031B\u0312\u030F\u030F\u0311\u0311")]
		private static readonly FastInvokeHandler setStateMethodDuplicate14;

		[WrapperMethod("\u031B\u0319\u0312\u031A\u0317\u0316\u0319\u0319\u030E\u031C\u0318")]
		private static readonly FastInvokeHandler setStateMethodDuplicate15;

		[WrapperMethod("\u0319\u0311\u0316\u031B\u0312\u030E\u0312\u031B\u031B\u0310\u0317")]
		private static readonly FastInvokeHandler setStateMethodDuplicate16;

		[WrapperMethod("\u0318\u030E\u030E\u031A\u0316\u0318\u030D\u0318\u031A\u030D\u0311")]
		private static readonly FastInvokeHandler setStateMethodDuplicate17;

		[WrapperMethod("\u0311\u0310\u0318\u0310\u031C\u030F\u030F\u0315\u0313\u0317\u0312")]
		private static readonly FastInvokeHandler setStateMethodDuplicate18;

		[WrapperMethod("\u0319\u0312\u030E\u0314\u0314\u031B\u0317\u030D\u031B\u0313\u0310")]
		private static readonly FastInvokeHandler setStateMethodDuplicate19;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
		#endregion

	}
}
