using AccuracyIndicator.Settings;
using Common.Wrappers;
using System;
using System.Collections.Generic;
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

		private bool configWindowEnabled;
		private readonly FileInfo configFilePath;
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

		public AccuracyIndicator() {
			configWindowEnabled = false;
			configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "AccuracyIndicatorConfig.xml"));
			lastSongTime = -5.0;
		}

		private void WriteDefaultConfig() {
			config = new Config();
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.OpenWrite()) {
				serializer.Serialize(configOut, config);
			}
		}

		#region Unity Methods

		void Start() {
			if (!configFilePath.Exists) {
				WriteDefaultConfig();
			} else {
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = configFilePath.OpenRead()) {
					config = serializer.Deserialize(configIn) as Config;
				}
			}
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					basePlayers = gameManager.BasePlayers;
					notes = basePlayers[0].Notes;
					currentNoteIndex = 0;
					totalNoteCount = notes?.Count ?? 0;
					lastNoteHitTime = -5.0;
					lastNoteHitDifference = 0.0f;
					lastNoteActualTime = 0.0f;
					hitNotes = 0;
					hitAccuracy = 0.0;
				}
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
				//! In practice mode, the song time is set to 1.5s before the section or A/B. If it is looping, it is
				//! initially set to 0, then to the appropriate time. As long as the user isn't on less than 10FPS, this should work.
				if (Math.Abs(gameManager.SongTime - lastSongTime) > 0.1) {
					notes = basePlayers[0].Notes;
					currentNoteIndex = 0;
					totalNoteCount = notes?.Count ?? 0;
					lastNoteHitTime = -5.0;
					lastNoteHitDifference = 0.0f;
					lastNoteActualTime = 0.0f;
					hitNotes = 0;
					hitAccuracy = 0.0;
				}
				if (notes != null) {
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
								Debug.LogError($"Panic?");
							}
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
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			if (Input.GetKeyDown(KeyCode.F7) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
				configWindowEnabled = !configWindowEnabled;
			}
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
			if (configWindowEnabled) {
				var outputRect = GUILayout.Window(5318010, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Accuracy Indicator Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && notes != null) {
				if (config.Enabled) {
					Color labelColor = Color.white;
					string message = string.Empty;
					if (lastNoteHitDifference < -config.CutoffVeryEarly) {
						labelColor = Config.ARGBToColor(config.ColorVeryEarlyARGB);
						message = "Very Early";
					} else if (lastNoteHitDifference < -config.CutoffEarly) {
						labelColor = Config.ARGBToColor(config.ColorEarlyARGB);
						message = "Early";
					} else if (lastNoteHitTime < -config.CutoffSlightlyEarly) {
						labelColor = Config.ARGBToColor(config.ColorSlightlyEarlyARGB);
						message = "Slightly Early!";
					} else if (lastNoteHitDifference == 0.07f) {
						labelColor = Config.ARGBToColor(config.ColorMissedARGB);
						message = "Missed";
					} else if (lastNoteHitDifference > config.CutoffVeryLate) {
						labelColor = Config.ARGBToColor(config.ColorVeryLateARGB);
						message = "Very Late";
					} else if (lastNoteHitDifference > config.CutoffLate) {
						labelColor = Config.ARGBToColor(config.ColorLateARGB);
						message = "Late";
					} else if (lastNoteHitDifference > config.CutoffSlightlyLate) {
						labelColor = Config.ARGBToColor(config.ColorSlightlyLateARGB);
						message = "Slightly Late!";
					} else {
						labelColor = Config.ARGBToColor(config.ColorPerfectARGB);
						message = "Perfect!";
					}
					if (config.AccuracyTime) GUI.Label(new Rect(config.AccuracyTimeX, config.AccuracyTimeY, 0.1f, 0.1f), new GUIContent($"{(lastNoteHitDifference * 1000.0f).ToString("0.00")}ms"), new GUIStyle {
						fontSize = config.AccuracyTimeScale,
						alignment = TextAnchor.MiddleCenter,
						fontStyle = (config.AccuracyTimeBold ? FontStyle.Bold : FontStyle.Normal) | (config.AccuracyTimeItalic ? FontStyle.Italic : FontStyle.Normal),
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						}
					});
					if (config.AccuracyMessage) GUI.Label(new Rect(config.AccuracyMessageX, config.AccuracyMessageY, 0.1f, 0.1f), new GUIContent(message), new GUIStyle {
						fontSize = config.AccuracyMessageScale,
						alignment = TextAnchor.MiddleCenter,
						fontStyle = (config.AccuracyMessageBold ? FontStyle.Bold : FontStyle.Normal) | (config.AccuracyMessageItalic ? FontStyle.Italic : FontStyle.Normal),
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						}
					});
					if (config.AverageAccuracy) GUI.Label(new Rect(config.AverageAccuracyX, config.AverageAccuracyY, 0.1f, 0.1f), new GUIContent($"{(hitAccuracy * 1000.0).ToString("0.00")}ms"), new GUIStyle {
						fontSize = config.AverageAccuracyScale,
						alignment = TextAnchor.MiddleCenter,
						fontStyle = (config.AverageAccuracyBold ? FontStyle.Bold : FontStyle.Normal) | (config.AverageAccuracyItalic ? FontStyle.Italic : FontStyle.Normal),
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						}
					});
				}
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

			GUILayout.Label("Settings", largeLabelStyle);
			config.Enabled = GUILayout.Toggle(config.Enabled, "Enabled", settingsToggleStyle);
			config.LayoutTest = GUILayout.Toggle(config.LayoutTest, "Test Layout (for helping you)", settingsToggleStyle);
			GUILayout.Label("Time on-screen", smallLabelStyle);
			config.TimeOnScreen = GUILayout.HorizontalSlider(config.TimeOnScreen, 0.0f, 5.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.TimeOnScreen.ToString(), settingsTextFieldStyle), out float timeOnScreen)) config.TimeOnScreen = timeOnScreen;

			GUILayout.Space(25.0f);
			GUILayout.Label("Accuracy Time Indicator", largeLabelStyle);
			config.AccuracyTime = GUILayout.Toggle(config.AccuracyTime, "Enabled", settingsToggleStyle);
			GUILayout.Label("X", smallLabelStyle);
			config.AccuracyTimeX = GUILayout.HorizontalSlider(config.AccuracyTimeX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AccuracyTimeX.ToString(), settingsTextFieldStyle), out float accuracyTimeX)) config.AccuracyTimeX = accuracyTimeX;
			GUILayout.Label("Y", smallLabelStyle);
			config.AccuracyTimeY = GUILayout.HorizontalSlider(config.AccuracyTimeY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AccuracyTimeY.ToString(), settingsTextFieldStyle), out float accuracyTimeY)) config.AccuracyTimeY = accuracyTimeY;
			GUILayout.Label("Scale", smallLabelStyle);
			config.AccuracyTimeScale = (int)GUILayout.HorizontalSlider(config.AccuracyTimeScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.AccuracyTimeScale.ToString(), settingsTextFieldStyle), out int accuracyTimeScale)) config.AccuracyTimeScale = accuracyTimeScale;
			config.AccuracyTimeBold = GUILayout.Toggle(config.AccuracyTimeBold, "Bold", settingsToggleStyle);
			config.AccuracyTimeItalic = GUILayout.Toggle(config.AccuracyTimeItalic, "Italic", settingsToggleStyle);

			GUILayout.Space(25.0f);
			GUILayout.Label("Accuracy Message Indicator", largeLabelStyle);
			config.AccuracyMessage = GUILayout.Toggle(config.AccuracyMessage, "Enabled", settingsToggleStyle);
			GUILayout.Label("X", smallLabelStyle);
			config.AccuracyMessageX = GUILayout.HorizontalSlider(config.AccuracyMessageX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AccuracyMessageX.ToString(), settingsTextFieldStyle), out float accuracyMessageX)) config.AccuracyMessageX = accuracyMessageX;
			GUILayout.Label("Y", smallLabelStyle);
			config.AccuracyMessageY = GUILayout.HorizontalSlider(config.AccuracyMessageY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AccuracyMessageY.ToString(), settingsTextFieldStyle), out float accuracyMessageY)) config.AccuracyMessageY = accuracyMessageY;
			GUILayout.Label("Scale", smallLabelStyle);
			config.AccuracyMessageScale = (int)GUILayout.HorizontalSlider(config.AccuracyMessageScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.AccuracyMessageScale.ToString(), settingsTextFieldStyle), out int accuracyMessageScale)) config.AccuracyMessageScale = accuracyMessageScale;
			config.AccuracyMessageBold = GUILayout.Toggle(config.AccuracyMessageBold, "Bold", settingsToggleStyle);
			config.AccuracyMessageItalic = GUILayout.Toggle(config.AccuracyMessageItalic, "Italic", settingsToggleStyle);

			GUILayout.Space(25.0f);
			GUILayout.Label("Average Accuracy Indicator", largeLabelStyle);
			GUILayout.Label("(this is mostly for testing latency)", new GUIStyle(smallLabelStyle) {
				fontStyle = FontStyle.Italic
			});
			config.AverageAccuracy = GUILayout.Toggle(config.AverageAccuracy, "Enabled", settingsToggleStyle);
			GUILayout.Label("X", smallLabelStyle);
			config.AverageAccuracyX = GUILayout.HorizontalSlider(config.AverageAccuracyX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AverageAccuracyX.ToString(), settingsTextFieldStyle), out float averageAccuracyX)) config.AverageAccuracyX = averageAccuracyX;
			GUILayout.Label("Y", smallLabelStyle);
			config.AverageAccuracyY = GUILayout.HorizontalSlider(config.AverageAccuracyY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.AverageAccuracyY.ToString(), settingsTextFieldStyle), out float averageAccuracyY)) config.AverageAccuracyY = averageAccuracyY;
			GUILayout.Label("Scale", smallLabelStyle);
			config.AverageAccuracyScale = (int)GUILayout.HorizontalSlider(config.AverageAccuracyScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.AverageAccuracyScale.ToString(), settingsTextFieldStyle), out int averageAccuracyScale)) config.AverageAccuracyScale = averageAccuracyScale;
			config.AverageAccuracyBold = GUILayout.Toggle(config.AverageAccuracyBold, "Bold", settingsToggleStyle);
			config.AverageAccuracyItalic = GUILayout.Toggle(config.AverageAccuracyItalic, "Italic", settingsToggleStyle);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Perfect", largeLabelStyle);
			var perfectColor = Config.ARGBToColor(config.ColorPerfectARGB);
			GUILayout.Label("Red", smallLabelStyle);
			perfectColor.r = GUILayout.HorizontalSlider(perfectColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(perfectColor.r.ToString(), settingsTextFieldStyle), out float perfectR)) perfectColor.r = perfectR;
			GUILayout.Label("Green", smallLabelStyle);
			perfectColor.g = GUILayout.HorizontalSlider(perfectColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(perfectColor.g.ToString(), settingsTextFieldStyle), out float perfectG)) perfectColor.g = perfectG;
			GUILayout.Label("Blue", smallLabelStyle);
			perfectColor.b = GUILayout.HorizontalSlider(perfectColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(perfectColor.b.ToString(), settingsTextFieldStyle), out float perfectB)) perfectColor.b = perfectB;
			GUILayout.Label("Alpha", smallLabelStyle);
			perfectColor.a = GUILayout.HorizontalSlider(perfectColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(perfectColor.a.ToString(), settingsTextFieldStyle), out float perfectA)) perfectColor.a = perfectA;
			config.ColorPerfectARGB = Config.ColorToARGB(perfectColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Missed", largeLabelStyle);
			var missedColor = Config.ARGBToColor(config.ColorMissedARGB);
			GUILayout.Label("Red", smallLabelStyle);
			missedColor.r = GUILayout.HorizontalSlider(missedColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(missedColor.r.ToString(), settingsTextFieldStyle), out float missedR)) missedColor.r = missedR;
			GUILayout.Label("Green", smallLabelStyle);
			missedColor.g = GUILayout.HorizontalSlider(missedColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(missedColor.g.ToString(), settingsTextFieldStyle), out float missedG)) missedColor.g = missedG;
			GUILayout.Label("Blue", smallLabelStyle);
			missedColor.b = GUILayout.HorizontalSlider(missedColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(missedColor.b.ToString(), settingsTextFieldStyle), out float missedB)) missedColor.b = missedB;
			GUILayout.Label("Alpha", smallLabelStyle);
			missedColor.a = GUILayout.HorizontalSlider(missedColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(missedColor.a.ToString(), settingsTextFieldStyle), out float missedA)) missedColor.a = missedA;
			config.ColorMissedARGB = Config.ColorToARGB(missedColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Slightly Early", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffSlightlyEarly = GUILayout.HorizontalSlider(config.CutoffSlightlyEarly, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffSlightlyEarly.ToString(), settingsTextFieldStyle), out float cutoffSlightlyEarly)) config.CutoffSlightlyEarly = cutoffSlightlyEarly;
			var slightlyEarlyColor = Config.ARGBToColor(config.ColorSlightlyEarlyARGB);
			GUILayout.Label("Red", smallLabelStyle);
			slightlyEarlyColor.r = GUILayout.HorizontalSlider(slightlyEarlyColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyEarlyColor.r.ToString(), settingsTextFieldStyle), out float slightlyEarlyR)) slightlyEarlyColor.r = slightlyEarlyR;
			GUILayout.Label("Green", smallLabelStyle);
			slightlyEarlyColor.g = GUILayout.HorizontalSlider(slightlyEarlyColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyEarlyColor.g.ToString(), settingsTextFieldStyle), out float slightlyEarlyG)) slightlyEarlyColor.g = slightlyEarlyG;
			GUILayout.Label("Blue", smallLabelStyle);
			slightlyEarlyColor.b = GUILayout.HorizontalSlider(slightlyEarlyColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyEarlyColor.b.ToString(), settingsTextFieldStyle), out float slightlyEarlyB)) slightlyEarlyColor.b = slightlyEarlyB;
			GUILayout.Label("Alpha", smallLabelStyle);
			slightlyEarlyColor.a = GUILayout.HorizontalSlider(slightlyEarlyColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyEarlyColor.a.ToString(), settingsTextFieldStyle), out float slightlyEarlyA)) slightlyEarlyColor.a = slightlyEarlyA;
			config.ColorSlightlyEarlyARGB = Config.ColorToARGB(slightlyEarlyColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Early", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffEarly = GUILayout.HorizontalSlider(config.CutoffEarly, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffEarly.ToString(), settingsTextFieldStyle), out float cutoffEarly)) config.CutoffEarly = cutoffEarly;
			var earlyColor = Config.ARGBToColor(config.ColorEarlyARGB);
			GUILayout.Label("Red", smallLabelStyle);
			earlyColor.r = GUILayout.HorizontalSlider(earlyColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(earlyColor.r.ToString(), settingsTextFieldStyle), out float earlyR)) earlyColor.r = earlyR;
			GUILayout.Label("Green", smallLabelStyle);
			earlyColor.g = GUILayout.HorizontalSlider(earlyColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(earlyColor.g.ToString(), settingsTextFieldStyle), out float earlyG)) earlyColor.g = earlyG;
			GUILayout.Label("Blue", smallLabelStyle);
			earlyColor.b = GUILayout.HorizontalSlider(earlyColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(earlyColor.b.ToString(), settingsTextFieldStyle), out float earlyB)) earlyColor.b = earlyB;
			GUILayout.Label("Alpha", smallLabelStyle);
			earlyColor.a = GUILayout.HorizontalSlider(earlyColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(earlyColor.a.ToString(), settingsTextFieldStyle), out float earlyA)) earlyColor.a = earlyA;
			config.ColorEarlyARGB = Config.ColorToARGB(earlyColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Very Early", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffVeryEarly = GUILayout.HorizontalSlider(config.CutoffVeryEarly, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffVeryEarly.ToString(), settingsTextFieldStyle), out float cutoffVeryEarly)) config.CutoffVeryEarly = cutoffVeryEarly;
			var veryEarlyColor = Config.ARGBToColor(config.ColorVeryEarlyARGB);
			GUILayout.Label("Red", smallLabelStyle);
			veryEarlyColor.r = GUILayout.HorizontalSlider(veryEarlyColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryEarlyColor.r.ToString(), settingsTextFieldStyle), out float veryEarlyR)) veryEarlyColor.r = veryEarlyR;
			GUILayout.Label("Green", smallLabelStyle);
			veryEarlyColor.g = GUILayout.HorizontalSlider(veryEarlyColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryEarlyColor.g.ToString(), settingsTextFieldStyle), out float veryEarlyG)) veryEarlyColor.g = veryEarlyG;
			GUILayout.Label("Blue", smallLabelStyle);
			veryEarlyColor.b = GUILayout.HorizontalSlider(veryEarlyColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryEarlyColor.b.ToString(), settingsTextFieldStyle), out float veryEarlyB)) veryEarlyColor.b = veryEarlyB;
			GUILayout.Label("Alpha", smallLabelStyle);
			veryEarlyColor.a = GUILayout.HorizontalSlider(veryEarlyColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryEarlyColor.a.ToString(), settingsTextFieldStyle), out float veryEarlyA)) veryEarlyColor.a = veryEarlyA;
			config.ColorVeryEarlyARGB = Config.ColorToARGB(veryEarlyColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Slightly Late", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffSlightlyLate = GUILayout.HorizontalSlider(config.CutoffSlightlyLate, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffSlightlyLate.ToString(), settingsTextFieldStyle), out float cutoffSlightlyLate)) config.CutoffSlightlyLate = cutoffSlightlyLate;
			var slightlyLateColor = Config.ARGBToColor(config.ColorSlightlyLateARGB);
			GUILayout.Label("Red", smallLabelStyle);
			slightlyLateColor.r = GUILayout.HorizontalSlider(slightlyLateColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyLateColor.r.ToString(), settingsTextFieldStyle), out float slightlyLateR)) slightlyLateColor.r = slightlyLateR;
			GUILayout.Label("Green", smallLabelStyle);
			slightlyLateColor.g = GUILayout.HorizontalSlider(slightlyLateColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyLateColor.g.ToString(), settingsTextFieldStyle), out float slightlyLateG)) slightlyLateColor.g = slightlyLateG;
			GUILayout.Label("Blue", smallLabelStyle);
			slightlyLateColor.b = GUILayout.HorizontalSlider(slightlyLateColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyLateColor.b.ToString(), settingsTextFieldStyle), out float slightlyLateB)) slightlyLateColor.b = slightlyLateB;
			GUILayout.Label("Alpha", smallLabelStyle);
			slightlyLateColor.a = GUILayout.HorizontalSlider(slightlyLateColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(slightlyLateColor.a.ToString(), settingsTextFieldStyle), out float slightlyLateA)) slightlyLateColor.a = slightlyLateA;
			config.ColorSlightlyLateARGB = Config.ColorToARGB(slightlyLateColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Late", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffLate = GUILayout.HorizontalSlider(config.CutoffLate, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffLate.ToString(), settingsTextFieldStyle), out float cutoffLate)) config.CutoffLate = cutoffLate;
			var lateColor = Config.ARGBToColor(config.ColorLateARGB);
			GUILayout.Label("Red", smallLabelStyle);
			lateColor.r = GUILayout.HorizontalSlider(lateColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(lateColor.r.ToString(), settingsTextFieldStyle), out float lateR)) lateColor.r = lateR;
			GUILayout.Label("Green", smallLabelStyle);
			lateColor.g = GUILayout.HorizontalSlider(lateColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(lateColor.g.ToString(), settingsTextFieldStyle), out float lateG)) lateColor.g = lateG;
			GUILayout.Label("Blue", smallLabelStyle);
			lateColor.b = GUILayout.HorizontalSlider(lateColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(lateColor.b.ToString(), settingsTextFieldStyle), out float lateB)) lateColor.b = lateB;
			GUILayout.Label("Alpha", smallLabelStyle);
			lateColor.a = GUILayout.HorizontalSlider(lateColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(lateColor.a.ToString(), settingsTextFieldStyle), out float lateA)) lateColor.a = lateA;
			config.ColorLateARGB = Config.ColorToARGB(lateColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Colors - Very Late", largeLabelStyle);
			GUILayout.Label("Cutoff time", smallLabelStyle);
			config.CutoffVeryLate = GUILayout.HorizontalSlider(config.CutoffVeryLate, 0.0f, 0.07f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.CutoffVeryLate.ToString(), settingsTextFieldStyle), out float cutoffVeryLate)) config.CutoffVeryLate = cutoffVeryLate;
			var veryLateColor = Config.ARGBToColor(config.ColorVeryLateARGB);
			GUILayout.Label("Red", smallLabelStyle);
			veryLateColor.r = GUILayout.HorizontalSlider(veryLateColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryLateColor.r.ToString(), settingsTextFieldStyle), out float veryLateR)) veryLateColor.r = veryLateR;
			GUILayout.Label("Green", smallLabelStyle);
			veryLateColor.g = GUILayout.HorizontalSlider(veryLateColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryLateColor.g.ToString(), settingsTextFieldStyle), out float veryLateG)) veryLateColor.g = veryLateG;
			GUILayout.Label("Blue", smallLabelStyle);
			veryLateColor.b = GUILayout.HorizontalSlider(veryLateColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryLateColor.b.ToString(), settingsTextFieldStyle), out float veryLateB)) veryLateColor.b = veryLateB;
			GUILayout.Label("Alpha", smallLabelStyle);
			veryLateColor.a = GUILayout.HorizontalSlider(veryLateColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(veryLateColor.a.ToString(), settingsTextFieldStyle), out float veryLateA)) veryLateColor.a = veryLateA;
			config.ColorVeryLateARGB = Config.ColorToARGB(veryLateColor);




			GUILayout.Space(25.0f);
			if (GUILayout.Button("Save Config", settingsButtonStyle)) {
				if (configFilePath.Exists) configFilePath.Delete();
				var serializer = new XmlSerializer(typeof(Config));
				using (var configOut = configFilePath.OpenWrite()) {
					serializer.Serialize(configOut, config);
				}
			}
			GUILayout.Space(25.0f);

			GUILayout.Label($"Accuracy Indicator v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUILayout.EndScrollView();
			GUI.DragWindow();
		}
	}
}