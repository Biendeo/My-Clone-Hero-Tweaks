using BepInEx;
using BepInEx.Configuration;
using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BiendeoCHLib {
	[BepInPlugin("com.biendeo.biendeochlib", "Biendeo CH Lib", "1.5.2")]
	public class BiendeoCHLib : BaseUnityPlugin {
		public static BiendeoCHLib Instance { get; private set; }
		public string[] SystemFonts { get; private set; }
		public const string CloneHeroDefaultFontName = "Clone Hero Default Font";
		public Font CloneHeroDefaultFont { get; private set; }

		private VersionCheck versionCheck;

		public BiendeoCHLib() {
			Instance = this;
			WrapperBase.InitializeWrappers(Logger);

			CloneHeroDefaultFont = null;
		}

		public void Awake() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);

			var fonts = Font.GetOSInstalledFontNames().ToList();
			fonts.Add("Clone Hero Default");
			fonts.Sort();

			SystemFonts = fonts.ToArray();
		}

		public void Update() {
			if (CloneHeroDefaultFont == null) {
				if (SceneManager.GetActiveScene().name == "Main Menu") {

					//TODO: Get the font directly from the bundle?

					CloneHeroDefaultFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
					Logger.LogDebug(CloneHeroDefaultFont.name);
				}
			}
		}
	}
}
