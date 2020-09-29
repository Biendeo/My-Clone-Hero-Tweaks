using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using BiendeoCHLib.Patches.Attributes;
using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using ExtraSongUI.Settings;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GUI;

namespace ExtraSongUI {
	[HarmonyCHPatch(typeof(BaseGuitarPlayerWrapper), nameof(BaseGuitarPlayerWrapper.HitNote))]
	public class HitNoteHandler {
		[HarmonyCHPostfix]
		static void Postfix(BaseGuitarPlayer __instance, object __0) {
			var player = BasePlayerWrapper.Wrap(__instance);
			var note = NoteWrapper.Wrap(__0);
			SongUI.Instance.HitNote(player, note);
		}
	}

	[HarmonyCHPatch(typeof(BasePlayerWrapper), nameof(BasePlayerWrapper.MissNote))]
	public class MissNoteHandler {
		[HarmonyCHPostfix]
		static void Postfix(BasePlayer __instance, object __0) {
			var player = BasePlayerWrapper.Wrap(__instance);
			var note = NoteWrapper.Wrap(__0);
			SongUI.Instance.MissNote(player, note);
		}
	}

	[HarmonyCHPatch(typeof(BasePlayerWrapper), nameof(BasePlayerWrapper.OverStrum))]
	public class OverStrumHandler {
		[HarmonyCHPostfix]
		static void Postfix(BasePlayer __instance, bool __0) {
			var player = BasePlayerWrapper.Wrap(__instance);
			SongUI.Instance.OverStrum(player, __0);
		}
	}

	[BepInPlugin("com.biendeo.extrasongui", "Extra Song UI", "1.5.0.0")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class SongUI : BaseUnityPlugin {
		public static SongUI Instance { get; private set; }

		private bool sceneChanged;

		// Song length
		private GameManagerWrapper gameManager;

		// Star progress
		private StarProgressWrapper starProgress;

		// Note count
		private BasePlayerWrapper[] basePlayers;
		private int[] totalNoteCount;
		private int[] totalStarPowers;

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

		private TimeSpan songTime;
		private TimeSpan songLength;
		private double songTimePercentage;

		private int numPlayers;

		private int currentStarCount;
		private int currentScore;
		private int previousStarScore;
		private int nextStarScore;
		private double nextStarPercentage;
		private int sevenStarScore;
		private double sevenStarPercentage;

		private const int bandIndex = 4;

		private int[] hitNotes;
		private int[] missedNotes;
		private int[] seenNotes;
		private double[] hitNotesPercentage;
		private double[] seenNotesPercentage;
		private string[] fcIndicator;

		private int[] starPowersGotten;
		private double[] starPowerPercentage;
		private double[] currentStarPower;

		private int[] currentCombo;
		private int[] highestCombo;

		private readonly VersionCheck versionCheck;
		private Rect changelogRect;

		private readonly Dictionary<string, Func<string, string>> formatActions;

		private Harmony Harmony;

		public SongUI() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.extrasongui");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);

			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
			labels = new List<Tuple<SongUILabel, GameObject, Text>>();

			totalNoteCount = new int[bandIndex + 1];
			totalStarPowers = new int[bandIndex + 1];

			hitNotes = new int[bandIndex + 1];
			missedNotes = new int[bandIndex + 1];
			seenNotes = new int[bandIndex + 1];
			hitNotesPercentage = new double[bandIndex + 1];
			seenNotesPercentage = new double[bandIndex + 1];
			fcIndicator = new string[bandIndex + 1];

			starPowersGotten = new int[bandIndex + 1];
			starPowerPercentage = new double[bandIndex + 1];
			currentStarPower = new double[bandIndex + 1];

			currentCombo = new int[bandIndex + 1];
			highestCombo = new int[bandIndex + 1];

			formatActions = new Dictionary<string, Func<string, string>> {
				{ "songtime", (format) => songTime.ToString(format) },
				{ "songlength", (format) => songLength.ToString(format) },
				{ "songtimepercentage", (format) => songTimePercentage.ToString(format) },
				{ "currentstar", (format) => currentStarCount.ToString(format) },
				{ "nextstar", (format) => (currentStarCount + 1).ToString(format) },
				{ "currentscore", (format) => currentScore.ToString(format) },
				{ "currentstarscore", (format) => (currentScore - previousStarScore).ToString(format) },
				{ "previousstarscore", (format) => previousStarScore.ToString(format) },
				{ "nextstarscore", (format) => nextStarScore.ToString(format) },
				{ "currentstarpercentage", (format) => nextStarPercentage.ToString(format) },
				{ "sevenstarscore", (format) => sevenStarScore.ToString(format) },
				{ "sevenstarpercentage", (format) => sevenStarPercentage.ToString(format) },
				{ "hitnotes", (format) => hitNotes[bandIndex].ToString(format) },
				{ "hitnotes1", (format) => hitNotes[0].ToString(format) },
				{ "hitnotes2", (format) => hitNotes[1].ToString(format) },
				{ "hitnotes3", (format) => hitNotes[2].ToString(format) },
				{ "hitnotes4", (format) => hitNotes[3].ToString(format) },
				{ "seennotes", (format) => seenNotes[bandIndex].ToString(format) },
				{ "seennotes1", (format) => seenNotes[0].ToString(format) },
				{ "seennotes2", (format) => seenNotes[1].ToString(format) },
				{ "seennotes3", (format) => seenNotes[2].ToString(format) },
				{ "seennotes4", (format) => seenNotes[3].ToString(format) },
				{ "missednotes", (format) => missedNotes[bandIndex].ToString(format) },
				{ "missednotes1", (format) => missedNotes[0].ToString(format) },
				{ "missednotes2", (format) => missedNotes[1].ToString(format) },
				{ "missednotes3", (format) => missedNotes[2].ToString(format) },
				{ "missednotes4", (format) => missedNotes[3].ToString(format) },
				{ "totalnotes", (format) => totalNoteCount[bandIndex].ToString(format) },
				{ "totalnotes1", (format) => totalNoteCount[0].ToString(format) },
				{ "totalnotes2", (format) => totalNoteCount[1].ToString(format) },
				{ "totalnotes3", (format) => totalNoteCount[2].ToString(format) },
				{ "totalnotes4", (format) => totalNoteCount[3].ToString(format) },
				{ "hitnotespercentage", (format) => hitNotesPercentage[bandIndex].ToString(format) },
				{ "hitnotespercentage1", (format) => hitNotesPercentage[0].ToString(format) },
				{ "hitnotespercentage2", (format) => hitNotesPercentage[1].ToString(format) },
				{ "hitnotespercentage3", (format) => hitNotesPercentage[2].ToString(format) },
				{ "hitnotespercentage4", (format) => hitNotesPercentage[3].ToString(format) },
				{ "seennotespercentage", (format) => seenNotesPercentage[bandIndex].ToString(format) },
				{ "seennotespercentage1", (format) => seenNotesPercentage[0].ToString(format) },
				{ "seennotespercentage2", (format) => seenNotesPercentage[1].ToString(format) },
				{ "seennotespercentage3", (format) => seenNotesPercentage[2].ToString(format) },
				{ "seennotespercentage4", (format) => seenNotesPercentage[3].ToString(format) },
				{ "fcindicator", (format) => fcIndicator[bandIndex] },
				{ "fcindicator1", (format) => fcIndicator[0] },
				{ "fcindicator2", (format) => fcIndicator[1] },
				{ "fcindicator3", (format) => fcIndicator[2] },
				{ "fcindicator4", (format) => fcIndicator[3] },
				{ "starpowersgotten", (format) => starPowersGotten[bandIndex].ToString(format) },
				{ "starpowersgotten1", (format) => starPowersGotten[0].ToString(format) },
				{ "starpowersgotten2", (format) => starPowersGotten[1].ToString(format) },
				{ "starpowersgotten3", (format) => starPowersGotten[2].ToString(format) },
				{ "starpowersgotten4", (format) => starPowersGotten[3].ToString(format) },
				{ "totalstarpowers", (format) => totalStarPowers[bandIndex].ToString(format) },
				{ "totalstarpowers1", (format) => totalStarPowers[0].ToString(format) },
				{ "totalstarpowers2", (format) => totalStarPowers[1].ToString(format) },
				{ "totalstarpowers3", (format) => totalStarPowers[2].ToString(format) },
				{ "totalstarpowers4", (format) => totalStarPowers[3].ToString(format) },
				{ "starpowerpercentage", (format) => starPowerPercentage[bandIndex].ToString(format) },
				{ "starpowerpercentage1", (format) => starPowerPercentage[0].ToString(format) },
				{ "starpowerpercentage2", (format) => starPowerPercentage[1].ToString(format) },
				{ "starpowerpercentage3", (format) => starPowerPercentage[2].ToString(format) },
				{ "starpowerpercentage4", (format) => starPowerPercentage[3].ToString(format) },
				{ "currentstarpower", (format) => currentStarPower[bandIndex].ToString(format) },
				{ "currentstarpower1", (format) => currentStarPower[0].ToString(format) },
				{ "currentstarpower2", (format) => currentStarPower[1].ToString(format) },
				{ "currentstarpower3", (format) => currentStarPower[2].ToString(format) },
				{ "currentstarpower4", (format) => currentStarPower[3].ToString(format) },
				{ "currentcombo", (format) => currentCombo[bandIndex].ToString(format) },
				{ "currentcombo1", (format) => currentCombo[0].ToString(format) },
				{ "currentcombo2", (format) => currentCombo[1].ToString(format) },
				{ "currentcombo3", (format) => currentCombo[2].ToString(format) },
				{ "currentcombo4", (format) => currentCombo[3].ToString(format) },
				{ "highestcombo", (format) => highestCombo[bandIndex].ToString(format) },
				{ "highestcombo1", (format) => highestCombo[0].ToString(format) },
				{ "highestcombo2", (format) => highestCombo[1].ToString(format) },
				{ "highestcombo3", (format) => highestCombo[2].ToString(format) },
				{ "highestcombo4", (format) => highestCombo[3].ToString(format) }
			};
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
				try {
					substitutedString = formatActions[substitute](format);
				} catch (FormatException) {
					Logger.LogError($"Format string {format} is invalid for type {matchTerm}");
				} catch (KeyNotFoundException) {
					Logger.LogError($"Match term {matchTerm} does not have an associated format");
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
					songLength = TimeSpan.FromSeconds(gameManager.SongLength);
					numPlayers = basePlayers.Count(bp => !bp.IsNull());
					for (int i = bandIndex; i >= 0; --i) {
						fcIndicator[i] = "FC";
						if (i == bandIndex) {
							totalNoteCount[i] = 0;
							totalStarPowers[i] = 0;
						} else {
							var player = basePlayers[i];
							if (!player.IsNull()) {
								var notes = player.Notes;
								totalNoteCount[bandIndex] += (totalNoteCount[i] = notes.Count);
								totalStarPowers[bandIndex] += (totalStarPowers[i] = notes.Count(n => n.IsStarPowerEnd));
							} else {
								totalNoteCount[i] = 0;
								hitNotesPercentage[i] = 100.0;
								seenNotesPercentage[i] = 100.0;
								totalStarPowers[i] = 0;
								starPowerPercentage[i] = 0.00;
							}
						}
						hitNotes[i] = 0;
						hitNotesPercentage[i] = 0.00;
						missedNotes[i] = 0;
						seenNotesPercentage[i] = 0.00;
						seenNotes[i] = 0;
						starPowerPercentage[i] = 0.00;
						currentCombo[i] = 0;
						highestCombo[i] = 0;
					}

					DestroyLabels();
					Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;

					foreach (var label in config.Layout[numPlayers - 1]) {
						CreateGameplayLabel(canvasTransform, label, uiFont);
					}

				} else {
					DestroyLabels();
				}
			}
			if (sceneName == "Gameplay" && !gameManager.IsNull() && gameManager.PracticeUI.PracticeUI == null) {
				// Song length
				songTime = TimeSpan.FromSeconds(gameManager.SongTime);
				songTimePercentage = Math.Max(Math.Min(gameManager.SongTime * 100.0 / gameManager.SongLength, 100.0), 0.0);

				// Star progress
				currentStarCount = starProgress.CurrentStar;
				currentScore = Math.Min(starProgress.LastScore, starProgress.StarScores[6]); //TODO: Migrate to ScoreManager 'cause this stops incremented after you reach 7-star.
				previousStarScore = starProgress.CurrentStar == 0 ? 0 : starProgress.StarScores[starProgress.CurrentStar - 1];
				nextStarScore = starProgress.StarScores[starProgress.CurrentStar];
				nextStarPercentage = starProgress.CurrentStar < 7 ? (currentScore - previousStarScore) * 100.0 / (nextStarScore - previousStarScore) : 100.0;
				sevenStarScore = starProgress.StarScores[6];
				sevenStarPercentage = Math.Min(currentScore * 100.0 / sevenStarScore, 100.0);

				currentStarPower[bandIndex] = 0.0;
				for (int i = 0; i < bandIndex; ++i) {
					var player = basePlayers[i];
					if (!player.IsNull()) {
						currentStarPower[i] = player.SPBar.SomeFloat * 100.0;
						currentStarPower[bandIndex] += currentStarPower[i];
					}
				}

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

		internal void HitNote(BasePlayerWrapper player, NoteWrapper note) {
			int playerIndex = player.Player.PlayerIndex;
			++hitNotes[playerIndex];
			++hitNotes[bandIndex];
			++seenNotes[playerIndex];
			++seenNotes[bandIndex];
			++currentCombo[playerIndex];
			highestCombo[playerIndex] = Math.Max(currentCombo[playerIndex], highestCombo[playerIndex]);
			++currentCombo[bandIndex];
			highestCombo[bandIndex] = Math.Max(currentCombo[bandIndex], highestCombo[bandIndex]);
			if (note.IsStarPowerEnd) {
				++starPowersGotten[playerIndex];
				++starPowersGotten[bandIndex];
			}

			UpdateNotePercentages(playerIndex);
			UpdateNotePercentages(bandIndex);
		}

		internal void MissNote(BasePlayerWrapper player, NoteWrapper note) {
			int playerIndex = player.Player.PlayerIndex;
			++missedNotes[playerIndex];
			++missedNotes[bandIndex];
			++seenNotes[playerIndex];
			++seenNotes[bandIndex];
			//TODO: Handle star powers missed.
			currentCombo[playerIndex] = 0;
			currentCombo[bandIndex] = 0;
			fcIndicator[playerIndex] = seenNotes[playerIndex] == hitNotes[playerIndex] ? (!player.FirstNoteMissed ? "FC" : "100%") : $"-{missedNotes[playerIndex]}";
			fcIndicator[bandIndex] = seenNotes[bandIndex] == hitNotes[bandIndex] ? (!basePlayers.Any(b => b.FirstNoteMissed) ? "FC" : "100%") : $"-{missedNotes[bandIndex]}";

			UpdateNotePercentages(playerIndex);
			UpdateNotePercentages(bandIndex);
		}

		internal void OverStrum(BasePlayerWrapper player, bool strummed) {
			int playerIndex = player.Player.PlayerIndex;
			//TODO: Handle star powers missed.
			currentCombo[playerIndex] = 0;
			currentCombo[bandIndex] = 0;
			if (seenNotes[playerIndex] == hitNotes[playerIndex]) {
				fcIndicator[playerIndex] = "100%";
			}
			if (seenNotes[bandIndex] == hitNotes[bandIndex]) {
				fcIndicator[bandIndex] = "100%";
			}
			Logger.LogDebug($"Overstrum {strummed}, seenNotes: {seenNotes[playerIndex]}, hitNotes: {hitNotes[playerIndex]}");
		}

		internal void UpdateNotePercentages(int index) {
			hitNotesPercentage[index] = totalNoteCount[index] == 0 ? 100.0 : hitNotes[index] * 100.0 / totalNoteCount[index];
			seenNotesPercentage[index] = seenNotes[index] == 0 ? 100.0 : hitNotes[index] * 100.0 / seenNotes[index];
			starPowerPercentage[index] = totalStarPowers[index] == 0 ? 100.0 : starPowersGotten[index] * 100.0 / totalStarPowers[index];
		}

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

				foreach (var layout in config.Layout) {
					foreach (var label in layout) {
						label.DraggableWindowsEnabled = config.DraggableLabelsEnabled;
					}
				}

				GUILayout.Space(20.0f);
				config.Enabled = !GUILayout.Toggle(!config.Enabled, "Hide all extra UI", settingsToggleStyle);

				GUILayout.Space(20.0f);
				GUILayout.Label("Layout for the following players:");
				GUILayout.BeginHorizontal();
				for (int i = 0; i < bandIndex; ++i) {
					if (GUILayout.Toggle(config.LayoutIndexSelected == i, $"{i + 1}", settingsToggleStyle)) {
						config.LayoutIndexSelected = i;
					}
				}
				GUILayout.EndHorizontal();
				config.Enabled = !GUILayout.Toggle(!config.Enabled, "Hide all extra UI", settingsToggleStyle);

				GUILayout.Space(20.0f);
				for (int i = 0; i < config.Layout[config.LayoutIndexSelected].Count; ++i) {
					GUILayout.Label($"#{i + 1}", styles.SmallLabel);
					var layout = config.Layout[config.LayoutIndexSelected];
					var label = layout[i];
					if (GUILayout.Button($"Edit: {label.Name}", styles.Button)) {
						settingsCurrentlyEditing = label;
					}
					GUILayout.BeginHorizontal();
					if (i > 0 && GUILayout.Button("Shift up")) {
						layout[i] = layout[i - 1];
						layout[i - 1] = label;
					}
					if (i < layout.Count - 1 && GUILayout.Button("Shift down")) {
						layout[i] = layout[i + 1];
						layout[i + 1] = label;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Delete")) {
						layout.RemoveAt(i);
						var existingLabel = labels.FirstOrDefault(l => l.Item1 == label);
						if (existingLabel != default) {
							Destroy(existingLabel.Item2);
							labels.Remove(existingLabel);
						}
					}
					if (GUILayout.Button("Insert new")) {
						layout.Insert(i + 1, new SongUILabel {
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
							CreateGameplayLabel(canvasTransform, layout[i + 1], uiFont);
						}
					}
					GUILayout.EndHorizontal();
					GUILayout.Space(15.0f);
				}
				if (config.Layout[config.LayoutIndexSelected].Count == 0) {
					if (GUILayout.Button("Insert")) {
						config.Layout[config.LayoutIndexSelected].Add(new SongUILabel {
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
							CreateGameplayLabel(canvasTransform, config.Layout[config.LayoutIndexSelected][0], uiFont);
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