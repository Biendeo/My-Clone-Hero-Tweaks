using ExtraSongUI.Settings;
using ExtraSongUI.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		private FileInfo configFilePath;
		private Config config;

		private WindowFunction setingsOnWindow;
		private LabelSettings settingsCurrentlyEditing;
		private WindowFunction settingsCurrentBack;

		private Rect r;

		public SongUI() {
			r = new Rect(2000.0f, 100.0f, 250.0f, 0.1f);
			configWindowEnabled = false;
			configFilePath = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Tweaks", "ExtraSongUIConfig.xml"));
			if (!configFilePath.Exists) {
				WriteDefaultConfig();
			} else {
				var serializer = new XmlSerializer(typeof(Config));
				using (var configIn = configFilePath.OpenRead()) {
					config = serializer.Deserialize(configIn) as Config;
				}
			}
		}

		private void WriteDefaultConfig() {
			config = new Config {
				TimeName = new EditableLabelSettings {
					X = 100.0f,
					Y = 750.0f,
					Size = 30,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "Time:",
				},
				SongTime = new FormattedLabelSettings {
					X = 400.0f,
					Y = 750.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				SongLength = new FormattedLabelSettings {
					X = 670.0f,
					Y = 750.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				CurrentStarProgressName = new FormattedLabelSettings {
					X = 100.0f,
					Y = 810.0f,
					Size = 30,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} → {1}:"
				},
				CurrentStarProgressScore = new FormattedLabelSettings {
					X = 400.0f,
					Y = 810.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				CurrentStarProgressEndScore = new FormattedLabelSettings {
					X = 670.0f,
					Y = 810.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				CurrentStarProgressPercentage = new FormattedLabelSettings {
					X = 700.0f,
					Y = 810.0f,
					Size = 50,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				SevenStarProgressName = new FormattedLabelSettings {
					X = 100.0f,
					Y = 870.0f,
					Size = 30,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} → {1}:"
				},
				SevenStarProgressScore = new FormattedLabelSettings {
					X = 400.0f,
					Y = 870.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				SevenStarProgressEndScore = new FormattedLabelSettings {
					X = 670.0f,
					Y = 870.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				SevenStarProgressPercentage = new FormattedLabelSettings {
					X = 700.0f,
					Y = 870.0f,
					Size = 50,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				NotesName = new EditableLabelSettings {
					X = 100.0f,
					Y = 930.0f,
					Size = 30,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "Notes:"
				},
				NotesHitCounter = new FormattedLabelSettings {
					X = 330.0f,
					Y = 930.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				NotesPassedCounter = new FormattedLabelSettings {
					X = 530.0f,
					Y = 930.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				TotalNotesCounter = new FormattedLabelSettings {
					X = 680.0f,
					Y = 930.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				NotesHitPercentage = new FormattedLabelSettings {
					X = 700.0f,
					Y = 930.0f,
					Size = 50,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				NotesMissedCounter = new FormattedLabelSettings {
					X = 780.0f,
					Y = 1070.0f,
					Size = 150,
					Alignment = TextAnchor.MiddleRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				StarPowerName = new EditableLabelSettings {
					X = 100.0f,
					Y = 990.0f,
					Size = 30,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Content = "SP:"
				},
				StarPowersGottenCounter = new FormattedLabelSettings {
					X = 400.0f,
					Y = 990.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0} /"
				},
				TotalStarPowersCounter = new FormattedLabelSettings {
					X = 520.0f,
					Y = 990.0f,
					Size = 50,
					Alignment = TextAnchor.LowerRight,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "{0}"
				},
				StarPowerPercentage = new FormattedLabelSettings {
					X = 700.0f,
					Y = 990.0f,
					Size = 50,
					Alignment = TextAnchor.LowerLeft,
					Bold = true,
					Italic = false,
					ColorARGB = LabelSettings.ColorToARGB(Color.white),
					Visible = true,
					Format = "({0}%)"
				},
				ConfigX = 100.0f,
				ConfigY = 100.0f,
				HideAll = false
			};
			var serializer = new XmlSerializer(typeof(Config));
			using (var configOut = configFilePath.OpenWrite()) {
				serializer.Serialize(configOut, config);
			}
		}

		#region Unity Methods

		void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __)
			{
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//var queue = new Queue<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
				//var objects = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
				//while (queue.Count > 0) {
				//	var o = queue.Dequeue();
				//	for (int i = 0; i < o.transform.childCount; ++i) {
				//		queue.Enqueue(o.transform.GetChild(i).gameObject);
				//		objects.Add(o.transform.GetChild(i).gameObject);
				//	}
				//}
				//foreach (var o in objects.Where(o => o.GetComponent<Text>() != null)) {
				//	Debug.LogError($"Object name: {o.name}, text: {o.GetComponent<Text>().text}");
				//}
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
			if (Input.GetKeyDown(KeyCode.F5) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift)) {
				configWindowEnabled = !configWindowEnabled;
				if (configWindowEnabled) {
					setingsOnWindow = OnWindowHead;
				} else {
					var serializer = new XmlSerializer(typeof(Config));
					using (var configOut = configFilePath.OpenWrite()) {
						serializer.Serialize(configOut, config);
					}
				}
			}
		}


		void OnGUI() {
			if (configWindowEnabled) {
				GUILayout.Window(0, r, setingsOnWindow, new GUIContent("Extra Song UI Settings"));
				//Debug.LogError($"X: {config.ConfigX} -> {newRect.x}, Y: {config.ConfigY} -> {newRect.y}");
				//config.ConfigX = newRect.x;
				//config.ConfigY = newRect.y;
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null) {
				//var style = new GUIStyle {
				//	font = uiFont,
				//	fontStyle = FontStyle.Bold,
				//	fontSize = 50,
				//	alignment = TextAnchor.MiddleRight,
				//	normal = new GUIStyleState {
				//		textColor = Color.white,
				//	}
				//};

				// Song length
				// TODO: The times are a bit incorrect for practice mode (they're offset by the timestamp at the beginning of the practise section).
				if (!config.HideAll && config.TimeName.Visible) GUI.Label(config.TimeName.Rect, new GUIContent(config.TimeName.Content), config.TimeName.Style(uiFont));
				if (!config.HideAll && config.SongTime.Visible) GUI.Label(config.SongTime.Rect, new GUIContent(string.Format(config.SongTime.Format, DoubleToTimeString(gameManager.SongTime))), config.SongTime.Style(uiFont));
				if (!config.HideAll && config.SongLength.Visible) GUI.Label(config.SongLength.Rect, new GUIContent(string.Format(config.SongLength.Format, DoubleToTimeString(gameManager.SongLength))), config.SongLength.Style(uiFont));
				//GUI.Label(new Rect(800f, 800f, 0.1f, 0.1f), new GUIContent($"{DoubleToTimeString(gameManager.SongTime)} / {DoubleToTimeString(gameManager.SongLength)}"), style);

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
				//GUI.Label(new Rect(160f, 860f, 0.1f, 0.1f), new GUIContent($"{starProgress.CurrentStar} → {Math.Min(7, starProgress.CurrentStar + 1)}:"), style);
				//GUI.Label(new Rect(800f, 860f, 0.1f, 0.1f), new GUIContent($"{currentScore - previousStarScore} / {nextStarScore - previousStarScore} ({nextStarPercentage.ToString("0.00")}%)"), style);
				//GUI.Label(new Rect(160f, 920f, 0.1f, 0.1f), new GUIContent($"0 → 7:"), style);
				//GUI.Label(new Rect(800f, 920f, 0.1f, 0.1f), new GUIContent($"{currentScore} / {sevenStarScore} ({sevenStarPercentage.ToString("0.00")}%)"), style);

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
				//GUI.Label(new Rect(160f, 980f, 0.1f, 0.1f), new GUIContent($"Notes:"), style);
				//GUI.Label(new Rect(800f, 980f, 0.1f, 0.1f), new GUIContent($"{hitNotes} / {seenNotes} / {totalNoteCount} ({(hitNotes * 100.0 / totalNoteCount).ToString("0.00")}%{(seenNotes == hitNotes ? (!gameManager.BasePlayers[0].FirstNoteMissed ? ", FC": string.Empty) : $", -{missedNotes}")})"), style);
				//GUI.Label(new Rect(160f, 1040f, 0.1f, 0.1f), new GUIContent($"SP:"), style);
				//GUI.Label(new Rect(800f, 1040f, 0.1f, 0.1f), new GUIContent($"{basePlayers[0].StarPowersHit} / {totalStarPowers} ({(basePlayers[0].StarPowersHit * 100.0 / totalStarPowers).ToString("0.00")}%)"), style);
			}
		}

		#endregion

		private void OnWindowHead(int id) {
			if (GUILayout.Button("Time")) {
				setingsOnWindow = OnWindowTime;
			}
			if (GUILayout.Button("Current Star")) {
				setingsOnWindow = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Seven Star")) {
				setingsOnWindow = OnWindowSevenStar;
			}
			if (GUILayout.Button("Notes")) {
				setingsOnWindow = OnWindowNotes;
			}
			if (GUILayout.Button("Star Power")) {
				setingsOnWindow = OnWindowStarPower;
			}

			GUILayout.Space(10.0f);
			config.HideAll = GUILayout.Toggle(config.HideAll, "Hide all extra UI");
			GUILayout.Space(50.0f);

			var style = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.MiddleCenter,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
		}

		private void OnWindowTime(int id) {
			if (GUILayout.Button("Name Label")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TimeName;
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Time")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongTime;
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Length")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongLength;
				settingsCurrentBack = OnWindowTime;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back")) {
				setingsOnWindow = OnWindowHead;
			}
		}

		private void OnWindowCurrentStar(int id) {
			if (GUILayout.Button("Name Label")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressName;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Current Score")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("End Score")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressEndScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Percentage")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressPercentage;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back")) {
				setingsOnWindow = OnWindowHead;
			}
		}

		private void OnWindowSevenStar(int id) {
			if (GUILayout.Button("Name Label")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressName;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Current Score")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("End Score")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressEndScore;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Percentage")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressPercentage;
				settingsCurrentBack = OnWindowCurrentStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back")) {
				setingsOnWindow = OnWindowHead;
			}
		}

		private void OnWindowNotes(int id) {
			if (GUILayout.Button("Name Label")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesName;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Passed Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesPassedCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Total Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalNotesCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Percentage")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitPercentage;
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Missed Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesMissedCounter;
				settingsCurrentBack = OnWindowNotes;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back")) {
				setingsOnWindow = OnWindowHead;
			}
		}

		private void OnWindowStarPower(int id) {
			if (GUILayout.Button("Name Label")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerName;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Hit Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowersGottenCounter;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Total Counter")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalStarPowersCounter;
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Percentage")) {
				setingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerPercentage;
				settingsCurrentBack = OnWindowStarPower;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back")) {
				setingsOnWindow = OnWindowHead;
			}
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
				((EditableLabelSettings)settingsCurrentlyEditing).Content = GUILayout.TextField(((EditableLabelSettings)settingsCurrentlyEditing).Content);
			}
			if (settingsCurrentlyEditing is FormattedLabelSettings) {
				GUILayout.Label("Format", style);
				((FormattedLabelSettings)settingsCurrentlyEditing).Format = GUILayout.TextField(((FormattedLabelSettings)settingsCurrentlyEditing).Format);
			}
			GUILayout.Label("X", style);
			settingsCurrentlyEditing.X = GUILayout.HorizontalSlider(settingsCurrentlyEditing.X, -2160, 2160.0f);
			if (float.TryParse(GUILayout.TextField(settingsCurrentlyEditing.X.ToString()), out float x)) settingsCurrentlyEditing.X = x;
			GUILayout.Label("Y", style);
			settingsCurrentlyEditing.Y = GUILayout.HorizontalSlider(settingsCurrentlyEditing.Y, -2160, 2160.0f);
			if (float.TryParse(GUILayout.TextField(settingsCurrentlyEditing.Y.ToString()), out float y)) settingsCurrentlyEditing.Y = y;
			GUILayout.Label("Size", style);
			settingsCurrentlyEditing.Size = (int)GUILayout.HorizontalSlider(settingsCurrentlyEditing.Size, 0.0f, 500.0f);
			if (int.TryParse(GUILayout.TextField(settingsCurrentlyEditing.Size.ToString()), out int size)) settingsCurrentlyEditing.Size = size;
			var color = LabelSettings.ARGBToColor(settingsCurrentlyEditing.ColorARGB);
			GUILayout.Label("Red", style);
			color.r = GUILayout.HorizontalSlider(color.r, 0.0f, 1.0f);
			if (float.TryParse(GUILayout.TextField(color.r.ToString()), out float r)) color.r = r;
			GUILayout.Label("Green", style);
			color.g = GUILayout.HorizontalSlider(color.g, 0.0f, 1.0f);
			if (float.TryParse(GUILayout.TextField(color.g.ToString()), out float g)) color.g = g;
			GUILayout.Label("Blue", style);
			color.b = GUILayout.HorizontalSlider(color.b, 0.0f, 1.0f);
			if (float.TryParse(GUILayout.TextField(color.b.ToString()), out float b)) color.b = b;
			GUILayout.Label("Alpha", style);
			color.a = GUILayout.HorizontalSlider(color.a, 0.0f, 1.0f);
			if (float.TryParse(GUILayout.TextField(color.a.ToString()), out float a)) color.a = a;
			settingsCurrentlyEditing.ColorARGB = LabelSettings.ColorToARGB(color);
			if (GUILayout.Button($"Alignment: {settingsCurrentlyEditing.Alignment.ToString()}")) {
				settingsCurrentlyEditing.Alignment = (TextAnchor)((int)(settingsCurrentlyEditing.Alignment + 1) % 9);
			}
			settingsCurrentlyEditing.Bold = GUILayout.Toggle(settingsCurrentlyEditing.Bold, "Bold");
			settingsCurrentlyEditing.Italic = GUILayout.Toggle(settingsCurrentlyEditing.Italic, "Italic");
			settingsCurrentlyEditing.Visible = GUILayout.Toggle(settingsCurrentlyEditing.Visible, "Visible");
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", new GUILayoutOption[0])) {
				setingsOnWindow = settingsCurrentBack;
			}
		}

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