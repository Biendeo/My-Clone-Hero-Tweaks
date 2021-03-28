using AccuracyIndicator.Components;
using AccuracyIndicator.Settings;
using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using BiendeoCHLib.Patches.Attributes;
using BiendeoCHLib.Wrappers;
using HarmonyLib;
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
	[HarmonyCHPatch(typeof(BaseGuitarPlayerWrapper), nameof(BaseGuitarPlayerWrapper.HitNote))]
	public class HitNoteHandler {
		[HarmonyCHPostfix]
		static void Postfix(object __0) {
			var note = NoteWrapper.Wrap(__0);
			AccuracyIndicator.Instance.HitNote(note);
		}
	}

	[HarmonyCHPatch(typeof(BasePlayerWrapper), nameof(BasePlayerWrapper.MissNote))]
	public class MissNoteHandler {
		[HarmonyCHPostfix]
		static void Postfix(object __0) {
			var note = NoteWrapper.Wrap(__0);
			AccuracyIndicator.Instance.MissNote(note);
		}
	}

	[BepInPlugin("com.biendeo.accuracyindicator", "Accuracy Indicator", "1.5.1")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class AccuracyIndicator : BaseUnityPlugin {
		public static AccuracyIndicator Instance { get; private set; }

		private bool sceneChanged;

		private GameManagerWrapper gameManager;

		private string ConfigPath => Path.Combine(Paths.ConfigPath, Info.Metadata.GUID + ".config.xml");
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

		private VersionCheck versionCheck;
		private Rect changelogRect;

		private Harmony Harmony;

		public AccuracyIndicator() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.accuracyindicator");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);

			lastSongTime = -5.0;
			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
		}

		~AccuracyIndicator() {
			Harmony.UnpatchAll();
		}

		#region Unity Methods

		public void Awake() {
			versionCheck = gameObject.AddComponent<VersionCheck>();
			versionCheck.InitializeSettings(Assembly.GetExecutingAssembly(), Config);
		}

		public void Start() {
			config = Settings.Config.LoadConfig(ConfigPath);
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
			var notes = gameManager.BasePlayers[0].Notes;
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
			Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;

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
					textComponents[y].font = BiendeoCHLib.BiendeoCHLib.Instance.CloneHeroDefaultFont;
					textComponents[y].fontSize = Screen.height * 30 / 1440;
					textComponents[y].alignment = TextAnchor.MiddleRight;
					textComponents[y].fontStyle = FontStyle.Bold;
					textComponents[y].horizontalOverflow = HorizontalWrapMode.Overflow;
					textComponents[y].verticalOverflow = VerticalWrapMode.Overflow;
				}
				if (x == 0) {
					textComponents[0].text = $"Very early ({highestVeryEarly * 1000.0f:0.00}ms):";
					int veryEarlies = noteHits.Count(nh => nh < highestVeryEarly);
					textComponents[1].text = veryEarlies.ToString();
					textComponents[2].text = $"({veryEarlies * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorVeryEarly.Color;
				} else if (x == 1) {
					textComponents[0].text = $"Early ({highestEarly * 1000.0f:0.00}ms):";
					int earlies = noteHits.Count(nh => nh < highestEarly && nh >= highestVeryEarly);
					textComponents[1].text = earlies.ToString();
					textComponents[2].text = $"({earlies * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorEarly.Color;
				} else if (x == 2) {
					textComponents[0].text = $"Slightly early ({highestSlightlyEarly * 1000.0f:0.00}ms):";
					int slightlyEarlies = noteHits.Count(nh => nh < highestSlightlyEarly && nh >= highestEarly);
					textComponents[1].text = slightlyEarlies.ToString();
					textComponents[2].text = $"({slightlyEarlies * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorSlightlyEarly.Color;
				} else if (x == 3) {
					textComponents[0].text = $"Perfect:";
					int perfects = noteHits.Count(nh => nh <= highestSlightlyLate && nh >= highestSlightlyEarly);
					textComponents[1].text = perfects.ToString();
					textComponents[2].text = $"({perfects * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorPerfect.Color;
				} else if (x == 4) {
					textComponents[0].text = $"Slightly late ({highestSlightlyLate * 1000.0f:0.00}ms):";
					int slightlyLates = noteHits.Count(nh => nh <= highestLate && nh > highestSlightlyLate);
					textComponents[1].text = slightlyLates.ToString();
					textComponents[2].text = $"({slightlyLates * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorSlightlyLate.Color;
				} else if (x == 5) {
					textComponents[0].text = $"Late ({highestLate * 1000.0f:0.00}ms):";
					int lates = noteHits.Count(nh => nh <= highestVeryLate && nh > highestLate);
					textComponents[1].text = lates.ToString();
					textComponents[2].text = $"({lates * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorLate.Color;
				} else if (x == 6) {
					textComponents[0].text = $"Very late ({highestVeryLate * 1000.0f:0.00}ms):";
					int veryLates = noteHits.Count(nh => nh > highestVeryLate);
					textComponents[1].text = veryLates.ToString();
					textComponents[2].text = $"({veryLates * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorVeryLate.Color;
				} else if (x == 7) {
					textComponents[0].text = $"Missed:";
					int misses = totalNoteCount - noteHits.Count();
					textComponents[1].text = misses.ToString();
					textComponents[2].text = $"({misses * 100.0 / totalNoteCount:0.00}%)";
					textComponents[0].color = config.ColorMissed.Color;
				} else if (x == 8) {
					textComponents[0].text = $"Average time:";
					textComponents[2].text = $"{noteHits.Average() * 1000.0f:0.00}ms";
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

		private void UpdateLabels() {
			if (config.Enabled) {
				double timeFromLastNote = gameManager.SongTime - lastNoteHitTime;

				// Determine what color and message displays.
				Color labelColor;
				string message;
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
					text.text = $"{lastNoteHitDifference * 1000.0f:0.00}ms";
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
					text.text = $"{hitAccuracy * 1000.0f:0.00}ms";
					text.color = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)));
				} else {
					averageAccuracyLabel.GetComponent<Text>().enabled = false;
				}
			}
		}

		public void LateUpdate() {
			string sceneName = SceneManager.GetActiveScene().name;
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (sceneName == "Gameplay") {
					int uiLayerMask = LayerMask.NameToLayer("UI");
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = GameManagerWrapper.Wrap(gameManagerObject.GetComponent<GameManager>());
					ResetGameplaySceneValues();

					DestroyAndNullGameplayLabels();
					Transform canvasTransform = FadeBehaviourWrapper.Instance.FadeGraphic.canvas.transform;

					accuracyIndicatorLabel = new GameObject($"Accuracy Indicator", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					accuracyIndicatorLabel.layer = uiLayerMask;
					accuracyIndicatorLabel.transform.SetParent(canvasTransform);
					accuracyIndicatorLabel.transform.SetSiblingIndex(0);
					accuracyIndicatorLabel.transform.localEulerAngles = new Vector3();
					accuracyIndicatorLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					accuracyIndicatorLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					accuracyIndicatorLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					accuracyIndicatorLabel.GetComponent<Text>().font = BiendeoCHLib.BiendeoCHLib.Instance.CloneHeroDefaultFont;

					accuracyMessageLabel = new GameObject($"Accuracy Message", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					accuracyMessageLabel.layer = uiLayerMask;
					accuracyMessageLabel.transform.SetParent(canvasTransform);
					accuracyMessageLabel.transform.SetSiblingIndex(0);
					accuracyMessageLabel.transform.localEulerAngles = new Vector3();
					accuracyMessageLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					accuracyMessageLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					accuracyMessageLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					accuracyMessageLabel.GetComponent<Text>().font = BiendeoCHLib.BiendeoCHLib.Instance.CloneHeroDefaultFont;

					averageAccuracyLabel = new GameObject($"Average Accuracy", new Type[] {
						typeof(Text),
						typeof(DestroyOnSceneChange)
					});
					averageAccuracyLabel.layer = uiLayerMask;
					averageAccuracyLabel.transform.SetParent(canvasTransform);
					averageAccuracyLabel.transform.SetSiblingIndex(0);
					averageAccuracyLabel.transform.localEulerAngles = new Vector3();
					averageAccuracyLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					averageAccuracyLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					averageAccuracyLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					averageAccuracyLabel.GetComponent<Text>().font = BiendeoCHLib.BiendeoCHLib.Instance.CloneHeroDefaultFont;
				} else {
					DestroyAndNullGameplayLabels();
				}
				if (config.Enabled && sceneName == "EndOfSong") {
					InstantiateEndOfSongLabels();
				}
			}
			if (sceneName == "Gameplay") {
				//! In practice mode, the song time is set to 1.5s before the section or A/B. If it is looping, it is
				//! initially set to 0, then to the appropriate time. As long as the user isn't on less than 10FPS, this should work.
				if (Math.Abs(gameManager.SongTime - lastSongTime) > 1.5 && gameManager.PracticeUI.PracticeUI != null) {
					ResetGameplaySceneValues();
				}
				UpdateGreatestThresholds();
				UpdateLabels();
			}
			config.HandleInput();
			if (!gameManager.IsNull()) {
				lastSongTime = gameManager.SongTime;
			}
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
				var outputRect = GUILayout.Window(187002001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Accuracy Indicator Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187002998, changelogRect, OnChangelogWindow, new GUIContent($"Perfect Mode Changelog"), settingsWindowStyle);
			}
		}

		#endregion

		internal void HitNote(NoteWrapper note) {
			lastNoteActualTime = note.Time;
			lastNoteHitTime = gameManager.SongTime;
			if (lastNoteHitTime > lastNoteActualTime && (lastNoteHitTime - Time.deltaTime) < lastNoteActualTime) {
				lastNoteHitDifference = 0.0f;
			} else if (lastNoteHitTime > lastNoteActualTime) {
				lastNoteHitDifference = (float)(lastNoteHitTime - Time.deltaTime - lastNoteActualTime);
			} else if (lastNoteHitTime < lastNoteActualTime) {
				lastNoteHitDifference = (float)(lastNoteHitTime - lastNoteActualTime);
			} else {
				Logger.LogError("Panic?");
			}
			noteHits.Add(lastNoteHitDifference);
			if (hitNotes == 0) {
				hitAccuracy = lastNoteHitDifference;
			} else {
				hitAccuracy = (hitAccuracy * hitNotes + lastNoteHitDifference) / (hitNotes + 1);
			}
			++hitNotes;
		}

		internal void MissNote(NoteWrapper note) {
			lastNoteActualTime = note.Time;
			lastNoteHitTime = gameManager.SongTime;
			lastNoteHitDifference = 0.07f;
		}

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
			config.ConfigureGUI(new BiendeoCHLib.Settings.GUIConfigurationStyles {
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
			if (GUILayout.Button("Reload Config", settingsButtonStyle)) {
				config.ReloadConfig(ConfigPath);
			}
			if (GUILayout.Button("Save Config", settingsButtonStyle)) {
				config.SaveConfig(ConfigPath);
			}

			GUILayout.Space(25.0f);

			GUILayout.Label($"Accuracy Indicator v{versionCheck.AssemblyVersion}");
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
			GUILayout.Label("BepInEx is used as the mod loading framework now. This should lead to more robust features for mod developers.", smallLabelStyle);
			GUILayout.Label("Performance improvements (hopefully) by perform all the logic on note hits rather than polling every frame.", smallLabelStyle);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = versionCheck.AssemblyVersion;
				config.SaveConfig(ConfigPath);
			}
			GUI.DragWindow();
		}
	}
}