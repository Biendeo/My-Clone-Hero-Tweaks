using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(FrameRate))]
	public struct FrameRateWrapper {
		public FrameRate FrameRate { get; private set; }

		public static FrameRateWrapper Wrap(FrameRate frameRate) => new FrameRateWrapper {
			FrameRate = frameRate
		};

		public override bool Equals(object obj) => FrameRate.Equals(obj);

		public override int GetHashCode() => FrameRate.GetHashCode();

		public bool IsNull() => FrameRate == null;

		#region Fields

		public TextMeshProUGUI Text {
			get => textField(FrameRate);
			set => textField(FrameRate) = value;
		}
		[WrapperField("\u0318\u030F\u0319\u0310\u0312\u0310\u030E\u0314\u0310\u031A\u0313")]
		private static readonly AccessTools.FieldRef<FrameRate, TextMeshProUGUI> textField;

		#endregion
	}
}
