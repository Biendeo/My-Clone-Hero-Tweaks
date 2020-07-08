using AccuracyIndicator.Components;
using AccuracyIndicator.Settings;
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

namespace AccuracyIndicator {
	public class AccuracyIndicator : MonoBehaviour {
		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private BasePlayerWrapper[] basePlayers;
		private List<NoteWrapper> notes;

		private Font uiFont;

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

		private int totalNoteCount;
		private int currentNoteIndex;

		private double lastNoteHitTime;
		private float lastNoteHitDifference;
		private float lastNoteActualTime;

		private int hitNotes;
		private double hitAccuracy;
		private double lastSongTime;
		private List<float> noteHits;
		private float highestVeryLate;
		private float highestLate;
		private float highestSlightlyLate;
		private float highestSlightlyEarly;
		private float highestEarly;
		private float highestVeryEarly;

		private GameObject accuracyIndicatorLabel;
		private GameObject accuracyMessageLabel;
		private GameObject averageAccuracyLabel;

		private readonly VersionCheck VersionCheck;
		private Rect ChangelogRect;
		private string version;

		public AccuracyIndicator() {
			lastSongTime = -5.0;
			VersionCheck = new VersionCheck(187002999);
			ChangelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
			version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
		}

		#region Unity Methods

		private void Start() {
			config = Config.LoadConfig();
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		private void DestroyAndNullGameplayLabels() {
			if (accuracyIndicatorLabel != null) Destroy(accuracyIndicatorLabel);
			if (accuracyMessageLabel != null) Destroy(accuracyMessageLabel);
			if (averageAccuracyLabel != null) Destroy(averageAccuracyLabel);
			accuracyIndicatorLabel = null;
			accuracyMessageLabel = null;
			averageAccuracyLabel = null;
		}

		private void ResetGameplaySceneValues() {
			notes = basePlayers[0].Notes;
			currentNoteIndex = 0;
			totalNoteCount = notes?.Count ?? 0;
			lastNoteHitTime = -5.0;
			lastNoteHitDifference = 0.0f;
			lastNoteActualTime = 0.0f;
			hitNotes = 0;
			hitAccuracy = 0.0;
			noteHits = new List<float>();
			highestVeryLate = 0.0f;
			highestLate = 0.0f;
			highestSlightlyLate = 0.0f;
			highestSlightlyEarly = 0.0f;
			highestEarly = 0.0f;
			highestVeryEarly = 0.0f;
		}

		private void InstantiateEndOfSongLabels() {
			Transform canvasTransform = FadeBehaviourWrapper.instance.fadeGraphic.canvas.transform;

			foreach (var x in Enumerable.Range(0, 9)) {
				var gameObjects = new GameObject[3];
				var textComponents = new Text[3];
				foreach (var y in Enumerable.Range(0, 3)) {
					gameObjects[y] = new GameObject($"Accuracy Indicator Text Element {x}-{y}", new Type[] {
								typeof(Text),
								typeof(DestroyOnSceneChange)
							});

					gameObjects[y].layer = LayerMask.NameToLayer("UI");
					gameObjects[y].transform.SetParent(canvasTransform);
					gameObjects[y].transform.SetSiblingIndex(0);
					gameObjects[y].transform.localPosition = new Vector3(0.0f, 0.0f, x % 2 == 0 ? -100.0f : 100.0f);
					gameObjects[y].transform.localEulerAngles = new Vector3();
					gameObjects[y].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					textComponents[y] = gameObjects[y].GetComponent<Text>();
					textComponents[y].color = Color.white;
					textComponents[y].font = uiFont;
					textComponents[y].fontSize = Screen.height * 30 / 1440;
					textComponents[y].alignment = TextAnchor.MiddleRight;
					textComponents[y].fontStyle = FontStyle.Bold;
					textComponents[y].horizontalOverflow = HorizontalWrapMode.Overflow;
					textComponents[y].verticalOverflow = VerticalWrapMode.Overflow;
				}
				if (x == 0) {
					textComponents[0].text = $"Very early ({(highestVeryEarly * 1000.0f).ToString("0.00")}ms):";
					int veryEarlies = noteHits.Count(nh => nh < highestVeryEarly);
					textComponents[1].text = veryEarlies.ToString();
					textComponents[2].text = $"({(veryEarlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorVeryEarly.Color;
				} else if (x == 1) {
					textComponents[0].text = $"Early ({(highestEarly * 1000.0f).ToString("0.00")}ms):";
					int earlies = noteHits.Count(nh => nh < highestEarly && nh >= highestVeryEarly);
					textComponents[1].text = earlies.ToString();
					textComponents[2].text = $"({(earlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorEarly.Color;
				} else if (x == 2) {
					textComponents[0].text = $"Slightly early ({(highestSlightlyEarly * 1000.0f).ToString("0.00")}ms):";
					int slightlyEarlies = noteHits.Count(nh => nh < highestSlightlyEarly && nh >= highestEarly);
					textComponents[1].text = slightlyEarlies.ToString();
					textComponents[2].text = $"({(slightlyEarlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorSlightlyEarly.Color;
				} else if (x == 3) {
					textComponents[0].text = $"Perfect:";
					int perfects = noteHits.Count(nh => nh <= highestSlightlyLate && nh >= highestSlightlyEarly);
					textComponents[1].text = perfects.ToString();
					textComponents[2].text = $"({(perfects * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorPerfect.Color;
				} else if (x == 4) {
					textComponents[0].text = $"Slightly late ({(highestSlightlyLate * 1000.0f).ToString("0.00")}ms):";
					int slightlyLates = noteHits.Count(nh => nh <= highestLate && nh > highestSlightlyLate);
					textComponents[1].text = slightlyLates.ToString();
					textComponents[2].text = $"({(slightlyLates * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorSlightlyLate.Color;
				} else if (x == 5) {
					textComponents[0].text = $"Late ({(highestLate * 1000.0f).ToString("0.00")}ms):";
					int lates = noteHits.Count(nh => nh <= highestVeryLate && nh > highestLate);
					textComponents[1].text = lates.ToString();
					textComponents[2].text = $"({(lates * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorLate.Color;
				} else if (x == 6) {
					textComponents[0].text = $"Very late ({(highestVeryLate * 1000.0f).ToString("0.00")}ms):";
					int veryLates = noteHits.Count(nh => nh > highestVeryLate);
					textComponents[1].text = veryLates.ToString();
					textComponents[2].text = $"({(veryLates * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorVeryLate.Color;
				} else if (x == 7) {
					textComponents[0].text = $"Missed:";
					int misses = totalNoteCount - noteHits.Count();
					textComponents[1].text = misses.ToString();
					textComponents[2].text = $"({(misses * 100.0 / totalNoteCount).ToString("0.00")}%)";
					textComponents[0].color = config.ColorMissed.Color;
				} else if (x == 8) {
					textComponents[0].text = $"Average time:";
					textComponents[2].text = $"{(noteHits.Average() * 1000.0f).ToString("0.00")}ms";
					textComponents[0].color = config.ColorPerfect.Color;
					Destroy(textComponents[1]);
				}
				float height = Screen.height * 50.0f / 1440.0f - (x * 37.0f);
				gameObjects[0].transform.localPosition = new Vector3(Screen.width * 200.0f / 1440.0f - Screen.width / 2, height, 50);
				if (x < 8) gameObjects[1].transform.localPosition = new Vector3(Screen.width * 250.0f / 1440.0f - Screen.width / 2, height, 50);
				gameObjects[2].transform.localPosition = new Vector3(Screen.width * 350.0f / 1440.0f - Screen.width / 2, height, 50);
			}
		}

		private void UpdateGreatestThresholds() {
			highestVeryLate = Math.Max(highestVeryLate, config.CutoffVeryLate);
			highestLate = Math.Max(highestLate, config.CutoffLate);
			highestSlightlyLate = Math.Max(highestSlightlyLate, config.CutoffSlightlyLate);
			highestSlightlyEarly = Math.Min(highestSlightlyEarly, -config.CutoffSlightlyEarly);
			highestEarly = Math.Min(highestEarly, -config.CutoffEarly);
			highestVeryEarly = Math.Min(highestVeryEarly, -config.CutoffVeryEarly);
		}

		private void UpdateNotes() {
			while (currentNoteIndex < totalNoteCount && (notes[currentNoteIndex].WasHit || notes[currentNoteIndex].WasMissed)) {
				lastNoteActualTime = notes[currentNoteIndex].Time;
				lastNoteHitTime = gameManager.SongTime;
				if (notes[currentNoteIndex].WasHit) {
					if (lastNoteHitTime > lastNoteActualTime && (lastNoteHitTime - Time.deltaTime) < lastNoteActualTime) {
						lastNoteHitDifference = 0.0f;
					} else if (lastNoteHitTime > lastNoteActualTime) {
						lastNoteHitDifference = (float)(lastNoteHitTime - Time.deltaTime - lastNoteActualTime);
					} else if (lastNoteHitTime < lastNoteActualTime) {
						lastNoteHitDifference = (float)(lastNoteHitTime - lastNoteActualTime);
					} else {
						UnityEngine.Debug.LogError($"Panic?");
					}
					noteHits.Add(lastNoteHitDifference);
					if (hitNotes == 0) {
						hitAccuracy = lastNoteHitDifference;
					} else {
						hitAccuracy = (hitAccuracy * hitNotes + lastNoteHitDifference) / (hitNotes + 1);
					}
					++hitNotes;
				} else if (notes[currentNoteIndex].WasMissed) {
					lastNoteHitDifference = 0.07f;
				}
				++currentNoteIndex;
			}
		}

		private void UpdateLabels() {
			if (config.Enabled) {
				double timeFromLastNote = gameManager.SongTime - lastNoteHitTime;

				// Determine what color and message displays.
				Color labelColor = Color.white;
				string message = string.Empty;
				if (lastNoteHitDifference == 0.07f) {
					labelColor = config.ColorMissed.Color;
					message = "Missed";
				} else if (lastNoteHitDifference < -config.CutoffVeryEarly) {
					labelColor = config.ColorVeryEarly.Color;
					message = "Very Early";
				} else if (lastNoteHitDifference > config.CutoffVeryLate) {
					labelColor = config.ColorVeryLate.Color;
					message = "Very Late";
				} else if (lastNoteHitDifference < -config.CutoffEarly) {
					labelColor = config.ColorEarly.Color;
					message = "Early";
				} else if (lastNoteHitDifference > config.CutoffLate) {
					labelColor = config.ColorLate.Color;
					message = "Late";
				} else if (lastNoteHitDifference < -config.CutoffSlightlyEarly) {
					labelColor = config.ColorSlightlyEarly.Color;
					message = "Slightly Early!";
				} else if (lastNoteHitDifference > config.CutoffSlightlyLate) {
					labelColor = config.ColorSlightlyLate.Color;
					message = "Slightly Late!";
				} else {
					labelColor = config.ColorPerfect.Color;
					message = "Perfect!";
				}

				// Set the colors and messages of the labels.
				if (config.AccuracyTime.Visible && (config.LayoutTest || timeFromLastNote < config.TimeOnScreen)) {
					accuracyIndicatorLabel.transform.localPosition = new Vector3(config.AccuracyTime.X - Screen.width / 2, Screen.height / 2 - config.AccuracyTime.Y);
					var text = accuracyIndicatorLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.AccuracyTime.Size;
					text.alignment = config.AccuracyTime.Alignment;
					text.fontStyle = (config.AccuracyTime.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.AccuracyTime.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{(lastNoteHitDifference * 1000.0f).ToString("0.00")}ms";
					text.color = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)));
				} else {
					accuracyIndicatorLabel.GetComponent<Text>().enabled = false;
				}

				if (config.AccuracyMessage.Visible && (config.LayoutTest || timeFromLastNote < config.TimeOnScreen)) {
					accuracyMessageLabel.transform.localPosition = new Vector3(config.AccuracyMessage.X - Screen.width / 2, Screen.height / 2 - config.AccuracyMessage.Y);
					var text = accuracyMessageLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.AccuracyMessage.Size;
					text.alignment = config.AccuracyMessage.Alignment;
					text.fontStyle = (config.AccuracyMessage.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.AccuracyMessage.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = message;
					text.color = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)));
				} else {
					accuracyMessageLabel.GetComponent<Text>().enabled = false;
				}

				if (config.AverageAccuracy.Visible && (config.LayoutTest || timeFromLastNote < config.TimeOnScreen)) {
					averageAccuracyLabel.transform.localPosition = new Vector3(config.AverageAccuracy.X - Screen.width / 2, Screen.height / 2 - config.AverageAccuracy.Y);
					var text = averageAccuracyLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.AverageAccuracy.Size;
					text.alignment = config.AverageAccuracy.Alignment;
					text.fontStyle = (config.AverageAccuracy.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.AverageAccuracy.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{(hitAccuracy * 1000.0f).ToString("0.00")}ms";
					text.color = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)));
				} else {
					averageAccuracyLabel.GetComponent<Text>().enabled = false;
				}
			}
		}

		private void LateUpdate() {
			string scene = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (scene.Equals("Gameplay")) {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					basePlayers = gameManager.BasePlayers;
					ResetGameplaySceneValues();

					DestroyAndNullGameplayLabels();
					Transform canvasTransform = FadeBehaviourWrapper.instance.fadeGraphic.canvas.transform;

					accuracyIndicatorLabel = new GameObject($"Accuracy Indicator", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					accuracyIndicatorLabel.layer = LayerMask.NameToLayer("UI");
					accuracyIndicatorLabel.transform.SetParent(canvasTransform);
					accuracyIndicatorLabel.transform.SetSiblingIndex(0);
					accuracyIndicatorLabel.transform.localEulerAngles = new Vector3();
					accuracyIndicatorLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					accuracyIndicatorLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					accuracyIndicatorLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					accuracyIndicatorLabel.GetComponent<Text>().font = uiFont;

					accuracyMessageLabel = new GameObject($"Accuracy Message", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					accuracyMessageLabel.layer = LayerMask.NameToLayer("UI");
					accuracyMessageLabel.transform.SetParent(canvasTransform);
					accuracyMessageLabel.transform.SetSiblingIndex(0);
					accuracyMessageLabel.transform.localEulerAngles = new Vector3();
					accuracyMessageLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					accuracyMessageLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					accuracyMessageLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					accuracyMessageLabel.GetComponent<Text>().font = uiFont;

					averageAccuracyLabel = new GameObject($"Average Accuracy", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					averageAccuracyLabel.layer = LayerMask.NameToLayer("UI");
					averageAccuracyLabel.transform.SetParent(canvasTransform);
					averageAccuracyLabel.transform.SetSiblingIndex(0);
					averageAccuracyLabel.transform.localEulerAngles = new Vector3();
					averageAccuracyLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					averageAccuracyLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					averageAccuracyLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					averageAccuracyLabel.GetComponent<Text>().font = uiFont;
				} else {
					DestroyAndNullGameplayLabels();
				}
				if (config.Enabled && scene.Equals("EndOfSong")) {
					InstantiateEndOfSongLabels();
				}
			}
			if (scene.Equals("Gameplay")) {
				//! In practice mode, the song time is set to 1.5s before the section or A/B. If it is looping, it is
				//! initially set to 0, then to the appropriate time. As long as the user isn't on less than 10FPS, this should work.
				if (Math.Abs(gameManager.SongTime - lastSongTime) > 1.5 && gameManager.PracticeUI.practiceUI != null) {
					ResetGameplaySceneValues();
				}
				UpdateGreatestThresholds();
				if (notes != null) {
					UpdateNotes();
				}
				UpdateLabels();
			} else if (scene.Equals("Main Menu")) {
				if (uiFont is null) {
					//TODO: Get the font directly from the bundle?
					uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
				}
				if (!VersionCheck.HasVersionBeenChecked) {
					if (config.SilenceUpdates) {
						VersionCheck.HasVersionBeenChecked = true;
					} else {
						string detectedVersion = GlobalVariablesWrapper.instance.buildVersion;
						VersionCheck.CheckVersion(detectedVersion);
					}
				}
			}
			config.HandleInput();
			if (gameManager != null) {
				lastSongTime = gameManager.SongTime;
			}
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
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(187002001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Accuracy Indicator Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (VersionCheck.IsShowingUpdateWindow) {
				VersionCheck.DrawUpdateWindow(settingsWindowStyle, settingsLabelStyle, settingsButtonStyle);
			}
			if (!config.SeenChangelog && config.TweakVersion != version) {
				ChangelogRect = GUILayout.Window(187002998, ChangelogRect, OnChangelogWindow, new GUIContent($"Perfect Mode Changelog"), settingsWindowStyle);
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

			GUILayout.Label($"Accuracy Indicator v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
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
			GUILayout.Label("Thankyou for downloading Accuracy Indicator!", largeLabelStyle);
			GUILayout.Label("When you enter gameplay, you should see the accuracy indicator appear each time you hit a note. You can press F7 to enable/disable this feature.", smallLabelStyle);
			GUILayout.Label("Press Ctrl + Shift + F7 to enable/disable the config window.", smallLabelStyle);
			GUILayout.Label("The config window lets you change things such as the keys to enable/disable the mode, the leniency of the different accuracy ratings, and the layout of the UI.", smallLabelStyle);
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