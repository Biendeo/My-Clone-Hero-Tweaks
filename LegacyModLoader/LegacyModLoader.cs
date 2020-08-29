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
		private string versionNumber;
		private string mainMenuLabelContent;

		private GUIStyle labelStyle;
		private GUIStyle buttonStyle;
		private GUIStyle windowStyle;
		private GUIStyle disabledButtonStyle;

		private ConfigEntry<bool> enableUnloadFeature;
		private bool showingLoaderMenu;
		private Rect loaderMenuRect;
		private int loaderMenuId;

		private List<LoaderInfo> loaders;

		public LegacyModLoader() {
			Instance = this;
			isMainMenu = true;
			showMainMenuLabel = Config.Bind("Visuals", "Show Main Menu Label", true);
			labelStyle = null;
			buttonStyle = null;
			windowStyle = null;
			enableUnloadFeature = Config.Bind("Features", "Enable Unload Feature", false);
			showingLoaderMenu = false;
			loaderMenuRect = new Rect(10.0f, 10.0f, 300.0f, 500.0f);
			loaderMenuId = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			loaders = new List<LoaderInfo>();
			LoadModInfo();
			InitialiseMods();
			versionNumber = $"v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
			mainMenuLabelContent = string.Empty;
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
				if (loader.Instance == null) {
					LoadMod(loader);
				}
			}
		}

		private void LoadMod(LoaderInfo loader) {
			try {
				loader.Instance = Activator.CreateInstance(loader.LoaderType);
				loader.LoadTweakMethod.Invoke(loader.Instance, Array.Empty<object>());
				Logger.LogInfo($"Loaded mod {loader.Assembly.FullName}");
			} catch (Exception exc) {
				loader.Instance = null;
				Logger.LogError($"Could not load mod {loader.Assembly.FullName}: {exc}");
			}
		}

		private void UnloadMod(LoaderInfo loader) {
			try {
				loader.UnloadTweakMethod.Invoke(loader.Instance, Array.Empty<object>());
				Logger.LogInfo($"Unloaded mod {loader.Assembly.FullName}");
				loader.Instance = null;
			} catch (Exception exc) {
				Logger.LogError($"Could not unload mod {loader.Assembly.FullName}: {exc}");
			}
		}

		#region Unity Methods

		public void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene destination) {
				isMainMenu = destination.name == "Main Menu";
			};
		}

		public void Update() {
			mainMenuLabelContent = $"Biendeo's Legacy Mod Loader {versionNumber} - {loaders.Count(l => l.Instance != null)} mods loaded";
			if (Input.GetKeyDown(KeyCode.F1)) {
				showingLoaderMenu = !showingLoaderMenu;
			}
		}

		public void OnGUI() {
			if (labelStyle == null || buttonStyle == null || windowStyle == null) {
				labelStyle = new GUIStyle(GUI.skin.label);
				buttonStyle = new GUIStyle(GUI.skin.button);
				windowStyle = new GUIStyle(GUI.skin.window);
				var disabledButtonTexture = new Texture2D(buttonStyle.normal.background.width, buttonStyle.normal.background.height);
				Graphics.CopyTexture(buttonStyle.normal.background, 0, 0, disabledButtonTexture, 0, 0);
				disabledButtonTexture.Apply();
				disabledButtonTexture.SetPixels(disabledButtonTexture.GetPixels().Select(c => new Color(c.r * 0.3f, c.g * 0.3f, c.b * 0.3f, c.a)).ToArray());
				disabledButtonTexture.Apply();
				disabledButtonStyle = new GUIStyle(GUI.skin.button) {
					normal = new GUIStyleState() {
						background = disabledButtonTexture,
						textColor = Color.white
					},
					active = new GUIStyleState() {
						background = disabledButtonTexture,
						textColor = Color.white
					},
					focused = new GUIStyleState() {
						background = disabledButtonTexture,
						textColor = Color.white
					},
					hover = new GUIStyleState() {
						background = disabledButtonTexture,
						textColor = Color.white
					}
				};
			}
			if (isMainMenu && showMainMenuLabel.Value) {
				GUI.Box(new Rect(0.0f, 0.0f, 350.0f, 22.0f), string.Empty);
				GUI.Label(new Rect(0.0f, 0.0f, 350.0f, 100.0f), mainMenuLabelContent);
			}
			if (showingLoaderMenu) {
				loaderMenuRect = GUILayout.Window(loaderMenuId, loaderMenuRect, DrawLoaderMenu, new GUIContent("Biendeo's Legacy Mod Loader"), windowStyle);
			}
		}

		private void DrawLoaderMenu(int id) {
			foreach (var loader in loaders) {
				if (!enableUnloadFeature.Value) {
					GUILayout.Button(new GUIContent(loader.Assembly.GetName().Name), disabledButtonStyle);
				} else {
					string loaderText;
					if (loader.Instance == null) {
						loaderText = $"Load {loader.Assembly.GetName().Name}";
					} else {
						loaderText = $"Unload {loader.Assembly.GetName().Name}";
					}
					if (GUILayout.Button(new GUIContent(loaderText), buttonStyle)) {
						if (loader.Instance == null) {
							LoadMod(loader);
						} else {
							UnloadMod(loader);
						}
					}
				}
			}
			GUILayout.Space(25.0f);

			GUILayout.Label($"Biendeo's Legacy Mod Loader {versionNumber}");
			GUILayout.Label("Thankyou for using this!");
			GUI.DragWindow();
		}

		#endregion
	}
}