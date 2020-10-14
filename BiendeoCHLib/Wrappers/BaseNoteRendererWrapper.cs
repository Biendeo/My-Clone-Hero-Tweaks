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
	[Wrapper(typeof(BaseNoteRenderer))]
	public struct BaseNoteRendererWrapper {
		public BaseNoteRenderer BaseNoteRenderer { get; private set; }

		public static BaseNoteRendererWrapper Wrap(BaseNoteRenderer baseNoteRenderer) => new BaseNoteRendererWrapper {
			BaseNoteRenderer = baseNoteRenderer
		};

		public override bool Equals(object obj) => BaseNoteRenderer.Equals(obj);

		public override int GetHashCode() => BaseNoteRenderer.GetHashCode();

		public bool IsNull() => BaseNoteRenderer == null;
	}
}
