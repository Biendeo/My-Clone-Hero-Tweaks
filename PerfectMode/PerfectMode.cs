using Common.Wrappers;
using PerfectMode.Settings;
using System;
using System.Collections.Generic;
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

		private string target;
		private bool isStillFC;
		private int missedNotes;
		private int totalNoteCount;
		private int currentNoteIndex;


		public PerfectMode() {
			configWindowEnabled = false;
			configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "PerfectModeConfig.xml"));
			settingsScrollPosition = new Vector2();
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
				failedObjective = false;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					notes = gameManager.BasePlayers[0].Notes;
					totalNoteCount = notes.Count;
					currentNoteIndex = 0;
					missedNotes = 0;
				}
			}
			if (config.Enabled) {
				if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null && !failedObjective) {
					target = config.FC ? "FC" : (config.NotesMissed == 0 ? "100%" : $"-{config.NotesMissed}");
					isStillFC = !gameManager.BasePlayers[0].FirstNoteMissed;
					while (currentNoteIndex < totalNoteCount && (notes[currentNoteIndex].WasHit || notes[currentNoteIndex].WasMissed)) {
						if (!notes[currentNoteIndex].WasHit && notes[currentNoteIndex].WasMissed) {
							++missedNotes;
						}
						++currentNoteIndex;
					}
					if (config.FC && !isStillFC || config.NotesMissed < missedNotes) {
						failedObjective = true;
						remainingTimeBeforeRestart = Math.Min(config.FailDelay, (float)(gameManager.SongLength - gameManager.SongTime));
					}
				}
				if (failedObjective && (gameManager.PauseMenu is null || !gameManager.PauseMenu.activeInHierarchy)) {
					remainingTimeBeforeRestart -= Time.deltaTime;
					if (remainingTimeBeforeRestart < 0.0f) {
						//TODO: I can guarantee there's more going on that just this. Multiplayer is a concern.
						SceneManager.LoadScene("Gameplay");
					}
				}
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			if (Input.GetKeyDown(KeyCode.F6) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
				configWindowEnabled = !configWindowEnabled;
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
				//TODO: Look into why the GUILayout just panics if this and Extra Song UI are both loaded.
				var outputRect = GUILayout.Window(5318009, new Rect(config.ConfigX, config.ConfigY, 250.0f, 500.0f), OnWindow, new GUIContent("Perfect Mode Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}

			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null) {
				if (config.Enabled) {
					if (config.DisplayImage) {
						var displayImageStyle = new GUIStyle {
							font = uiFont,
							fontSize = config.DisplayImageScale,
							fontStyle = (config.DisplayImageBold ? FontStyle.Bold : FontStyle.Normal) | (config.DisplayImageItalic ? FontStyle.Italic : FontStyle.Normal),
							alignment = TextAnchor.UpperLeft,
							normal = new GUIStyleState {
								textColor = Config.ARGBToColor(config.DisplayImageColorARGB)
							}
						};
						GUI.Label(new Rect(config.DisplayImageX, config.DisplayImageY, 0.1f, 0.1f), new GUIContent($"{target} mode active"), displayImageStyle);
					}
					if (config.RemainingNotesLeft && !config.FC) {
						var remainingNotesStyle = new GUIStyle {
							font = uiFont,
							fontSize = config.RemainingNotesLeftScale,
							fontStyle = (config.RemainingNotesLeftBold ? FontStyle.Bold : FontStyle.Normal) | (config.RemainingNotesLeftItalic ? FontStyle.Italic : FontStyle.Normal),
							alignment = TextAnchor.UpperLeft,
							normal = new GUIStyleState {
								textColor = Config.ARGBToColor(config.RemainingNotesLeftColorARGB)
							}
						};
						GUI.Label(new Rect(config.RemainingNotesLeftX, config.RemainingNotesLeftY, 0.1f, 0.1f), new GUIContent(config.NotesMissed - missedNotes >= 0 ? $"{config.NotesMissed - missedNotes} note{(config.NotesMissed - missedNotes == 1 ? string.Empty : "s")} can be missed" : $"Too many notes missed"), remainingNotesStyle);
					}
					if (failedObjective) {
						var failedStyle = new GUIStyle {
							fontSize =  Screen.height / 30,
							alignment = TextAnchor.MiddleCenter,
							fontStyle = FontStyle.Bold,
							normal = new GUIStyleState {
								textColor = new Color(1.0f, 1.0f, 1.0f, Math.Min((config.FailDelay - remainingTimeBeforeRestart) * 2.0f, 1.0f)),
							}
						};
						GUI.Label(new Rect(Screen.width / 2, Screen.height / 5 * 4, 0.1f, 0.1f), new GUIContent($"{target} failed, restarting in {(int)remainingTimeBeforeRestart + 1}"), failedStyle);
					}
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
			config.FC = GUILayout.Toggle(config.FC, "FC Mode", settingsToggleStyle);
			GUILayout.Label("Note Miss Limit (inclusive)", smallLabelStyle);
			config.NotesMissed = (int)GUILayout.HorizontalSlider(config.NotesMissed, 0.0f, 100.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.NotesMissed.ToString(), settingsTextFieldStyle), out int notesMissed)) config.NotesMissed = notesMissed;
			GUILayout.Label("Fail Delay Before Restart", smallLabelStyle);
			config.FailDelay = GUILayout.HorizontalSlider(config.FailDelay, 0.0f, 10.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.FailDelay.ToString(), settingsTextFieldStyle), out float failDelay)) config.FailDelay = failDelay;

			GUILayout.Space(25.0f);
			GUILayout.Label("Indicator", largeLabelStyle);
			config.DisplayImage = GUILayout.Toggle(config.DisplayImage, "On-screen indicator enabled", settingsToggleStyle);
			GUILayout.Label("Indicator X", smallLabelStyle);
			config.DisplayImageX = GUILayout.HorizontalSlider(config.DisplayImageX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.DisplayImageX.ToString(), settingsTextFieldStyle), out float displayImageX)) config.DisplayImageX = displayImageX;
			GUILayout.Label("Indicator Y", smallLabelStyle);
			config.DisplayImageY = GUILayout.HorizontalSlider(config.DisplayImageY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.DisplayImageY.ToString(), settingsTextFieldStyle), out float displayImageY)) config.DisplayImageY = displayImageY;
			GUILayout.Label("Indicator Scale", smallLabelStyle);
			config.DisplayImageScale = (int)GUILayout.HorizontalSlider(config.DisplayImageScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.DisplayImageScale.ToString(), settingsTextFieldStyle), out int displayImageScale)) config.DisplayImageScale = displayImageScale;
			config.DisplayImageBold = GUILayout.Toggle(config.DisplayImageBold, "Bold", settingsToggleStyle);
			config.DisplayImageItalic = GUILayout.Toggle(config.DisplayImageItalic, "Italic", settingsToggleStyle);
			var displayImageColor = Config.ARGBToColor(config.DisplayImageColorARGB);
			GUILayout.Label("Red", smallLabelStyle);
			displayImageColor.r = GUILayout.HorizontalSlider(displayImageColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(displayImageColor.r.ToString(), settingsTextFieldStyle), out float displayImageR)) displayImageColor.r = displayImageR;
			GUILayout.Label("Green", smallLabelStyle);
			displayImageColor.g = GUILayout.HorizontalSlider(displayImageColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(displayImageColor.g.ToString(), settingsTextFieldStyle), out float displayImageG)) displayImageColor.g = displayImageG;
			GUILayout.Label("Blue", smallLabelStyle);
			displayImageColor.b = GUILayout.HorizontalSlider(displayImageColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(displayImageColor.b.ToString(), settingsTextFieldStyle), out float displayImageB)) displayImageColor.b = displayImageB;
			GUILayout.Label("Alpha", smallLabelStyle);
			displayImageColor.a = GUILayout.HorizontalSlider(displayImageColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(displayImageColor.a.ToString(), settingsTextFieldStyle), out float displayImageA)) displayImageColor.a = displayImageA;
			config.DisplayImageColorARGB = Config.ColorToARGB(displayImageColor);

			GUILayout.Space(25.0f);
			GUILayout.Label("Remaining indicator", largeLabelStyle);
			config.RemainingNotesLeft = GUILayout.Toggle(config.RemainingNotesLeft, "Remaining indicator enabled", settingsToggleStyle);
			GUILayout.Label("Indicator X", smallLabelStyle);
			config.RemainingNotesLeftX = GUILayout.HorizontalSlider(config.RemainingNotesLeftX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.RemainingNotesLeftX.ToString(), settingsTextFieldStyle), out float remainingNotesLeftX)) config.RemainingNotesLeftX = remainingNotesLeftX;
			GUILayout.Label("Indicator Y", smallLabelStyle);
			config.RemainingNotesLeftY = GUILayout.HorizontalSlider(config.RemainingNotesLeftY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.RemainingNotesLeftY.ToString(), settingsTextFieldStyle), out float remainingNotesLeftY)) config.RemainingNotesLeftY = remainingNotesLeftY;
			GUILayout.Label("Indicator Scale", smallLabelStyle);
			config.RemainingNotesLeftScale = (int)GUILayout.HorizontalSlider(config.RemainingNotesLeftScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.RemainingNotesLeftScale.ToString(), settingsTextFieldStyle), out int remainingNotesLeftScale)) config.RemainingNotesLeftScale = remainingNotesLeftScale;
			config.RemainingNotesLeftBold = GUILayout.Toggle(config.RemainingNotesLeftBold, "Bold", settingsToggleStyle);
			config.RemainingNotesLeftItalic = GUILayout.Toggle(config.RemainingNotesLeftItalic, "Italic", settingsToggleStyle);
			var remainingNotesLeftColor = Config.ARGBToColor(config.RemainingNotesLeftColorARGB);
			GUILayout.Label("Red", smallLabelStyle);
			remainingNotesLeftColor.r = GUILayout.HorizontalSlider(remainingNotesLeftColor.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(remainingNotesLeftColor.r.ToString(), settingsTextFieldStyle), out float remainingNotesLeftR)) remainingNotesLeftColor.r = remainingNotesLeftR;
			GUILayout.Label("Green", smallLabelStyle);
			remainingNotesLeftColor.g = GUILayout.HorizontalSlider(remainingNotesLeftColor.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(remainingNotesLeftColor.g.ToString(), settingsTextFieldStyle), out float remainingNotesLeftG)) remainingNotesLeftColor.g = remainingNotesLeftG;
			GUILayout.Label("Blue", smallLabelStyle);
			remainingNotesLeftColor.b = GUILayout.HorizontalSlider(remainingNotesLeftColor.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(remainingNotesLeftColor.b.ToString(), settingsTextFieldStyle), out float remainingNotesLeftB)) remainingNotesLeftColor.b = remainingNotesLeftB;
			GUILayout.Label("Alpha", smallLabelStyle);
			remainingNotesLeftColor.a = GUILayout.HorizontalSlider(remainingNotesLeftColor.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(remainingNotesLeftColor.a.ToString(), settingsTextFieldStyle), out float remainingNotesLeftA)) remainingNotesLeftColor.a = remainingNotesLeftA;
			config.RemainingNotesLeftColorARGB = Config.ColorToARGB(remainingNotesLeftColor);

			GUILayout.Space(25.0f);
			if (GUILayout.Button("Save Config", settingsButtonStyle)) {
				if (configFilePath.Exists) configFilePath.Delete();
				var serializer = new XmlSerializer(typeof(Config));
				using (var configOut = configFilePath.OpenWrite()) {
					serializer.Serialize(configOut, config);
				}
			}
			GUILayout.Space(25.0f);

			GUILayout.Label($"Perfect Mode v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUILayout.EndScrollView();
			GUI.DragWindow();
		}
	}
}