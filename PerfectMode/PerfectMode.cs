using Common;
using Common.Wrappers;
using PerfectMode.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PerfectMode {
	public class PerfectMode : MonoBehaviour {
		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private List<NoteWrapper> notes;

		private bool failedObjective;
		private float remainingTimeBeforeRestart;

		private Font uiFont;
		private bool invokedSceneChange;

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
		private Vector2 settingsScrollPosition;

		private string target;
		private bool isStillFC;
		private int missedNotes;
		private int totalNoteCount;
		private int currentNoteIndex;

		private GameObject displayImageLabel;
		private GameObject remainingNotesLeftLabel;
		private GameObject restartIndicatorLabel;

		private readonly VersionCheck VersionCheck;
		private Rect ChangelogRect;

		public PerfectMode() {
			settingsScrollPosition = new Vector2();
			VersionCheck = new VersionCheck(187001999);
			ChangelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
		}

		#region Unity Methods

		void Start() {
			config = Config.LoadConfig();
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
				failedObjective = false;
			};
		}

		private void DestroyAndNullGameplayLabels() {
			if (displayImageLabel != null) Destroy(displayImageLabel);
			if (remainingNotesLeftLabel != null) Destroy(remainingNotesLeftLabel);
			if (restartIndicatorLabel != null) Destroy(restartIndicatorLabel);
			displayImageLabel = null;
			remainingNotesLeftLabel = null;
			restartIndicatorLabel = null;
		}

		private void ResetGameplaySceneValues() {
			notes = gameManager.BasePlayers[0].Notes;
			totalNoteCount = notes?.Count ?? 0;
			currentNoteIndex = 0;
			missedNotes = 0;
			invokedSceneChange = false;
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					ResetGameplaySceneValues();

					DestroyAndNullGameplayLabels();
					Transform canvasTransform = FadeBehaviourWrapper.instance.fadeGraphic.canvas.transform;

					displayImageLabel = new GameObject($"Perfect Mode Indicator", new Type[] {
						typeof(Text)
					});
					displayImageLabel.layer = LayerMask.NameToLayer("UI");
					displayImageLabel.transform.SetParent(canvasTransform);
					displayImageLabel.transform.SetSiblingIndex(0);
					displayImageLabel.transform.localEulerAngles = new Vector3();
					displayImageLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					displayImageLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					displayImageLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					displayImageLabel.GetComponent<Text>().font = uiFont;

					remainingNotesLeftLabel = new GameObject($"Perfect Mode Notes Remaining", new Type[] {
						typeof(Text)
					});
					remainingNotesLeftLabel.layer = LayerMask.NameToLayer("UI");
					remainingNotesLeftLabel.transform.SetParent(canvasTransform);
					remainingNotesLeftLabel.transform.SetSiblingIndex(0);
					remainingNotesLeftLabel.transform.localEulerAngles = new Vector3();
					remainingNotesLeftLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					remainingNotesLeftLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					remainingNotesLeftLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					remainingNotesLeftLabel.GetComponent<Text>().font = uiFont;

					restartIndicatorLabel = new GameObject($"Perfect Mode Restart Message", new Type[] {
						typeof(Text)
					});
					restartIndicatorLabel.layer = LayerMask.NameToLayer("UI");
					restartIndicatorLabel.transform.SetParent(canvasTransform);
					restartIndicatorLabel.transform.SetSiblingIndex(0);
					restartIndicatorLabel.transform.localEulerAngles = new Vector3();
					restartIndicatorLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					restartIndicatorLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					restartIndicatorLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					restartIndicatorLabel.GetComponent<Text>().font = uiFont;
				} else {
					DestroyAndNullGameplayLabels();
				}
			}
			if (SceneManager.GetActiveScene().name == "Main Menu" && !VersionCheck.HasVersionBeenChecked) {
				if (config.SilenceUpdates) {
					VersionCheck.HasVersionBeenChecked = true;
				} else {
					string detectedVersion = GlobalVariablesWrapper.instance.buildVersion;
					VersionCheck.CheckVersion(detectedVersion);
				}
			}
			if (config.Enabled && SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null) {
				target = config.FC ? "FC" : (config.NotesMissed == 0 ? "100%" : $"-{config.NotesMissed}");
				isStillFC = !gameManager.BasePlayers[0].FirstNoteMissed;
				while (currentNoteIndex < totalNoteCount && (notes[currentNoteIndex].WasHit || notes[currentNoteIndex].WasMissed)) {
					if (!notes[currentNoteIndex].WasHit && notes[currentNoteIndex].WasMissed) {
						++missedNotes;
					}
					++currentNoteIndex;
				}
				if (!failedObjective && (config.FC && !isStillFC || config.NotesMissed < missedNotes)) {
					failedObjective = true;
					remainingTimeBeforeRestart = Math.Min(config.FailDelay, (float)(gameManager.SongLength - gameManager.SongTime));
				}
				if (failedObjective && (gameManager.PauseMenu is null || !gameManager.PauseMenu.activeInHierarchy)) {
					remainingTimeBeforeRestart -= Time.deltaTime;
					if (remainingTimeBeforeRestart < 0.0f && !invokedSceneChange) {
						//TODO: Double-check that multiplayer works fine with this.
						StartCoroutine(FadeBehaviourWrapper.instance.InvokeSceneChange("Gameplay"));
						invokedSceneChange = true;
					}
				}

				if (config.DisplayImage.Visible || config.LayoutTest) {
					displayImageLabel.transform.localPosition = new Vector3(config.DisplayImage.X - Screen.width / 2, Screen.height / 2 - config.DisplayImage.Y);
					var text = displayImageLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.DisplayImage.Size;
					text.alignment = config.DisplayImage.Alignment;
					text.fontStyle = (config.DisplayImage.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.DisplayImage.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{target} mode active";
					text.color = config.DisplayImage.Color.Color;
				} else {
					displayImageLabel.GetComponent<Text>().enabled = false;
				}

				if ((config.RemainingNotesLeft.Visible && !config.FC) || config.LayoutTest) {
					remainingNotesLeftLabel.transform.localPosition = new Vector3(config.RemainingNotesLeft.X - Screen.width / 2, Screen.height / 2 - config.RemainingNotesLeft.Y);
					var text = remainingNotesLeftLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.RemainingNotesLeft.Size;
					text.alignment = config.RemainingNotesLeft.Alignment;
					text.fontStyle = (config.RemainingNotesLeft.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.RemainingNotesLeft.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = config.NotesMissed - missedNotes >= 0 ? $"{config.NotesMissed - missedNotes} note{(config.NotesMissed - missedNotes == 1 ? string.Empty : "s")} can be missed" : $"Too many notes missed";
					text.color = config.RemainingNotesLeft.Color.Color;
				} else {
					remainingNotesLeftLabel.GetComponent<Text>().enabled = false;
				}

				if ((config.RestartIndicator.Visible && failedObjective) || config.LayoutTest) {
					restartIndicatorLabel.transform.localPosition = new Vector3(config.RestartIndicator.X - Screen.width / 2, Screen.height / 2 - config.RestartIndicator.Y);
					var text = restartIndicatorLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.RestartIndicator.Size;
					text.alignment = config.RestartIndicator.Alignment;
					text.fontStyle = (config.RestartIndicator.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.RestartIndicator.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{target} failed, restarting in {(int)remainingTimeBeforeRestart + 1}";
					text.color = new Color(config.RestartIndicator.Color.Color.r, config.RestartIndicator.Color.Color.g, config.RestartIndicator.Color.Color.b, config.RestartIndicator.Color.Color.a * Math.Min((config.FailDelay - remainingTimeBeforeRestart) * 2.0f, 1.0f));
				} else {
					restartIndicatorLabel.GetComponent<Text>().enabled = false;
				}
			} else if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
				displayImageLabel.GetComponent<Text>().enabled = false;
				remainingNotesLeftLabel.GetComponent<Text>().enabled = false;
				restartIndicatorLabel.GetComponent<Text>().enabled = false;
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			config.HandleInput();
		}

		void OnGUI() {
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
				//TODO: Look into why the GUILayout just panics if this and Extra Song UI are both loaded.
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(187001001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Perfect Mode Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (VersionCheck.IsShowingUpdateWindow) {
				VersionCheck.DrawUpdateWindow(settingsWindowStyle, settingsLabelStyle, settingsButtonStyle);
			}
			if (config.TweakVersion != FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion && !config.SeenChangelog) {
				ChangelogRect = GUILayout.Window(187001998, ChangelogRect, OnChangelogWindow, new GUIContent($"Perfect Mode Changelog"), settingsWindowStyle);
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
			config.ConfigureGUI(new Common.Settings.GUIConfigurationStyles {
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

			GUILayout.Label($"Perfect Mode v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
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
			GUILayout.Label("Thankyou for downloading Perfect Mode!", largeLabelStyle);
			GUILayout.Label("To get started, press F6 to enable/disable Perfect Mode.", smallLabelStyle);
			GUILayout.Label("Press Ctrl + Shift + F6 to enable/disable the config window.", smallLabelStyle);
			GUILayout.Label("The config window lets you change things such as the keys to enable/disable the mode, how many notes you can miss, and the layout of the UI.", smallLabelStyle);
			GUILayout.Label("Please make sure to press the \"Save Config\" button at the bottom of the config window so that your settings are saved for the next time you run Clone Hero.", smallLabelStyle);
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			GUILayout.Label("Changelog", largeLabelStyle);
			GUILayout.Label("This changelog will now appear if you ever change this tweak's version! This should help new users know how to use this tweak, and tell you about any changes for more regular users.", smallLabelStyle);
			GUILayout.Label("If there's a new version available, a window will prompt, just like this changelog, telling you to download it. This is also based on the version of CH you're running, so when v0.24 comes out, you won't be spammed on v0.23.2.2.", smallLabelStyle);
			GUILayout.Label("The config window will now always try and show your mouse when you open it. One limitation is that if you try to open the configs for multiple tweaks, your mouse state may be hidden when you close some of them. In this case, just close and reopen the config window for the relative tweaks.", smallLabelStyle);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
				config.SaveConfig();
			}
			GUI.DragWindow();
		}
	}
}