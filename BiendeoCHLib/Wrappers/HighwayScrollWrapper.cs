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
	[Wrapper(typeof(HighwayScroll))]
	public struct HighwayScrollWrapper {
		public HighwayScroll HighwayScroll { get; private set; }

		public static HighwayScrollWrapper Wrap(HighwayScroll highwayScroll) => new HighwayScrollWrapper {
			HighwayScroll = highwayScroll
		};

		public override bool Equals(object obj) => HighwayScroll.Equals(obj);

		public override int GetHashCode() => HighwayScroll.GetHashCode();

		public bool IsNull() => HighwayScroll == null;

	}
}
