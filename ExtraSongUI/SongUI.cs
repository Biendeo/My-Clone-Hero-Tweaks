using ExtraSongUI.Settings;
using ExtraSongUI.Wrappers;
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

namespace ExtraSongUI {
	public class SongUI : MonoBehaviour {
		private bool sceneChanged;

		// Song length
		private GameManagerWrapper gameManager;

		// Star progress
		private StarProgressWrapper starProgress;

		// Note count
		private BasePlayerWrapper[] basePlayers;
		private int totalNoteCount;
		private int totalStarPowers;
		private List<NoteWrapper> noteSet;

		private Font uiFont;

		private bool configWindowEnabled;
		private readonly FileInfo configFilePath;
		private Config config;

		private WindowFunction settingsOnWindow;
		private LabelSettings settingsCurrentlyEditing;
		private WindowFunction settingsCurrentBack;

		private GUIStyle settingsWindowStyle;
		private GUIStyle settingsToggleStyle;
		private GUIStyle settingsButtonStyle;
		private GUIStyle settingsTextAreaStyle;
		private GUIStyle settingsTextFieldStyle;
		private GUIStyle settingsLabelStyle;
		private GUIStyle settingsBoxStyle;
		private GUIStyle settingsHorizontalSliderStyle;
		private GUIStyle settingsHorizontalSliderThumbStyle;

		public SongUI() {
			configWindowEnabled = false;
			configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ExtraSongUIConfig.xml"));
		}

		private void WriteDefaultConfig() {
			// These original numbers were designed with 1440p in mind so this'll sort it out.
			float widthScale = Screen.width / 2560.0f;
			float heightScale = Screen.height / 1440.0f;
			int smallFontSize = (int)(30 * widthScale);
			int largeFontSize = (int)(50 * widthScale);
			int extraLargeFontSize = (int)(150 * widthScale);

			config = new Config {
				TimeName = new EditableLabelSettings {
					X = 100.0f * widthScale,
					Y = 750.0f * heightScale,
					Size = smallFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "Time:",
				},
				SongTime = new FormattedLabelSettings {
					X = 400.0f * widthScale,
					Y = 750.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				SongLength = new FormattedLabelSettings {
					X = 670.0f * widthScale,
					Y = 750.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				CurrentStarProgressName = new FormattedLabelSettings {
					X = 100.0f * widthScale,
					Y = 810.0f * heightScale,
					Size = smallFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} → {1}:"
				},
				CurrentStarProgressScore = new FormattedLabelSettings {
					X = 400.0f * widthScale,
					Y = 810.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				CurrentStarProgressEndScore = new FormattedLabelSettings {
					X = 670.0f * widthScale,
					Y = 810.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				CurrentStarProgressPercentage = new FormattedLabelSettings {
					X = 700.0f * widthScale,
					Y = 810.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				SevenStarProgressName = new FormattedLabelSettings {
					X = 100.0f * widthScale,
					Y = 870.0f * heightScale,
					Size = smallFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} → {1}:"
				},
				SevenStarProgressScore = new FormattedLabelSettings {
					X = 400.0f * widthScale,
					Y = 870.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				SevenStarProgressEndScore = new FormattedLabelSettings {
					X = 670.0f * widthScale,
					Y = 870.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				SevenStarProgressPercentage = new FormattedLabelSettings {
					X = 700.0f * widthScale,
					Y = 870.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				NotesName = new EditableLabelSettings {
					X = 100.0f * widthScale,
					Y = 930.0f * heightScale,
					Size = smallFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "Notes:"
				},
				NotesHitCounter = new FormattedLabelSettings {
					X = 330.0f * widthScale,
					Y = 930.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				NotesPassedCounter = new FormattedLabelSettings {
					X = 530.0f * widthScale,
					Y = 930.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				TotalNotesCounter = new FormattedLabelSettings {
					X = 680.0f * widthScale,
					Y = 930.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				NotesHitPercentage = new FormattedLabelSettings {
					X = 700.0f * widthScale,
					Y = 930.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				NotesMissedCounter = new FormattedLabelSettings {
					X = 780.0f * widthScale,
					Y = 1070.0f * heightScale,
					Size = extraLargeFontSize,
					Alignment = TextAnchor.MiddleRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				StarPowerName = new EditableLabelSettings {
					X = 100.0f * widthScale,
					Y = 990.0f * heightScale,
					Size = smallFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "SP:"
				},
				StarPowersGottenCounter = new FormattedLabelSettings {
					X = 400.0f * widthScale,
					Y = 990.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				TotalStarPowersCounter = new FormattedLabelSettings {
					X = 520.0f * widthScale,
					Y = 990.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				StarPowerPercentage = new FormattedLabelSettings {
					X = 700.0f * widthScale,
					Y = 990.0f * heightScale,
					Size = largeFontSize,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				ConfigX = Screen.width - 350.0f,
				ConfigY = 100.0f * heightScale,
				HideAll = false
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
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __)
			{
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					// Song length
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					starProgress = gameManager.StarProgress;
					basePlayers = gameManager.BasePlayers;
					//? The player difficulty seems to go easy to expert rather than expert to easy, so this 3 - inverts that for this usage only. That should be looked into.
					var chart = gameManager.Song.GetChart(gameManager.BasePlayers[0].Player.PlayerProfile.Instrument, (sbyte)(3 - gameManager.BasePlayers[0].Player.PlayerProfile.Difficulty));
					totalNoteCount = chart.UnknownInt1;
					totalStarPowers = chart.StarPower.Length;
					noteSet = new List<NoteWrapper>();
				}
			}
			if (Input.GetKeyDown(KeyCode.F5) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
				configWindowEnabled = !configWindowEnabled;
				if (configWindowEnabled) {
					settingsOnWindow = OnWindowHead;
				} else {
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
				var outputRect = GUILayout.Window(5318008, new Rect(config.ConfigX, config.ConfigY, 250.0f, 0.1f), settingsOnWindow, new GUIContent("Extra Song UI Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}

			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null) {
				// Song length
				if (!config.HideAll && config.TimeName.Visible) GUI.Label(config.TimeName.Rect, new GUIContent(config.TimeName.Content), config.TimeName.Style(uiFont));
				if (!config.HideAll && config.SongTime.Visible) GUI.Label(config.SongTime.Rect, new GUIContent(string.Format(config.SongTime.Format, DoubleToTimeString(gameManager.SongTime))), config.SongTime.Style(uiFont));
				if (!config.HideAll && config.SongLength.Visible) GUI.Label(config.SongLength.Rect, new GUIContent(string.Format(config.SongLength.Format, DoubleToTimeString(gameManager.SongLength))), config.SongLength.Style(uiFont));

				// Star progress
				int currentScore = starProgress.LastScore;
				int previousStarScore = starProgress.CurrentStar == 0 ? 0 : starProgress.StarScores[starProgress.CurrentStar - 1];
				int nextStarScore = starProgress.StarScores[starProgress.CurrentStar];
				double nextStarPercentage = starProgress.CurrentStar < 7 ? (currentScore - previousStarScore) * 100.0 / (nextStarScore - previousStarScore) : 100.0;
				int sevenStarScore = starProgress.StarScores[6];
				double sevenStarPercentage = Math.Min(100.0, currentScore * 100.0 / sevenStarScore);

				if (!config.HideAll && config.CurrentStarProgressName.Visible) GUI.Label(config.CurrentStarProgressName.Rect, new GUIContent(string.Format(config.CurrentStarProgressName.Format, starProgress.CurrentStar, Math.Min(7, starProgress.CurrentStar + 1))), config.CurrentStarProgressName.Style(uiFont));
				if (!config.HideAll && config.CurrentStarProgressScore.Visible) GUI.Label(config.CurrentStarProgressScore.Rect, new GUIContent(string.Format(config.CurrentStarProgressScore.Format, currentScore - previousStarScore)), config.CurrentStarProgressScore.Style(uiFont));
				if (!config.HideAll && config.CurrentStarProgressEndScore.Visible) GUI.Label(config.CurrentStarProgressEndScore.Rect, new GUIContent(string.Format(config.CurrentStarProgressEndScore.Format, nextStarScore - previousStarScore)), config.CurrentStarProgressEndScore.Style(uiFont));
				if (!config.HideAll && config.CurrentStarProgressPercentage.Visible) GUI.Label(config.CurrentStarProgressPercentage.Rect, new GUIContent(string.Format(config.CurrentStarProgressPercentage.Format, nextStarPercentage.ToString("0.00"))), config.CurrentStarProgressPercentage.Style(uiFont));

				if (!config.HideAll && config.SevenStarProgressName.Visible) GUI.Label(config.SevenStarProgressName.Rect, new GUIContent(string.Format(config.SevenStarProgressName.Format, 0, 7)), config.SevenStarProgressName.Style(uiFont));
				if (!config.HideAll && config.SevenStarProgressScore.Visible) GUI.Label(config.SevenStarProgressScore.Rect, new GUIContent(string.Format(config.SevenStarProgressScore.Format, currentScore)), config.SevenStarProgressScore.Style(uiFont));
				if (!config.HideAll && config.SevenStarProgressEndScore.Visible) GUI.Label(config.SevenStarProgressEndScore.Rect, new GUIContent(string.Format(config.SevenStarProgressEndScore.Format, sevenStarScore)), config.SevenStarProgressEndScore.Style(uiFont));
				if (!config.HideAll && config.SevenStarProgressPercentage.Visible) GUI.Label(config.SevenStarProgressPercentage.Rect, new GUIContent(string.Format(config.SevenStarProgressPercentage.Format, sevenStarPercentage.ToString("0.00"))), config.SevenStarProgressPercentage.Style(uiFont));

				// Note count
				var newNotes = gameManager.BasePlayers[0].HittableNotes.ToList();
				foreach (var nonNull in newNotes.Where(n => !(n.note is null))) {
					if (!noteSet.Exists(n => n.note == nonNull.note)) {
						noteSet.Add(nonNull);
					}
				}
				int hitNotes = noteSet.Count(n => n.WasHit);
				int missedNotes = noteSet.Count(n => n.WasMissed && !n.WasHit);
				int seenNotes = hitNotes + missedNotes;

				if (!config.HideAll && config.NotesName.Visible) GUI.Label(config.NotesName.Rect, new GUIContent(config.NotesName.Content), config.NotesName.Style(uiFont));
				if (!config.HideAll && config.NotesHitCounter.Visible) GUI.Label(config.NotesHitCounter.Rect, new GUIContent(string.Format(config.NotesHitCounter.Format, hitNotes)), config.NotesHitCounter.Style(uiFont));
				if (!config.HideAll && config.NotesPassedCounter.Visible) GUI.Label(config.NotesPassedCounter.Rect, new GUIContent(string.Format(config.NotesPassedCounter.Format, seenNotes)), config.NotesPassedCounter.Style(uiFont));
				if (!config.HideAll && config.TotalNotesCounter.Visible) GUI.Label(config.TotalNotesCounter.Rect, new GUIContent(string.Format(config.TotalNotesCounter.Format, totalNoteCount)), config.TotalNotesCounter.Style(uiFont));
				if (!config.HideAll && config.NotesHitPercentage.Visible) GUI.Label(config.NotesHitPercentage.Rect, new GUIContent(string.Format(config.NotesHitPercentage.Format, (hitNotes * 100.0 / totalNoteCount).ToString("0.00"))), config.NotesHitPercentage.Style(uiFont));
				if (!config.HideAll && config.NotesMissedCounter.Visible) GUI.Label(config.NotesMissedCounter.Rect, new GUIContent(string.Format(config.NotesMissedCounter.Format, seenNotes == hitNotes ? (!gameManager.BasePlayers[0].FirstNoteMissed ? "FC" : "100%") : $"-{missedNotes}")), config.NotesMissedCounter.Style(uiFont));

				if (!config.HideAll && config.StarPowerName.Visible) GUI.Label(config.StarPowerName.Rect, new GUIContent(config.StarPowerName.Content), config.StarPowerName.Style(uiFont));
				if (!config.HideAll && config.StarPowersGottenCounter.Visible) GUI.Label(config.StarPowersGottenCounter.Rect, new GUIContent(string.Format(config.StarPowersGottenCounter.Format, basePlayers[0].StarPowersHit)), config.StarPowersGottenCounter.Style(uiFont));
				if (!config.HideAll && config.TotalStarPowersCounter.Visible) GUI.Label(config.TotalStarPowersCounter.Rect, new GUIContent(string.Format(config.TotalStarPowersCounter.Format, totalStarPowers)), config.TotalStarPowersCounter.Style(uiFont));
				if (!config.HideAll && config.StarPowerPercentage.Visible) GUI.Label(config.StarPowerPercentage.Rect, new GUIContent(string.Format(config.StarPowerPercentage.Format, (basePlayers[0].StarPowersHit * 100.0 / totalStarPowers).ToString("0.00"))), config.StarPowerPercentage.Style(uiFont));
			}
		}

		#endregion

		#region OnWindow Methods

		private void OnWindowHead(int id) {
			if (GUILayout.Button("Time", settingsButtonStyle)) {
				settingsOnWindow = OnWindowTime;
			}
			if (GUILayout.Button("Current Star", settingsButtonStyle)) {
				settingsOnWindow = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Seven Star", settingsButtonStyle)) {
				settingsOnWindow = OnWindowSevenStar;
			}
			if (GUILayout.Button("Notes", settingsButtonStyle)) {
				settingsOnWindow = OnWindowNotes;
			}
			if (GUILayout.Button("Star Power", settingsButtonStyle)) {
				settingsOnWindow = OnWindowStarPower;
			}

			GUILayout.Space(10.0f);
			config.HideAll = GUILayout.Toggle(config.HideAll, "Hide all extra UI", settingsToggleStyle);
			GUILayout.Space(50.0f);

			var style = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.MiddleCenter,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label($"Extra Song UI v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUI.DragWindow();
		}

		private void OnWindowTime(int id) {
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TimeName;
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Time", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongTime;
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Length", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongLength;
				settingsCurrentBack = OnWindowTime;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowCurrentStar(int id) {
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressName;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Current Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("End Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressEndScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressPercentage;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowSevenStar(int id) {
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressName;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Current Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("End Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressEndScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressPercentage;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowNotes(int id) {
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesName;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Passed Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesPassedCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Total Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalNotesCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitPercentage;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Missed Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesMissedCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowStarPower(int id) {
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerName;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Hit Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowersGottenCounter;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Total Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalStarPowersCounter;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerPercentage;
				settingsCurrentBack = OnWindowStarPower;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowEdit(int id) {
			var style = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			if (settingsCurrentlyEditing is EditableLabelSettings) {
				GUILayout.Label("Content", style);
				((EditableLabelSettings)settingsCurrentlyEditing).Content = GUILayout.TextField(((EditableLabelSettings)settingsCurrentlyEditing).Content, settingsTextFieldStyle);
			}
			if (settingsCurrentlyEditing is FormattedLabelSettings) {
				GUILayout.Label("Format", style);
				((FormattedLabelSettings)settingsCurrentlyEditing).Format = GUILayout.TextField(((FormattedLabelSettings)settingsCurrentlyEditing).Format, settingsTextFieldStyle);
			}
			GUILayout.Label("X", style);
			settingsCurrentlyEditing.X = GUILayout.HorizontalSlider(settingsCurrentlyEditing.X, -2160, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(settingsCurrentlyEditing.X.ToString(), settingsTextFieldStyle), out float x)) settingsCurrentlyEditing.X = x;
			GUILayout.Label("Y", style);
			settingsCurrentlyEditing.Y = GUILayout.HorizontalSlider(settingsCurrentlyEditing.Y, -2160, 2160.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (float.TryParse(GUILayout.TextField(settingsCurrentlyEditing.Y.ToString(), settingsTextFieldStyle), out float y)) settingsCurrentlyEditing.Y = y;
			GUILayout.Label("Size", style);
			settingsCurrentlyEditing.Size = (int)GUILayout.HorizontalSlider(settingsCurrentlyEditing.Size, 0.0f, 500.0f, settingsHorizontalSliderStyle, settingsHorizontalSliderThumbStyle);
			if (int.TryParse(GUILayout.TextField(settingsCurrentlyEditing.Size.ToString(), settingsTextFieldStyle), out int size)) settingsCurrentlyEditing.Size = size;
			var color = LabelSettings.ARGBToColor(settingsCurrentlyEditing.ColorARGB);
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
			settingsCurrentlyEditing.ColorARGB = LabelSettings.ColorToARGB(color);
			if (GUILayout.Button($"Alignment: {settingsCurrentlyEditing.Alignment.ToString()}", settingsButtonStyle)) {
				settingsCurrentlyEditing.Alignment = (TextAnchor)((int)(settingsCurrentlyEditing.Alignment + 1) % 9);
			}
			settingsCurrentlyEditing.Bold = GUILayout.Toggle(settingsCurrentlyEditing.Bold, "Bold", settingsToggleStyle);
			settingsCurrentlyEditing.Italic = GUILayout.Toggle(settingsCurrentlyEditing.Italic, "Italic", settingsToggleStyle);
			settingsCurrentlyEditing.Visible = GUILayout.Toggle(settingsCurrentlyEditing.Visible, "Visible", settingsToggleStyle);
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = settingsCurrentBack;
			}
			GUI.DragWindow();
		}

		#endregion

		/// <summary>
		/// Converts a time into a displayable string (m:ss.ms)
		/// </summary>
		/// <param name="t">The input time (as seconds)</param>
		/// <returns></returns>
		private static string DoubleToTimeString(double t) {
			var sb = new StringBuilder();

			if (t < 0.0) {
				sb.Append("-");
				t = Math.Abs(t);
			}
			sb.Append((int)(t / 60.0));
			sb.Append(":");
			sb.Append(((int)(t % 60.0)).ToString().PadLeft(2, '0'));
			sb.Append(".");
			sb.Append(((int)((t * 1000.0) % 1000.0)).ToString().PadLeft(3, '0'));

			return sb.ToString();
		}


	}
}