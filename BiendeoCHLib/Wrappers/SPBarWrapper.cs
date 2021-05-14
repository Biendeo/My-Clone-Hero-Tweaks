using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(SPBar))]
	public struct SPBarWrapper {
		public SPBar SPBar { get; private set; }

		public static SPBarWrapper Wrap(SPBar spBar) => new SPBarWrapper {
			SPBar = spBar
		};

		public override bool Equals(object obj) => SPBar.Equals(obj);

		public override int GetHashCode() => SPBar.GetHashCode();

		public bool IsNull() => SPBar == null;

		#region Casts

		public MonoBehaviour CastToMonoBehaviour() => SPBar;

		#endregion

		#region Fields

		public float LastFillAmount {
			get => lastFillAmountField(SPBar);
			set => lastFillAmountField(SPBar) = value;
		}
		[WrapperField("\u0319\u031C\u031A\u030E\u0314\u030E\u0312\u0311\u0317\u0318\u0316")]
		private static readonly AccessTools.FieldRef<SPBar, float> lastFillAmountField;

		#endregion

		#region Methods

		public void Awake() => awakeMethod(SPBar);
		[WrapperMethod("Awake")]
		private static readonly FastInvokeHandler awakeMethod;

		public void SetFill(float fillAmount, bool isSPActive) => setFillMethod(SPBar, fillAmount, isSPActive);
		[WrapperMethod("\u031A\u0312\u030D\u0317\u0312\u031A\u0315\u0311\u0312\u030F\u0315")]
		private static readonly FastInvokeHandler setFillMethod;

		#endregion
	}
}
