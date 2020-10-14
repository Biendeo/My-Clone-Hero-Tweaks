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

	}
}
