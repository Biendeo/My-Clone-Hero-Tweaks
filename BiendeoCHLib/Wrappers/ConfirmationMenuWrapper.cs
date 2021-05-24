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
	[Wrapper(typeof(ConfirmationMenu))]
	public struct ConfirmationMenuWrapper {
		public ConfirmationMenu ConfirmationMenu { get; private set; }

		public static ConfirmationMenuWrapper Wrap(ConfirmationMenu confirmationMenu) => new ConfirmationMenuWrapper {
			ConfirmationMenu = confirmationMenu
		};

		public override bool Equals(object obj) => ConfirmationMenu.Equals(obj);

		public override int GetHashCode() => ConfirmationMenu.GetHashCode();

		public bool IsNull() => ConfirmationMenu == null;

		#region Methods

		public void Enable(string message, string option1, string option2, MenuDelegate confirm, MenuDelegate decline) => enableMethod(ConfirmationMenu, message, option1, option2, confirm, decline);
		[WrapperMethod("\u0319\u0310\u0319\u031C\u030F\u0313\u0313\u0317\u0311\u0315\u031B")]
		private static readonly FastInvokeHandler enableMethod;

		#endregion

		#region Delegates

		public delegate void MenuDelegate();

		#endregion

	}
}
