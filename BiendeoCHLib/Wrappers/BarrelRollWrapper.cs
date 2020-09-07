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
	[Wrapper(typeof(BarrelRoll))]
	public struct BarrelRollWrapper {
		public BarrelRoll BarrelRoll { get; private set; }

		public static BarrelRollWrapper Wrap(BarrelRoll barrelRoll) => new BarrelRollWrapper {
			BarrelRoll = barrelRoll
		};

		public override bool Equals(object obj) => BarrelRoll.Equals(obj);

		public override int GetHashCode() => BarrelRoll.GetHashCode();

		public bool IsNull() => BarrelRoll == null;
	}
}
