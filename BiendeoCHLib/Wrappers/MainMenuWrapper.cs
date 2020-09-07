using BiendeoCHLib.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(MainMenu))]
	public struct MainMenuWrapper {
		public MainMenu MainMenu { get; private set; }

		public static MainMenuWrapper Wrap(MainMenu mainMenu) => new MainMenuWrapper {
			MainMenu = mainMenu
		};

		public override bool Equals(object obj) => MainMenu.Equals(obj);

		public override int GetHashCode() => MainMenu.GetHashCode();

		public bool IsNull() => MainMenu == null;

		#region Constructors

		public static MainMenuWrapper Construct() => new MainMenuWrapper {
			MainMenu = (MainMenu)defaultConstructor.Invoke(Array.Empty<object>())
		};
		[WrapperConstructor]
		private static readonly ConstructorInfo defaultConstructor;

		#endregion

		#region Fields

		#endregion

		#region Methods

		#endregion
	}
}
