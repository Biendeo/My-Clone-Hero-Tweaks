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
	[Wrapper(typeof(ComboColor))]
	public struct ComboColorWrapper {
		public ComboColor ComboColor { get; private set; }

		public static ComboColorWrapper Wrap(ComboColor comboColor) => new ComboColorWrapper {
			ComboColor = comboColor
		};

		public override bool Equals(object obj) => ComboColor.Equals(obj);

		public override int GetHashCode() => ComboColor.GetHashCode();

		public bool IsNull() => ComboColor == null;
	}
}
