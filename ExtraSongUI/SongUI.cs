using Common.Settings;
using Common.Wrappers;
using ExtraSongUI.Settings;
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
		private List<NoteWrapper> notes;
		private int totalNoteCount;
		private int totalStarPowers;

		private Font uiFont;

		private Config config;

		private WindowFunction settingsOnWindow;
		private FormattableColorablePositionableLabel settingsCurrentlyEditing;
		private string settingsCurrentlyEditingName;
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

		private string formattedSongTime;
		private string formattedSongLength;

		private int currentStarCount;
		private int currentScore;
		private int previousStarScore;
		private int nextStarScore;
		private double nextStarPercentage;
		private int sevenStarScore;
		private double sevenStarPercentage;

		private int hitNotes;
		private int missedNotes;
		private int seenNotes;
		private double hitNotesPercentage;
		private string fcIndicator;

		private int starPowersGotten;
		private double starPowerPercentage;

		private int currentCombo;
		private int highestCombo;

		private int currentNoteIndex;

		private GameObject TimeNameLabel;
		private GameObject SongTimeLabel;
		private GameObject SongLengthLabel;

		private GameObject CurrentStarProgressNameLabel;
		private GameObject CurrentStarProgressScoreLabel;
		private GameObject CurrentStarProgressEndScoreLabel;
		private GameObject CurrentStarProgressPercentageLabel;

		private GameObject SevenStarProgressNameLabel;
		private GameObject SevenStarProgressScoreLabel;
		private GameObject SevenStarProgressEndScoreLabel;
		private GameObject SevenStarProgressPercentageLabel;

		private GameObject NotesNameLabel;
		private GameObject NotesHitCounterLabel;
		private GameObject NotesPassedCounterLabel;
		private GameObject TotalNotesCounterLabel;
		private GameObject NotesHitPercentageLabel;
		private GameObject NotesMissedCounterLabel;

		private GameObject StarPowerNameLabel;
		private GameObject StarPowersGottenCounterLabel;
		private GameObject TotalStarPowersCounterLabel;
		private GameObject StarPowerPercentageLabel;

		private GameObject ComboNameLabel;
		private GameObject CurrentComboCounterLabel;
		private GameObject HighestComboCounterLabel;

		public SongUI() {
			settingsOnWindow = OnWindowHead;
		}

		#region Unity Methods

		void Start() {
			config = Config.LoadConfig();
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		private void DestroyAndNullGameplayLabels() {
			if (TimeNameLabel != null) Destroy(TimeNameLabel);
			if (SongTimeLabel != null) Destroy(SongTimeLabel);
			if (SongLengthLabel != null) Destroy(SongLengthLabel);

			if (CurrentStarProgressNameLabel != null) Destroy(CurrentStarProgressNameLabel);
			if (CurrentStarProgressScoreLabel != null) Destroy(CurrentStarProgressScoreLabel);
			if (CurrentStarProgressEndScoreLabel != null) Destroy(CurrentStarProgressEndScoreLabel);
			if (CurrentStarProgressPercentageLabel != null) Destroy(CurrentStarProgressPercentageLabel);

			if (SevenStarProgressNameLabel != null) Destroy(SevenStarProgressNameLabel);
			if (SevenStarProgressScoreLabel != null) Destroy(SevenStarProgressScoreLabel);
			if (SevenStarProgressEndScoreLabel != null) Destroy(SevenStarProgressEndScoreLabel);
			if (SevenStarProgressPercentageLabel != null) Destroy(SevenStarProgressPercentageLabel);

			if (NotesNameLabel != null) Destroy(NotesNameLabel);
			if (NotesHitCounterLabel != null) Destroy(NotesHitCounterLabel);
			if (NotesPassedCounterLabel != null) Destroy(NotesPassedCounterLabel);
			if (TotalNotesCounterLabel != null) Destroy(TotalNotesCounterLabel);
			if (NotesHitPercentageLabel != null) Destroy(NotesHitPercentageLabel);
			if (NotesMissedCounterLabel != null) Destroy(NotesMissedCounterLabel);

			if (StarPowerNameLabel != null) Destroy(StarPowerNameLabel);
			if (StarPowersGottenCounterLabel != null) Destroy(StarPowersGottenCounterLabel);
			if (TotalStarPowersCounterLabel != null) Destroy(TotalStarPowersCounterLabel);
			if (StarPowerPercentageLabel != null) Destroy(StarPowerPercentageLabel);

			if (ComboNameLabel != null) Destroy(ComboNameLabel);
			if (CurrentComboCounterLabel != null) Destroy(CurrentComboCounterLabel);
			if (HighestComboCounterLabel != null) Destroy(HighestComboCounterLabel);

			TimeNameLabel = null;
			SongTimeLabel = null;
			SongLengthLabel = null;

			CurrentStarProgressNameLabel = null;
			CurrentStarProgressScoreLabel = null;
			CurrentStarProgressEndScoreLabel = null;
			CurrentStarProgressPercentageLabel = null;

			SevenStarProgressNameLabel = null;
			SevenStarProgressScoreLabel = null;
			SevenStarProgressEndScoreLabel = null;
			SevenStarProgressPercentageLabel = null;

			NotesNameLabel = null;
			NotesHitCounterLabel = null;
			NotesPassedCounterLabel = null;
			TotalNotesCounterLabel = null;
			NotesHitPercentageLabel = null;
			NotesMissedCounterLabel = null;

			StarPowerNameLabel = null;
			StarPowersGottenCounterLabel = null;
			TotalStarPowersCounterLabel = null;
			StarPowerPercentageLabel = null;

			ComboNameLabel = null;
			CurrentComboCounterLabel = null;
			HighestComboCounterLabel = null;

		}

		private GameObject CreateGameplayLabel(Transform canvasTransform, string labelName, Font uiFont) {
			var o = new GameObject(labelName, new Type[] {
						typeof(Text)
					});
			o.layer = LayerMask.NameToLayer("UI");
			o.transform.SetParent(canvasTransform);
			o.transform.SetSiblingIndex(0);
			o.transform.localEulerAngles = new Vector3();
			o.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			o.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
			o.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
			o.GetComponent<Text>().font = uiFont;
			return o;
		}

		private void UpdateGameplayLabel(GameObject o, FormattableColorablePositionableLabel labelSettings, string content) {
			if (labelSettings.Visible && config.Enabled) {
				o.transform.localPosition = new Vector3(labelSettings.X - Screen.width / 2, Screen.height / 2 - labelSettings.Y);
				var text = o.GetComponent<Text>();
				text.enabled = true;
				text.fontSize = labelSettings.Size;
				text.alignment = labelSettings.Alignment;
				text.fontStyle = (labelSettings.Bold ? FontStyle.Bold : FontStyle.Normal) | (labelSettings.Italic ? FontStyle.Italic : FontStyle.Normal);
				text.text = content;
				text.color = labelSettings.Color.Color;
			} else {
				o.GetComponent<Text>().enabled = false;
			}
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					// Song length
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					starProgress = gameManager.StarProgress;
					basePlayers = gameManager.BasePlayers;
					notes = basePlayers[0].Notes;
					totalNoteCount = notes?.Count ?? 0;
					totalStarPowers = notes?.Count(n => n.IsStarPowerEnd) ?? 0;
					hitNotes = 0;
					missedNotes = 0;
					currentCombo = 0;
					highestCombo = 0;
					currentNoteIndex = 0;

					DestroyAndNullGameplayLabels();
					Transform canvasTransform = FadeBehaviourWrapper.instance.fadeGraphic.canvas.transform;

					TimeNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Time Name Label", uiFont);
					SongTimeLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Time Label", uiFont);
					SongLengthLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Length Label", uiFont);

					CurrentStarProgressNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Current Star Progress Name Label", uiFont);
					CurrentStarProgressScoreLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Current Star Progress Score Label", uiFont);
					CurrentStarProgressEndScoreLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Current Star Progress End Score Label", uiFont);
					CurrentStarProgressPercentageLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Current Star Progress Percentage Label", uiFont);

					SevenStarProgressNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Seven Star Progress Name Label", uiFont);
					SevenStarProgressScoreLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Seven Star Progress Score Label", uiFont);
					SevenStarProgressEndScoreLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Seven Star Progress End Score Label", uiFont);
					SevenStarProgressPercentageLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Seven Star Progress Percentage Label", uiFont);

					NotesNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Notes Name Label", uiFont);
					NotesHitCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Notes Hit Counter Label", uiFont);
					NotesPassedCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Notes Passed Counter Label", uiFont);
					TotalNotesCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Total Notes Counter Label", uiFont);
					NotesHitPercentageLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Notes Hit Percentage Label", uiFont);
					NotesMissedCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Notes Missed Counter Label", uiFont);

					StarPowerNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Length Label", uiFont);
					StarPowersGottenCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Length Label", uiFont);
					TotalStarPowersCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Length Label", uiFont);
					StarPowerPercentageLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Song Length Label", uiFont);

					ComboNameLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Star Power Name Label", uiFont);
					CurrentComboCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Current Combo Counter Label", uiFont);
					HighestComboCounterLabel = CreateGameplayLabel(canvasTransform, "Extra Song UI Highest Combo Counter Label", uiFont);
				} else {
					DestroyAndNullGameplayLabels();
				}
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null && gameManager.PracticeUI.practiceUI == null) {
				// Song length
				formattedSongTime = string.Format(config.SongTime.Format, DoubleToTimeString(gameManager.SongTime));
				formattedSongLength = string.Format(config.SongLength.Format, DoubleToTimeString(gameManager.SongLength));

				// Star progress
				currentStarCount = starProgress.CurrentStar;
				currentScore = starProgress.LastScore; //TODO: Migrate to ScoreManager 'cause this stops after a while.
				previousStarScore = starProgress.CurrentStar == 0 ? 0 : starProgress.StarScores[starProgress.CurrentStar - 1];
				nextStarScore = starProgress.StarScores[starProgress.CurrentStar];
				nextStarPercentage = starProgress.CurrentStar < 7 ? (currentScore - previousStarScore) * 100.0 / (nextStarScore - previousStarScore) : 100.0;
				sevenStarScore = starProgress.StarScores[6];
				sevenStarPercentage = currentScore * 100.0 / sevenStarScore;

				// Note count
				while (currentNoteIndex < totalNoteCount && (notes[currentNoteIndex].WasHit || notes[currentNoteIndex].WasMissed)) {
					if (notes[currentNoteIndex].WasHit) {
						++hitNotes;
					} else if (notes[currentNoteIndex].WasMissed) {
						++missedNotes;
					}
					++currentNoteIndex;
				}
				seenNotes = hitNotes + missedNotes;
				hitNotesPercentage = hitNotes * 100.0 / totalNoteCount;
				fcIndicator = seenNotes == hitNotes ? (!gameManager.BasePlayers[0].FirstNoteMissed ? "FC" : "100%") : $"-{missedNotes}";

				starPowersGotten = basePlayers[0].StarPowersHit;
				starPowerPercentage = starPowersGotten * 100.0 / totalStarPowers;

				currentCombo = basePlayers[0].Combo;
				highestCombo = basePlayers[0].HighestCombo;

				UpdateGameplayLabel(TimeNameLabel, config.TimeName, config.TimeName.Format);
				UpdateGameplayLabel(SongTimeLabel, config.SongTime, formattedSongTime);
				UpdateGameplayLabel(SongLengthLabel, config.SongLength, formattedSongLength);

				UpdateGameplayLabel(CurrentStarProgressNameLabel, config.CurrentStarProgressName, string.Format(config.CurrentStarProgressName.Format, currentStarCount, Math.Min(7, currentStarCount + 1)));
				UpdateGameplayLabel(CurrentStarProgressScoreLabel, config.CurrentStarProgressScore, string.Format(config.CurrentStarProgressScore.Format, currentScore - previousStarScore));
				UpdateGameplayLabel(CurrentStarProgressEndScoreLabel, config.CurrentStarProgressEndScore, string.Format(config.CurrentStarProgressEndScore.Format, nextStarScore - previousStarScore));
				UpdateGameplayLabel(CurrentStarProgressPercentageLabel, config.CurrentStarProgressPercentage, string.Format(config.CurrentStarProgressPercentage.Format, nextStarPercentage.ToString("0.00")));

				UpdateGameplayLabel(SevenStarProgressNameLabel, config.SevenStarProgressName, string.Format(config.SevenStarProgressName.Format, 0, 7));
				UpdateGameplayLabel(SevenStarProgressScoreLabel, config.SevenStarProgressScore, string.Format(config.SevenStarProgressScore.Format, currentScore));
				UpdateGameplayLabel(SevenStarProgressEndScoreLabel, config.SevenStarProgressEndScore, string.Format(config.SevenStarProgressEndScore.Format, sevenStarScore));
				UpdateGameplayLabel(SevenStarProgressPercentageLabel, config.SevenStarProgressPercentage, string.Format(config.SevenStarProgressPercentage.Format, sevenStarPercentage.ToString("0.00")));

				UpdateGameplayLabel(NotesNameLabel, config.NotesName, config.NotesName.Format);
				UpdateGameplayLabel(NotesHitCounterLabel, config.NotesHitCounter, string.Format(config.NotesHitCounter.Format, hitNotes));
				UpdateGameplayLabel(NotesPassedCounterLabel, config.NotesPassedCounter, string.Format(config.NotesPassedCounter.Format, seenNotes));
				UpdateGameplayLabel(TotalNotesCounterLabel, config.TotalNotesCounter, string.Format(config.TotalNotesCounter.Format, totalNoteCount));
				UpdateGameplayLabel(NotesHitPercentageLabel, config.NotesHitPercentage, string.Format(config.NotesHitPercentage.Format, hitNotesPercentage.ToString("0.00")));
				UpdateGameplayLabel(NotesMissedCounterLabel, config.NotesMissedCounter, string.Format(config.NotesMissedCounter.Format, fcIndicator));

				UpdateGameplayLabel(StarPowerNameLabel, config.StarPowerName, config.StarPowerName.Format);
				UpdateGameplayLabel(StarPowersGottenCounterLabel, config.StarPowersGottenCounter, string.Format(config.StarPowersGottenCounter.Format, starPowersGotten));
				UpdateGameplayLabel(TotalStarPowersCounterLabel, config.TotalStarPowersCounter, string.Format(config.TotalStarPowersCounter.Format, totalStarPowers));
				UpdateGameplayLabel(StarPowerPercentageLabel, config.StarPowerPercentage, string.Format(config.StarPowerPercentage.Format, starPowerPercentage.ToString("0.00")));

				UpdateGameplayLabel(ComboNameLabel, config.ComboName, config.ComboName.Format);
				UpdateGameplayLabel(CurrentComboCounterLabel, config.CurrentComboCounter, string.Format(config.CurrentComboCounter.Format, currentCombo));
				UpdateGameplayLabel(HighestComboCounterLabel, config.HighestComboCounter, string.Format(config.HighestComboCounter.Format, highestCombo));
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
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(187000001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 807.0f), settingsOnWindow, new GUIContent("Extra Song UI Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
		}

		#endregion

		#region OnWindow Methods

		private void OnWindowHead(int id) {
			var smallLabelStyle = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			var styles = new GUIConfigurationStyles {
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
			};

			GUILayout.Label("Settings", largeLabelStyle);
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
			if (GUILayout.Button("Combo", settingsButtonStyle)) {
				settingsOnWindow = OnWindowCombo;
			}

			GUILayout.Space(25.0f);
			GUILayout.Label("Enable/Disable Keybind", largeLabelStyle);
			config.EnabledKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Configuration Keybind", largeLabelStyle);
			config.ConfigKeyBind.ConfigureGUI(styles);

			GUILayout.Space(25.0f);
			GUILayout.Label("Debugging Tools", largeLabelStyle);
			config.DraggableLabelsEnabled = GUILayout.Toggle(config.DraggableLabelsEnabled, "Draggable Labels", styles.Toggle);
			GUILayout.Label("Allows you to drag each label with a window", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			GUILayout.Label("(this disables some options in this window)", new GUIStyle(styles.SmallLabel) {
				fontStyle = FontStyle.Italic
			});
			config.TimeName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SongTime.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SongLength.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.CurrentStarProgressName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.CurrentStarProgressScore.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.CurrentStarProgressEndScore.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.CurrentStarProgressPercentage.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SevenStarProgressName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SevenStarProgressScore.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SevenStarProgressEndScore.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.SevenStarProgressPercentage.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.NotesName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.NotesHitCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.NotesPassedCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.TotalNotesCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.NotesHitPercentage.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.NotesMissedCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.StarPowerName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.StarPowersGottenCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.TotalStarPowersCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.StarPowerPercentage.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.ComboName.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.CurrentComboCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
			config.HighestComboCounter.DraggableWindowsEnabled = config.DraggableLabelsEnabled;

			GUILayout.Space(20.0f);
			config.Enabled = !GUILayout.Toggle(!config.Enabled, "Hide all extra UI", settingsToggleStyle);
			GUILayout.Space(20.0f);
			if (GUILayout.Button("Save Config", settingsButtonStyle)) config.SaveConfig();
			GUILayout.Space(50.0f);

			GUILayout.Label($"Extra Song UI v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUI.DragWindow();
		}

		private void OnWindowTime(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Time", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TimeName;
				settingsCurrentlyEditingName = "Time Name Label";
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Time", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongTime;
				settingsCurrentlyEditingName = "Song Time";
				settingsCurrentBack = OnWindowTime;
			}
			if (GUILayout.Button("Song Length", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SongLength;
				settingsCurrentlyEditingName = "Song Length";
				settingsCurrentBack = OnWindowTime;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowCurrentStar(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Current Star", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressName;
				settingsCurrentlyEditingName = "Current Star Progress Name Label";
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Current Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressScore;
				settingsCurrentlyEditingName = "Current Star Progress Score";
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("End Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressEndScore;
				settingsCurrentlyEditingName = "Current Star Progress End Score";
				settingsCurrentBack = OnWindowCurrentStar;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentStarProgressPercentage;
				settingsCurrentlyEditingName = "Current Star Progress Percentage";
				settingsCurrentBack = OnWindowCurrentStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowSevenStar(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Seven Star", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressName;
				settingsCurrentlyEditingName = "Seven Star Progress Name Label";
				settingsCurrentBack = OnWindowSevenStar;
			}
			if (GUILayout.Button("Current Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressScore;
				settingsCurrentlyEditingName = "Seven Star Progress Score";
				settingsCurrentBack = OnWindowSevenStar;
			}
			if (GUILayout.Button("End Score", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressEndScore;
				settingsCurrentlyEditingName = "Seven Star Progress End Score";
				settingsCurrentBack = OnWindowSevenStar;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.SevenStarProgressPercentage;
				settingsCurrentlyEditingName = "Seven Star Progress Percentage";
				settingsCurrentBack = OnWindowSevenStar;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowNotes(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Notes", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesName;
				settingsCurrentlyEditingName = "Notes Name Label";
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitCounter;
				settingsCurrentlyEditingName = "Notes Hit Counter";
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Passed Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesPassedCounter;
				settingsCurrentlyEditingName = "Notes Passed Counter";
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Total Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalNotesCounter;
				settingsCurrentlyEditingName = "Total Notes Counter";
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Hit Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesHitPercentage;
				settingsCurrentlyEditingName = "Notes Hit Percentage";
				settingsCurrentBack = OnWindowNotes;
			}
			if (GUILayout.Button("Missed Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.NotesMissedCounter;
				settingsCurrentlyEditingName = "Notes Missed Counter";
				settingsCurrentBack = OnWindowNotes;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowStarPower(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Star Power", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerName;
				settingsCurrentlyEditingName = "Star Power Name Label";
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Hit Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowersGottenCounter;
				settingsCurrentlyEditingName = "Star Powers Hit Counter";
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Total Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.TotalStarPowersCounter;
				settingsCurrentlyEditingName = "Total Star Powers Counter";
				settingsCurrentBack = OnWindowStarPower;
			}
			if (GUILayout.Button("Percentage", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.StarPowerPercentage;
				settingsCurrentlyEditingName = "Star Powers Hit Percentage";
				settingsCurrentBack = OnWindowStarPower;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowCombo(int id) {
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				fontStyle = FontStyle.Bold,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label("Combo", largeLabelStyle);
			if (GUILayout.Button("Name Label", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.ComboName;
				settingsCurrentlyEditingName = "Combo Name Label";
				settingsCurrentBack = OnWindowCombo;
			}
			if (GUILayout.Button("Current Combo Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.CurrentComboCounter;
				settingsCurrentlyEditingName = "Current Combo Counter";
				settingsCurrentBack = OnWindowCombo;
			}
			if (GUILayout.Button("Highest Combo Counter", settingsButtonStyle)) {
				settingsOnWindow = OnWindowEdit;
				settingsCurrentlyEditing = config.HighestComboCounter;
				settingsCurrentlyEditingName = "Highest Combo Counter";
				settingsCurrentBack = OnWindowCombo;
			}
			GUILayout.Space(50.0f);
			if (GUILayout.Button("Back", settingsButtonStyle)) {
				settingsOnWindow = OnWindowHead;
			}
			GUI.DragWindow();
		}

		private void OnWindowEdit(int id) {
			var smallLabelStyle = new GUIStyle {
				fontSize = 14,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			var largeLabelStyle = new GUIStyle {
				fontSize = 20,
				alignment = TextAnchor.UpperLeft,
				normal = new GUIStyleState {
					textColor = Color.white,
				}
			};
			GUILayout.Label(settingsCurrentlyEditingName, largeLabelStyle);
			settingsCurrentlyEditing.ConfigureGUI(new GUIConfigurationStyles {
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