using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using ExtraSongUI.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GUI;

namespace ExtraSongUI {
	[BepInPlugin("com.biendeo.extrasongui", "Extra Song UI", "1.5.0.0")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class SongUI : BaseUnityPlugin {
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

		private string ConfigPath => Path.Combine(Paths.ConfigPath, Info.Metadata.GUID + "layout.xml");
		private Config config;

		private List<Tuple<SongUILabel, GameObject, Text>> labels;


		private SongUILabel settingsCurrentlyEditing;

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

		private string formattedSongTime;
		private string formattedSongLength;
		private double songTimePercentage;

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
		private double seenNotesPercentage;
		private string fcIndicator;

		private int starPowersGotten;
		private double starPowerPercentage;
		private double currentStarPower;

		private int currentCombo;
		private int highestCombo;

		private int currentNoteIndex;

		private readonly VersionCheck versionCheck;
		private Rect changelogRect;

		public SongUI() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
			labels = new List<Tuple<SongUILabel, GameObject, Text>>();
		}

		#region Unity Methods

		public void Start() {
			config = Settings.Config.LoadConfig(ConfigPath);
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		private void DestroyLabels() {
			foreach (var label in labels) {
				Destroy(label.Item2);
			}
			labels.Clear();
		}

		private void CreateGameplayLabel(Transform canvasTransform, SongUILabel labelDetails, Font uiFont) {
			var o = new GameObject($"Extra Song UI - {labelDetails.Name}");
			var text = o.AddComponent<Text>();
			o.layer = LayerMask.NameToLayer("UI");
			o.transform.SetParent(canvasTransform);
			o.transform.SetSiblingIndex(0);
			o.transform.localEulerAngles = new Vector3();
			o.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.font = uiFont;
			labels.Add(new Tuple<SongUILabel, GameObject, Text>(labelDetails, o, text));
		}

		private void UpdateGameplayLabel(SongUILabel labelSettings, GameObject textObject, Text textLabel) {
			if (labelSettings.Visible && config.Enabled) {
				textObject.transform.localPosition = new Vector3(labelSettings.X - Screen.width / 2, Screen.height / 2 - labelSettings.Y);
				textLabel.enabled = true;
				textLabel.fontSize = labelSettings.Size;
				textLabel.alignment = labelSettings.Alignment;
				textLabel.fontStyle = (labelSettings.Bold ? FontStyle.Bold : FontStyle.Normal) | (labelSettings.Italic ? FontStyle.Italic : FontStyle.Normal);
				textLabel.color = labelSettings.Color.Color;
				textLabel.text = FormatSongLabel(labelSettings.Format);
			} else {
				textLabel.enabled = false;
			}
		}

		private string FormatSongLabel(string formatString) {
			var matches = Regex.Matches(formatString, @"{.+?}");
			for (int i = 0; i < matches.Count; ++i) {
				var match = matches[i];
				var matchTerm = match.Groups[0].Value.Substring(1, match.Groups[0].Value.Length - 2);
				string[] splitTerm = matchTerm.Split(':');
				string substitute = splitTerm[0].ToLower();
				string format = splitTerm.Length > 1 ? matchTerm.Substring(substitute.Length + 1) : string.Empty;
				string matchedString = $"{{{matchTerm}}}";
				string substitutedString = $"{{{matchTerm}}}";
				switch (substitute) {
					case "songtime":
						substitutedString = formattedSongTime;
						break;
					case "songlength":
						substitutedString = formattedSongLength;
						break;
					case "songtimepercentage":
						substitutedString = songTimePercentage.ToString(format);
						break;
					case "currentstar":
						substitutedString = currentStarCount.ToString(format);
						break;
					case "nextstar":
						substitutedString = (currentStarCount + 1).ToString(format);
						break;
					case "currentscore":
						substitutedString = currentScore.ToString(format);
						break;
					case "currentstarscore":
						substitutedString = (currentScore - previousStarScore).ToString(format);
						break;
					case "previousstarscore":
						substitutedString = previousStarScore.ToString(format);
						break;
					case "nextstarscore":
						substitutedString = nextStarScore.ToString(format);
						break;
					case "currentstarpercentage":
						substitutedString = nextStarPercentage.ToString(format);
						break;
					case "sevenstarscore":
						substitutedString = sevenStarScore.ToString(format);
						break;
					case "sevenstarpercentage":
						substitutedString = sevenStarPercentage.ToString(format);
						break;
					case "hitnotes":
						substitutedString = hitNotes.ToString(format);
						break;
					case "seennotes":
						substitutedString = seenNotes.ToString(format);
						break;
					case "missednotes":
						substitutedString = missedNotes.ToString(format);
						break;
					case "totalnotes":
						substitutedString = totalNoteCount.ToString(format);
						break;
					case "hitnotespercentage":
						substitutedString = hitNotesPercentage.ToString(format);
						break;
					case "seennotespercentage":
						substitutedString = seenNotesPercentage.ToString(format);
						break;
					case "fcindicator":
						substitutedString = fcIndicator;
						break;
					case "starpowersgotten":
						substitutedString = starPowersGotten.ToString(format);
						break;
					case "totalstarpowers":
						substitutedString = totalStarPowers.ToString(format);
						break;
					case "starpowerpercentage":
						substitutedString = starPowerPercentage.ToString(format);
						break;
					case "currentstarpower":
						substitutedString = currentStarPower.ToString(format);
						break;
					case "currentcombo":
						substitutedString = currentCombo.ToString(format);
						break;
					case "highestcombo":
						substitutedString = highestCombo.ToString(format);
						break;
				}
				formatString = formatString.Replace(matchedString, substitutedString);
			}
			return formatString;
		}

		public void LateUpdate() {
			string sceneName = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (sceneName == "Gameplay") {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = GameManagerWrapper.Wrap(gameManagerObject.GetComponent<GameManager>());
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

					DestroyLabels();
					Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;

					foreach (var label in config.Layout) {
						CreateGameplayLabel(canvasTransform, label, uiFont);
					}

				} else {
					DestroyLabels();
				}
			}
			if (sceneName == "Gameplay" && !gameManager.IsNull() && gameManager.PracticeUI.PracticeUI == null) {
				// Song length
				formattedSongTime = DoubleToTimeString(gameManager.SongTime);
				formattedSongLength = DoubleToTimeString(gameManager.SongLength);
				songTimePercentage = Math.Max(Math.Min(gameManager.SongTime * 100.0 / gameManager.SongLength, 100.0), 0.0);

				// Star progress
				currentStarCount = starProgress.CurrentStar;
				currentScore = Math.Min(starProgress.LastScore, starProgress.StarScores[6]); //TODO: Migrate to ScoreManager 'cause this stops incremented after you reach 7-star.
				previousStarScore = starProgress.CurrentStar == 0 ? 0 : starProgress.StarScores[starProgress.CurrentStar - 1];
				nextStarScore = starProgress.StarScores[starProgress.CurrentStar];
				nextStarPercentage = starProgress.CurrentStar < 7 ? (currentScore - previousStarScore) * 100.0 / (nextStarScore - previousStarScore) : 100.0;
				sevenStarScore = starProgress.StarScores[6];
				sevenStarPercentage = Math.Min(currentScore * 100.0 / sevenStarScore, 100.0);

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
				hitNotesPercentage = totalNoteCount == 0 ? 100.0 : hitNotes * 100.0 / totalNoteCount;
				seenNotesPercentage = seenNotes == 0 ? 100.0 : hitNotes * 100.0 / seenNotes;
				fcIndicator = seenNotes == hitNotes ? (!gameManager.BasePlayers[0].FirstNoteMissed ? "FC" : "100%") : $"-{missedNotes}";

				starPowersGotten = basePlayers[0].StarPowersHit;
				starPowerPercentage = totalStarPowers == 0 ? 100.0 : starPowersGotten * 100.0 / totalStarPowers;
				currentStarPower = basePlayers[0].SPBar.SomeFloat * 100.0;

				currentCombo = basePlayers[0].Combo;
				highestCombo = basePlayers[0].HighestCombo;

				foreach (var label in labels) {
					UpdateGameplayLabel(label.Item1, label.Item2, label.Item3);
				}
			}
			if (uiFont is null && sceneName == "Main Menu") {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
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
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(187000001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 807.0f), OnWindow, new GUIContent("Extra Song UI Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187000998, changelogRect, OnChangelogWindow, new GUIContent($"Extra Song UI Changelog"), settingsWindowStyle);
			}
		}

		#endregion

		#region OnWindow Methods

		private void OnWindow(int id) {
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
			if (settingsCurrentlyEditing == null) {
				settingsScrollPosition = GUILayout.BeginScrollView(settingsScrollPosition);
				GUILayout.Label("Settings", largeLabelStyle);

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

				foreach (var label in config.Layout) {
					label.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
				}

				GUILayout.Space(20.0f);
				config.Enabled = !GUILayout.Toggle(!config.Enabled, "Hide all extra UI", settingsToggleStyle);
				
				GUILayout.Space(20.0f);
				for (int i = 0; i < config.Layout.Count; ++i) {
					GUILayout.Label($"#{i + 1}", styles.SmallLabel);
					var label = config.Layout[i];
					if (GUILayout.Button($"Edit: {label.Name}", styles.Button)) {
						settingsCurrentlyEditing = label;
					}
					GUILayout.BeginHorizontal();
					if (i > 0 && GUILayout.Button("Shift up")) {
						config.Layout[i] = config.Layout[i - 1];
						config.Layout[i - 1] = label;
					}
					if (i < config.Layout.Count - 1 && GUILayout.Button("Shift down")) {
						config.Layout[i] = config.Layout[i + 1];
						config.Layout[i + 1] = label;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Delete")) {
						config.Layout.RemoveAt(i);
						var existingLabel = labels.FirstOrDefault(l => l.Item1 == label);
						if (existingLabel != default) {
							Destroy(existingLabel.Item2);
							labels.Remove(existingLabel);
						}
					}
					if (GUILayout.Button("Insert new")) {
						config.Layout.Insert(i + 1, new SongUILabel {
							Name = "New Label",
							Format = "EDIT ME",
							X = Screen.width / 2,
							Y = Screen.height / 2,
							Size = 50,
							Alignment = TextAnchor.MiddleCenter,
							Bold = true,
							Italic = false,
							Visible = true,
							Color = new ColorARGB(Color.white)
						});
						if (SceneManager.GetActiveScene().name == "Gameplay") {
							Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;
							CreateGameplayLabel(canvasTransform, config.Layout[i + 1], uiFont);
						}
					}
					GUILayout.EndHorizontal();
					GUILayout.Space(15.0f);
				}
				if (config.Layout.Count == 0) {
					if (GUILayout.Button("Insert")) {
						config.Layout.Add(new SongUILabel {
							Name = "New Label",
							Format = "EDIT ME",
							X = Screen.width / 2,
							Y = Screen.height / 2,
							Size = 50,
							Alignment = TextAnchor.MiddleCenter,
							Bold = true,
							Italic = false,
							Visible = true,
							Color = new ColorARGB(Color.white)
						});
						if (SceneManager.GetActiveScene().name == "Gameplay") {
							Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;
							CreateGameplayLabel(canvasTransform, config.Layout[0], uiFont);
						}
					}
				}

				GUILayout.Space(20.0f);
				if (GUILayout.Button("Reload Config", styles.Button)) {
					DestroyLabels();
					config.ReloadConfig(ConfigPath);
				}
				if (GUILayout.Button("Save Config", settingsButtonStyle)) {
					config.SaveConfig(ConfigPath);
				}
				GUILayout.Space(50.0f);

				GUILayout.Label($"Extra Song UI v{versionCheck.AssemblyVersion}");
				GUILayout.Label("Tweak by Biendeo");
				GUILayout.Label("Thankyou for using this!");
				GUILayout.EndScrollView();
			} else {
				settingsCurrentlyEditing.ConfigureGUI(styles);

				if (GUILayout.Button("Back", styles.Button)) {
					settingsCurrentlyEditing = null;
				}
			}
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
			GUILayout.Label("Thankyou for downloading Extra Song UI!", largeLabelStyle);
			GUILayout.Label("When you enter a song, you should see a default amount of extra UI elements. You can press F5 at any point in time to enable/disable it.", smallLabelStyle);
			GUILayout.Label("Press Ctrl + Shift + F5 to enable/disable the config window.", smallLabelStyle);
			GUILayout.Label("The config window lets you change things such as the keys to enable/disable the UI, which elements appear, and what color and position they'll be at.", smallLabelStyle);
			GUILayout.Label("Please make sure to press the \"Save Config\" button at the bottom of the config window so that your settings are saved for the next time you run Clone Hero.", smallLabelStyle);
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			GUILayout.Label("Changelog", largeLabelStyle);
			GUILayout.Label("TODO", smallLabelStyle);
			GUILayout.Label("Thanks E2 and MWisBest for the help.", smallLabelStyle);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = versionCheck.AssemblyVersion;
				config.SaveConfig(ConfigPath);
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