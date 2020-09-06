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
	[Wrapper(typeof(SpNeckRenderer))]
	public struct SpNeckRendererWrapper {
		public SpNeckRenderer SpNeckRenderer { get; private set; }

		public static SpNeckRendererWrapper Wrap(SpNeckRenderer spNeckRenderer) => new SpNeckRendererWrapper {
			SpNeckRenderer = spNeckRenderer
		};

		public override bool Equals(object obj) => SpNeckRenderer.Equals(obj);

		public override int GetHashCode() => SpNeckRenderer.GetHashCode();

		public bool IsNull() => SpNeckRenderer == null;

	}
}
