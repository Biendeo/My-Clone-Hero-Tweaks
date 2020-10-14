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
	[Wrapper(typeof(CameraShake))]
	public struct CameraShakeWrapper {
		public CameraShake CameraShake { get; private set; }

		public static CameraShakeWrapper Wrap(CameraShake cameraShake) => new CameraShakeWrapper {
			CameraShake = cameraShake
		};

		public override bool Equals(object obj) => CameraShake.Equals(obj);

		public override int GetHashCode() => CameraShake.GetHashCode();

		public bool IsNull() => CameraShake == null;

	}
}
