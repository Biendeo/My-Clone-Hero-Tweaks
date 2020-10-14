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
	[Wrapper(typeof(BaseNeckController))]
	public struct BaseNeckControllerWrapper {
		public BaseNeckController BaseNeckController { get; private set; }

		public static BaseNeckControllerWrapper Wrap(BaseNeckController baseNeckController) => new BaseNeckControllerWrapper {
			BaseNeckController = baseNeckController
		};

		public override bool Equals(object obj) => BaseNeckController.Equals(obj);

		public override int GetHashCode() => BaseNeckController.GetHashCode();

		public bool IsNull() => BaseNeckController == null;
	}
}
