using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using BiendeoCHLib.Wrappers;
using ComboIndicator.Settings;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GUI;

namespace ComboIndicator {
	[BepInPlugin("com.biendeo.comboindicator", "Combo Indicator", "1.5.0")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class ComboIndicator : BaseUnityPlugin {
		public static ComboIndicator Instance { get; private set; } 

		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private SoloCounterWrapper soloCounter;
		private ScoreManagerWrapper scoreManager;

		private Font uiFont;

		private string ConfigPath => Path.Combine(Paths.ConfigPath, Info.Metadata.GUID + ".config.xml");
		private Config config;

		private int lastCombo;
		private bool detectedHotStart;
		private float[] starPowers;
		private int numPlayers;

		private GUIStyle settingsWindowStyle;
		private GUIStyle settingsToggleStyle;
		private GUIStyle settingsButtonStyle;
		private GUIStyle settingsTextAreaStyle;
		private GUIStyle settingsTextFieldStyle;
		private GUIStyle settingsLabelStyle;
		private GUIStyle settingsBoxStyle;
		private GUIStyle settingsHorizontalSliderStyle;
		private GUIStyle settingsHorizontalSliderThumbStyle;

		private Vector2 settingsScrollPosition;

		private VersionCheck versionCheck;
		private Rect changelogRect;

		private Harmony Harmony;

		public ComboIndicator() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.comboindicator");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);

			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);

			starPowers = new float[4];
		}

		~ComboIndicator() {
			Harmony.UnpatchAll();
		}

		internal DancingText CreateDancingText(string text, bool isTest = false) {
			var textElement = new GameObject(string.Empty);
			var dancingText = textElement.AddComponent<DancingText>();
			dancingText.IsTest = isTest;
			dancingText.GameManager = gameManager;
			dancingText.Text = text;
			dancingText.Font = uiFont;
			dancingText.LabelSettings = config.Indicator;
			return dancingText;
		}

		#region Unity Methods

		public void Awake() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
		}

		public void Start() {
			config = Settings.Config.LoadConfig(ConfigPath);
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		public void LateUpdate() {
			string sceneName = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (sceneName == "Gameplay") {
					gameManager = GameManagerWrapper.Wrap(GameObject.Find("Game Manager")?.GetComponent<GameManager>());
					if (!gameManager.IsNull()) {
						soloCounter = gameManager.BasePlayers[0].SoloCounter;
						scoreManager = gameManager.ScoreManager;
					}
					lastCombo = 0;
					detectedHotStart = false;
					numPlayers = 0;
					for (int i = 0; i < 4; ++i) {
						numPlayers += gameManager.BasePlayers[i].IsNull() ? 0 : 1;
						starPowers[i] = 0.0f;
					}
				}
			}
			if (sceneName == "Gameplay" && !gameManager.IsNull()) {
				int currentCombo = scoreManager.OverallCombo;
				if (config.NoteStreakEnabled && currentCombo > 0 && currentCombo != lastCombo && (currentCombo == 50 || currentCombo % 100 == 0)) {
					CreateDancingText($"{currentCombo} Note Streak!");
				}
				if (!detectedHotStart && currentCombo == 25) {
					if (config.HotStartEnabled) {
						bool allPlayersFC = true;
						foreach (var player in gameManager.BasePlayers) {
							allPlayersFC &= player.IsNull() || !player.FirstNoteMissed;
						}
						if (allPlayersFC) {
							CreateDancingText($"Hot Start!");
						}
					}
					detectedHotStart = true;
				}
				if (config.StarPowerActiveEnabled) {
					for (int i = 0; i < 4; ++i) {
						var player = gameManager.BasePlayers[i];
						if (!player.IsNull()) {
							if (starPowers[i] < 0.5f && player.SPAmount >= 0.5f) {
								CreateDancingText(numPlayers > 1 ? $"{player.Player.PlayerProfile.PlayerName} Star Power Active!" : "Star Power Active!");
							}
							starPowers[i] = player.SPAmount;
						}
					}
				}
				lastCombo = currentCombo;
			} else if (sceneName == "Main Menu") {
				if (uiFont is null) {
					//TODO: Get the font directly from the bundle?
					uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
				}
			}
			config.HandleInput();
		}

		public void OnGUI() {
			if (settingsWindowStyle is null) {
				settingsWindowStyle = new GUIStyle(GUI.skin.window);
				settingsToggleStyle = new GUIStyle(GUI.skin.toggle);
				settingsButtonStyle = new GUIStyle(GUI.skin.button);
				settingsTextAreaStyle = new GUIStyle(GUI.skin.textArea);
				settingsTextFieldStyle = new GUIStyle(GUI.skin.textField);
				settingsLabelStyle = new GUIStyle(GUI.skin.label);
				settingsBoxStyle = new GUIStyle(GUI.skin.box);
				settingsHorizontalSliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
				settingsHorizontalSliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
			}
			if (config.ConfigWindowEnabled) {
				var outputRect = GUILayout.Window(187003001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Combo Indicator Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187003998, changelogRect, OnChangelogWindow, new GUIContent($"Combo Indicator Changelog"), settingsWindowStyle);
			}
		}

		private void OnWindow(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			var smallLabelStyle = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			settingsScrollPosition = GUILayout.BeginScrollView(settingsScrollPosition);
			config.ConfigureGUI(new BiendeoCHLib.Settings.GUIConfigurationStyles {
				LargeLabel = largeLabelStyle,
				SmallLabel = smallLabelStyle,
				Window = settingsWindowStyle,
				Toggle = settingsToggleStyle,
				Button = settingsButtonStyle,
				TextArea = settingsTextAreaStyle,
				TextField = settingsTextFieldStyle,
				Label = settingsLabelStyle,
				Box = settingsBoxStyle,
				HorizontalSlider = settingsHorizontalSliderStyle,
				HorizontalSliderThumb = settingsHorizontalSliderThumbStyle
			});

			GUILayout.Space(25.0f);
			if (GUILayout.Button("Reload Config", settingsButtonStyle)) {
				config.ReloadConfig(ConfigPath);
			}
			if (GUILayout.Button("Save Config", settingsButtonStyle)) {
				config.SaveConfig(ConfigPath);
			}

			GUILayout.Space(25.0f);

			GUILayout.Label($"Combo Indicator v{versionCheck.AssemblyVersion}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUILayout.EndScrollView();
			GUI.DragWindow();
		}

		private void OnChangelogWindow(int id) {
			var largeLabelStyle = new GUIStyle(settingsLabelStyle) {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				},
				wordWrap = false
			};
			var smallLabelStyle = new GUIStyle(settingsLabelStyle) {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				},
				wordWrap = true
			};
			GUILayout.Label("Thankyou for downloading Combo Indicator!", largeLabelStyle);
			GUILayout.Label("In gameplay, when you reach a note streak of 50, 100, 200, etc. you'll see the indicator show up above the highway.", smallLabelStyle);
			GUILayout.Label("Press Ctrl + Shift + F8 to enable/disable the config window.", smallLabelStyle);
			GUILayout.Label("The config window lets you change things such as the keys to enable/disable the mode, which indicators are active, and the layout of the indicator.", smallLabelStyle);
			GUILayout.Label("Please make sure to press the \"Save Config\" button at the bottom of the config window so that your settings are saved for the next time you run Clone Hero.", smallLabelStyle);
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			GUILayout.Label("Changelog", largeLabelStyle);
			GUILayout.Label("BepInEx is used as the mod loading framework now. This should lead to more robust features for mod developers.", smallLabelStyle);
			GUILayout.Label("Now supports \"Hot Start\" and \"Star Power Active\" indicators. You can now press Ctrl + Shift + F8 to change settings as well!", smallLabelStyle);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = versionCheck.AssemblyVersion;
				config.SaveConfig(ConfigPath);
			}
			GUI.DragWindow();
		}

		#endregion
	}
}