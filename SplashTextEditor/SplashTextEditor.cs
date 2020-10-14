using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using SplashTextEditor.Settings;
using System;
using System.Collections;
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

namespace SplashTextEditor {
	[BepInPlugin("com.biendeo.splashtexteditor", "Splash Text Editor", "1.5.0.0")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class SplashTextEditor : BaseUnityPlugin {
		private bool sceneChanged;

		private string ConfigPath => Path.Combine(Paths.ConfigPath, Info.Metadata.GUID + ".config.xml");
		private Config config;

		private TextMeshProUGUI splashTextComponent;
		private readonly System.Random randomGenerator;
		private int currentSplashIndex;
		private bool previewingMessage;

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

		private readonly VersionCheck versionCheck;
		private Rect changelogRect;

		private static readonly string[] aprilFoolsSplashMessages = new string[] {
			 "WELCOME TO THE DISASTER :)",
			 "YBOOBYYO TO DELETE DISASTER"
		};
		private static readonly string dragonforceSplashMessage = "NOT AS GOOD AS DOG ASMR";

		public SplashTextEditor() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
			changelogRect = new Rect(500.0f, 500.0f, 100.0f, 100.0f);
			randomGenerator = new System.Random();
			splashTextComponent = null;
			currentSplashIndex = 0;
			previewingMessage = false;
		}

		#region Unity Methods

		public void Start() {
			config = Settings.Config.LoadConfig(ConfigPath);
			config.ResetSplashes = () => {
				currentSplashIndex = GetNewSplashIndex();
			};
			config.InsertMessage = (int i) => {
				if (i == -1) {
					currentSplashIndex = 0;
				} else if (currentSplashIndex > i) {
					++currentSplashIndex;
				}
			};
			config.DeleteMessage = (int i) => {
				if (currentSplashIndex == i) {
					currentSplashIndex = config.Messages.Count == 0 ? -1 : randomGenerator.Next(0, config.Messages.Count);
					previewingMessage = false;
				} else if (currentSplashIndex > i) {
					--currentSplashIndex;
				}
			};
			config.PreviewMessage = (int i) => {
				currentSplashIndex = i;
				previewingMessage = true;
			};
			config.ShiftUpMessage = (int i) => {
				if (currentSplashIndex == i) {
					--currentSplashIndex;
				}
			};
			config.ShiftDownMessage = (int i) => {
				if (currentSplashIndex == i) {
					++currentSplashIndex;
				}
			};
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
			StartCoroutine(UpdateSplashIndex());
			Logger.LogDebug($"{GlobalVariablesWrapper.Instance.SplashMessages.Length} vanilla splash messages: [{string.Join(", ", GlobalVariablesWrapper.Instance.SplashMessages)}]");
		}

		private int GetNewSplashIndex() {
			int range = config.Messages.Count;
			if (config.VanillaSplashMessages) {
				range += GlobalVariablesWrapper.Instance.SplashMessages.Length;
				if (config.AprilFoolsSplashes && GlobalVariablesWrapper.Instance.AprilFoolsMode) {
					range += aprilFoolsSplashMessages.Length;
				}
			}
			if (range == 0) {
				return -1;
			} else {
				return randomGenerator.Next(range);
			}
		}

		private IEnumerator UpdateSplashIndex() {
			while (true) {
				if (!previewingMessage) {
					currentSplashIndex = GetNewSplashIndex();
				}
				yield return new WaitForSeconds(config.CycleTime);
			}
		}

		public void LateUpdate() {
			string sceneName = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				splashTextComponent = null;
				previewingMessage = false;
				this.sceneChanged = false;
			}
			if (splashTextComponent == null && sceneName == "Main Menu") {
				var splashTextObject = GameObject.Find("Tag");
				if (splashTextObject != null) {
					splashTextComponent = splashTextObject.GetComponent<TextMeshProUGUI>();
				}
			}
			if (config.Enabled && splashTextComponent != null) {
				if (config.DragonforceOverride && BassAudioManagerWrapper.Instance.MenuSong.SongEntry != null && BassAudioManagerWrapper.Instance.MenuSong.Artist.ValueLowerCase == "dragonforce") {
					splashTextComponent.text = dragonforceSplashMessage;
				} else if (currentSplashIndex >= 0) {
					if (currentSplashIndex < config.Messages.Count) {
						splashTextComponent.text = config.Messages[currentSplashIndex];
					} else if (config.VanillaSplashMessages && currentSplashIndex < config.Messages.Count + GlobalVariablesWrapper.Instance.SplashMessages.Length) {
						splashTextComponent.text = GlobalVariablesWrapper.Instance.SplashMessages[currentSplashIndex - config.Messages.Count];
						if (splashTextComponent.text.Contains("{0}")) {
							splashTextComponent.text = string.Format(splashTextComponent.text, Environment.UserName);
						}
					} else if (config.VanillaSplashMessages && config.AprilFoolsSplashes && GlobalVariablesWrapper.Instance.AprilFoolsMode) {
						splashTextComponent.text = aprilFoolsSplashMessages[currentSplashIndex - config.Messages.Count - GlobalVariablesWrapper.Instance.SplashMessages.Length];
					}
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
				var outputRect = GUILayout.Window(187004001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Splash Text Editor Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187004998, changelogRect, OnChangelogWindow, new GUIContent($"Splash Text Editor Changelog"), settingsWindowStyle);
			}
		}

		#endregion

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
			config.ConfigureGUI(new GUIConfigurationStyles {
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

			GUILayout.Label($"Splash Text Editor v{versionCheck.AssemblyVersion}");
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
			GUILayout.Label("Thankyou for downloading Splash Text Editor!", largeLabelStyle);
			GUILayout.Label("Simply press Ctrl + Shift + F9 to bring up the config menu. From there you can add new splash messages that will randomly be shown on the title screen.", smallLabelStyle);
			GUILayout.Label("In the config menu, unticking \"Enable\" will result in the original splash text appearing instead of your own. If your messages aren't showing, make sure you've got that ticked!", smallLabelStyle);
			GUILayout.Label("You can also tick \"Vanilla Splash Messages\" to show the default splash messages, as \"DragonForce Override\" and \"April Fools Splash\" if you want those extra messages to appear when appropriate. Finally, you can change the amount of time spent between splash messages with the slider.", smallLabelStyle);
			GUILayout.Label("To make a splash message, just click insert in the config menu. You can then edit the text of that message. You can add more splash messages by clicking insert more times (it'll add a new message directly underneath your last message). You can also click delete to remove that message, and shift up and shift down to organise the messages (note that the order does not affect how frequently the messages will appear).", smallLabelStyle);
			GUILayout.Label("If you are on the main menu, you can also click \"Preview\" which immediately replaces the current splash text with that item. Splash messages won't be randomly cycled through until you either click \"Randomly select new splash\" or change scene (i.e. going into credits or a song).", smallLabelStyle);
			GUILayout.Label("Make sure you press \"Save\" at the bottom of the config menu to ensure your messages persist when you reopen Clone Hero!", smallLabelStyle);
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = versionCheck.AssemblyVersion;
				config.SaveConfig(ConfigPath);
			}
			GUI.DragWindow();
		}
	}
}