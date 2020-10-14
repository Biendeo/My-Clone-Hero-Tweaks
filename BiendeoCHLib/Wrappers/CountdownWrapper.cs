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
	[Wrapper(typeof(Countdown))]
	public struct CountdownWrapper {
		public Countdown Countdown { get; private set; }

		public static CountdownWrapper Wrap(Countdown countdown) => new CountdownWrapper {
			Countdown = countdown
		};

		public override bool Equals(object obj) => Countdown.Equals(obj);

		public override int GetHashCode() => Countdown.GetHashCode();

		public bool IsNull() => Countdown == null;
	}
}
