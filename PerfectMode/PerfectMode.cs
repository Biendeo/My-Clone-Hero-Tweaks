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

		private string target;
		private bool isStillFC;
		private int missedNotes;

		public PerfectMode() {
			configWindowEnabled = false;
			configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "PerfectModeConfig.xml"));
		}

		private void WriteDefaultConfig() {
			config = new Config {
				ConfigX = 200.0f,
				ConfigY = 200.0f,
				Enabled = false,
				FC = true,
				NotesMissed = 0,
				FailDelay = 2.0f,
				DisplayImage = true,
				DisplayImageX = 30.0f,
				DisplayImageY = Screen.height - 80.0f,
				DisplayImageColorARGB = Config.ColorToARGB(Color.white),
				DisplayImageScale = 50
			};
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
				}
			}
			if (config.Enabled) {
				if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null && !failedObjective) {
					target = config.FC ? "FC" : (config.NotesMissed == 0 ? "100%" : $"-{config.NotesMissed}");
					isStillFC = !gameManager.BasePlayers[0].FirstNoteMissed;
					missedNotes = notes.Count(n => n.WasMissed && !n.WasHit);
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
				if (!configWindowEnabled) {
					var serializer = new XmlSerializer(typeof(Config));
					using (var configOut = configFilePath.OpenWrite()) {
						serializer.Serialize(configOut, config);
					}
				}
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
				var outputRect = GUILayout.Window(5318009, new Rect(config.ConfigX, config.ConfigY, 250.0f, 780.0f), OnWindow, new GUIContent("Perfect Mode Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}

			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null) {
				if (config.Enabled) {
					if (config.DisplayImage) {
						var displayImageStyle = new GUIStyle {
							font = uiFont,
							fontSize = config.DisplayImageScale,
							fontStyle = FontStyle.Bold,
							alignment = TextAnchor.UpperLeft,
							normal = new GUIStyleState {
								textColor = Config.ARGBToColor(config.DisplayImageColorARGB)
							}
						};
						GUI.Label(new Rect(config.DisplayImageX, config.DisplayImageY, 0.1f, 0.1f), new GUIContent($"{target} mode active"), displayImageStyle);
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
			var style = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			config.Enabled = GUILayout.Toggle(config.Enabled, "Enabled", settingsToggleStyle);
			config.FC = GUILayout.Toggle(config.FC, "FC Mode", settingsToggleStyle);
			GUILayout.Label("Note Miss Limit (inclusive)", style);
			config.NotesMissed = (int)GUILayout.HorizontalSlider(config.NotesMissed, 0.0f, 100.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.NotesMissed.ToString(), settingsTextFieldStyle), out int notesMissed)) config.NotesMissed = notesMissed;
			GUILayout.Label("Fail Delay Before Restart", style);
			config.FailDelay = GUILayout.HorizontalSlider(config.FailDelay, 0.0f, 10.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.FailDelay.ToString(), settingsTextFieldStyle), out float failDelay)) config.FailDelay = failDelay;
			config.DisplayImage = GUILayout.Toggle(config.DisplayImage, "On-screen indicator", settingsToggleStyle);
			GUILayout.Label("Indicator X", style);
			config.DisplayImageX = GUILayout.HorizontalSlider(config.DisplayImageX, -3840.0f, 3840.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.DisplayImageX.ToString(), settingsTextFieldStyle), out float displayImageX)) config.DisplayImageX = displayImageX;
			GUILayout.Label("Indicator Y", style);
			config.DisplayImageY = GUILayout.HorizontalSlider(config.DisplayImageY, -2160.0f, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(config.DisplayImageY.ToString(), settingsTextFieldStyle), out float displayImageY)) config.DisplayImageY = displayImageY;
			GUILayout.Label("Indicator Scale", style);
			config.DisplayImageScale = (int)GUILayout.HorizontalSlider(config.DisplayImageScale, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(config.DisplayImageScale.ToString(), settingsTextFieldStyle), out int displayImageScale)) config.DisplayImageScale = displayImageScale;
			var color = Config.ARGBToColor(config.DisplayImageColorARGB);
			GUILayout.Label("Red", style);
			color.r = GUILayout.HorizontalSlider(color.r, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(color.r.ToString(), settingsTextFieldStyle), out float r)) color.r = r;
			GUILayout.Label("Green", style);
			color.g = GUILayout.HorizontalSlider(color.g, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(color.g.ToString(), settingsTextFieldStyle), out float g)) color.g = g;
			GUILayout.Label("Blue", style);
			color.b = GUILayout.HorizontalSlider(color.b, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(color.b.ToString(), settingsTextFieldStyle), out float b)) color.b = b;
			GUILayout.Label("Alpha", style);
			color.a = GUILayout.HorizontalSlider(color.a, 0.0f, 1.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(color.a.ToString(), settingsTextFieldStyle), out float a)) color.a = a;
			config.DisplayImageColorARGB = Config.ColorToARGB(color);

			GUILayout.Space(50.0f);

			GUILayout.Label($"Perfect Mode v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUI.DragWindow();
		}
	}
}