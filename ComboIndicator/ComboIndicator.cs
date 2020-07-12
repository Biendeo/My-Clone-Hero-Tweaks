using ComboIndicator.Settings;
using Common;
using Common.Wrappers;
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
	public class ComboIndicator : MonoBehaviour {
		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private SoloCounterWrapper soloCounter;
		private ScoreManagerWrapper scoreManager;

		private Font uiFont;

		private int lastCombo;

		private Config config;

		private GUIStyle settingsWindowStyle;
		private GUIStyle settingsToggleStyle;
		private GUIStyle settingsButtonStyle;
		private GUIStyle settingsTextAreaStyle;
		private GUIStyle settingsTextFieldStyle;
		private GUIStyle settingsLabelStyle;
		private GUIStyle settingsBoxStyle;
		private GUIStyle settingsHorizontalSliderStyle;
		private GUIStyle settingsHorizontalSliderThumbStyle;

		private readonly VersionCheck versionCheck;
		private Rect changelogRect;

		public ComboIndicator() {
			versionCheck = new VersionCheck(187003999);
			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
		}

		#region Unity Methods

		public void Start() {
			config = Config.LoadConfig();
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		public void LateUpdate() {
			string sceneName = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (sceneName == "Gameplay") {
					gameManager = new GameManagerWrapper(GameObject.Find("Game Manager")?.GetComponent<GameManager>());
					if (!gameManager.IsNull()) {
						soloCounter = gameManager.BasePlayers[0].SoloCounter;
						scoreManager = gameManager.ScoreManager;
					}
					lastCombo = 0;
				}
			}
			if (sceneName == "Gameplay" && !gameManager.IsNull()) {
				int currentCombo = scoreManager.OverallCombo;
				if (currentCombo > 0 && currentCombo != lastCombo && (currentCombo == 50 || currentCombo % 100 /*100*/ == 0)) {
					var textElement = new GameObject(string.Empty, new Type[] {
						typeof(DancingText)
					});
					textElement.GetComponent<DancingText>().GameManager = gameManager;
					textElement.GetComponent<DancingText>().Text = $"{currentCombo} Note Streak!";
					textElement.GetComponent<DancingText>().Font = uiFont;
					textElement.GetComponent<DancingText>().RaisedForSolo = true; // Could be soloCounter.Bool2 but I want to gauge how people respond first.
				}
				lastCombo = currentCombo;
			} else if (sceneName == "Main Menu") {
				if (uiFont is null) {
					//TODO: Get the font directly from the bundle?
					uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
				}
				if (!versionCheck.HasVersionBeenChecked) {
					if (config.SilenceUpdates) {
						versionCheck.HasVersionBeenChecked = true;
					} else {
						string detectedVersion = GlobalVariablesWrapper.instance.buildVersion;
						versionCheck.CheckVersion(detectedVersion);
					}
				}
			}
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
			if (versionCheck.IsShowingUpdateWindow) {
				versionCheck.DrawUpdateWindow(settingsWindowStyle, settingsLabelStyle, settingsButtonStyle);
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187003998, changelogRect, OnChangelogWindow, new GUIContent($"Combo Indicator Changelog"), settingsWindowStyle);
			}
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
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			GUILayout.Label("Changelog", largeLabelStyle);
			GUILayout.Label("Performance improvements! Hopefully you enjoy the speed-ups.", smallLabelStyle);
			GUILayout.Label("Thanks E2 and MWisBest for the help.", smallLabelStyle);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = versionCheck.AssemblyVersion;
				config.SaveConfig();
			}
			GUI.DragWindow();
		}

		#endregion
	}
}