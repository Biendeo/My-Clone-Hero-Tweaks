using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LegacyModLoader {
	[BepInPlugin("com.biendeo.legacymodloader", "Legacy Mod Loader", "1.5.0.0")]
	public class LegacyModLoader : BaseUnityPlugin {
		public static LegacyModLoader Instance { get; private set; }

		private bool isMainMenu;
		private ConfigEntry<bool> showMainMenuLabel;
		private string mainMenuLabelContent;

		private List<LoaderInfo> loaders;

		public LegacyModLoader() {
			Instance = this;
			isMainMenu = true;
			showMainMenuLabel = Config.Bind("Visuals", "Show Main Menu Label", true);
			loaders = new List<LoaderInfo>();
			LoadModInfo();
			InitialiseMods();
			mainMenuLabelContent = $"Biendeo's Legacy Mod Loader v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion} - {loaders.Count(l => l.Instance != null)} mods loaded";
		}

		private void LoadModInfo() {
			var tweakDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tweaks"));
			if (!tweakDirectory.Exists) {
				tweakDirectory.Create();
				Logger.LogInfo($"Made new tweaks folder {tweakDirectory.FullName}");
			}
			foreach (var file in tweakDirectory.GetFiles()) {
				try {
					var assembly = Assembly.LoadFile(file.FullName);
					var loaderType = assembly.GetType($"{Path.GetFileNameWithoutExtension(file.FullName)}.Loader");
					var loadMethod = loaderType.GetMethod("LoadTweak");
					var unloadMethod = loaderType.GetMethod("UnloadTweak");
					loaders.Add(new LoaderInfo {
						Assembly = assembly,
						LoaderType = loaderType,
						LoadTweakMethod = loadMethod,
						UnloadTweakMethod = unloadMethod,
						Instance = null
					});
					Logger.LogInfo($"Found mod {assembly.FullName}");
				} catch (Exception exc) when (
					exc is FileLoadException ||
					exc is BadImageFormatException ||
					exc is ArgumentException ||
					exc is AmbiguousMatchException) {

				}
			}
		}

		private void InitialiseMods() {
			foreach (var loader in loaders) {
				try {
					if (loader.Instance == null) {
						loader.Instance = Activator.CreateInstance(loader.LoaderType);
						loader.LoadTweakMethod.Invoke(loader.Instance, Array.Empty<object>());
						Logger.LogInfo($"Initialised mod {loader.Assembly.FullName}");
					}
				} catch (Exception exc) {
					loader.Instance = null;
					Logger.LogError($"Could not load mod {loader.Assembly.FullName}: {exc}");
				}
			}
		}

		#region Unity Methods

		public void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene destination) {
				isMainMenu = destination.name == "Main Menu";
			};
		}

		public void OnGUI() {
			if (isMainMenu && showMainMenuLabel.Value) {
				GUI.Box(new Rect(0.0f, 0.0f, 330.0f, 22.0f), string.Empty);
				GUI.Label(new Rect(0.0f, 0.0f, 330.0f, 100.0f), mainMenuLabelContent);
			}
		}

		#endregion
	}
}