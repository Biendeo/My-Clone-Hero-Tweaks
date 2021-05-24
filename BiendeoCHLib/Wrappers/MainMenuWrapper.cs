using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using Rewired.UI.ControlMapper;
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

		public GameObject SongSelect {
			get => songSelectField(MainMenu);
			set => songSelectField(MainMenu) = value;
		}
		[WrapperField("songSelect")]
		private static readonly AccessTools.FieldRef<MainMenu, GameObject> songSelectField;

		public GameObject SettingsMenu {
			get => settingsMenuField(MainMenu);
			set => settingsMenuField(MainMenu) = value;
		}
		[WrapperField("settingsMenu")]
		private static readonly AccessTools.FieldRef<MainMenu, GameObject> settingsMenuField;

		public GameObject OnlineMenu {
			get => onlineMenuField(MainMenu);
			set => onlineMenuField(MainMenu) = value;
		}
		[WrapperField("onlineMenu")]
		private static readonly AccessTools.FieldRef<MainMenu, GameObject> onlineMenuField;

		public SongScanWrapper SongScan {
			get => SongScanWrapper.Wrap(songScanField(MainMenu));
			set => songScanField(MainMenu) = value.SongScan;
		}
		[WrapperField("songScan")]
		private static readonly AccessTools.FieldRef<MainMenu, SongScan> songScanField;

		public ControlMapper ControlMapper {
			get => controlMapperField(MainMenu);
			set => controlMapperField(MainMenu) = value;
		}
		[WrapperField("controlMapper")]
		private static readonly AccessTools.FieldRef<MainMenu, ControlMapper> controlMapperField;

		public ConfirmationMenuWrapper ConfirmMenu {
			get => ConfirmationMenuWrapper.Wrap(confirmMenuField(MainMenu));
			set => confirmMenuField(MainMenu) = value.ConfirmationMenu;
		}
		[WrapperField("confirmMenu")]
		private static readonly AccessTools.FieldRef<MainMenu, ConfirmationMenu> confirmMenuField;

		public ConfirmationMenuWrapper NotSupportedMenu {
			get => ConfirmationMenuWrapper.Wrap(notSupportedMenuField(MainMenu));
			set => notSupportedMenuField(MainMenu) = value.ConfirmationMenu;
		}
		[WrapperField("notSupportedMenu")]
		private static readonly AccessTools.FieldRef<MainMenu, ConfirmationMenu> notSupportedMenuField;

		#endregion

		#region Methods

		#endregion
	}
}
