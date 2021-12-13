using BepInEx;
using BiendeoCHLib;
using BiendeoCHLib.Patches;
using BiendeoCHLib.Patches.Attributes;
using BiendeoCHLib.Settings;
using BiendeoCHLib.Wrappers;
using HarmonyLib;
using PerfectMode.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PerfectMode {

	[HarmonyCHPatch(typeof(BasePlayerWrapper), nameof(BasePlayerWrapper.MissNote))]
	public class MissNoteHandler {
		[HarmonyCHPostfix]
		static void Postfix(object __0) {
			var note = NoteWrapper.Wrap(__0);
			PerfectMode.Instance.MissNote(note);
		}
	}

	[BepInPlugin("com.biendeo.perfectmode", "Perfect Mode", "1.5.2")]
	[BepInDependency("com.biendeo.biendeochlib")]
	public class PerfectMode : BaseUnityPlugin {
		public static PerfectMode Instance { get; private set; }

		private bool sceneChanged;

		private GameManagerWrapper gameManager;

		private bool failedObjective;
		private float remainingTimeBeforeRestart;

		private Font uiFont;
		private bool invokedSceneChange;

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

		private string target;
		private bool isStillFC;
		private int missedNotes;

		private GameObject displayImageLabel;
		private GameObject remainingNotesLeftLabel;
		private GameObject restartIndicatorLabel;

		private VersionCheck versionCheck;
		private Rect changelogRect;

		private Harmony Harmony;

		public PerfectMode() {
			Instance = this;
			Harmony = new Harmony("com.biendeo.perfectmode");
			PatchBase.InitializePatches(Harmony, Assembly.GetExecutingAssembly(), Logger);

			settingsScrollPosition = new Vector2();
			changelogRect = new Rect(400.0f, 400.0f, 100.0f, 100.0f);
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
				failedObjective = false;
			};
		}

		private void DestroyAndNullGameplayLabels() {
			if (displayImageLabel != null) Destroy(displayImageLabel);
			if (remainingNotesLeftLabel != null) Destroy(remainingNotesLeftLabel);
			if (restartIndicatorLabel != null) Destroy(restartIndicatorLabel);
			displayImageLabel = null;
			remainingNotesLeftLabel = null;
			restartIndicatorLabel = null;
		}

		private void ResetGameplaySceneValues() {
			missedNotes = 0;
			invokedSceneChange = false;
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

					displayImageLabel = new GameObject($"Perfect Mode Indicator", new Type[] {
						typeof(Text)
					});
					displayImageLabel.layer = uiLayerMask;
					displayImageLabel.transform.SetParent(canvasTransform);
					displayImageLabel.transform.SetSiblingIndex(0);
					displayImageLabel.transform.localEulerAngles = new Vector3();
					displayImageLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					displayImageLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					displayImageLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					displayImageLabel.GetComponent<Text>().font = uiFont;

					remainingNotesLeftLabel = new GameObject($"Perfect Mode Notes Remaining", new Type[] {
						typeof(Text)
					});
					remainingNotesLeftLabel.layer = uiLayerMask;
					remainingNotesLeftLabel.transform.SetParent(canvasTransform);
					remainingNotesLeftLabel.transform.SetSiblingIndex(0);
					remainingNotesLeftLabel.transform.localEulerAngles = new Vector3();
					remainingNotesLeftLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					remainingNotesLeftLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					remainingNotesLeftLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					remainingNotesLeftLabel.GetComponent<Text>().font = uiFont;

					restartIndicatorLabel = new GameObject($"Perfect Mode Restart Message", new Type[] {
						typeof(Text)
					});
					restartIndicatorLabel.layer = uiLayerMask;
					restartIndicatorLabel.transform.SetParent(canvasTransform);
					restartIndicatorLabel.transform.SetSiblingIndex(0);
					restartIndicatorLabel.transform.localEulerAngles = new Vector3();
					restartIndicatorLabel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					restartIndicatorLabel.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
					restartIndicatorLabel.GetComponent<Text>().verticalOverflow = VerticalWrapMode.Overflow;
					restartIndicatorLabel.GetComponent<Text>().font = uiFont;
				} else {
					DestroyAndNullGameplayLabels();
				}
			}
			if (config.Enabled && sceneName == "Gameplay" && !gameManager.IsNull()) {
				target = config.FC ? "FC" : (config.NotesMissed == 0 ? "100%" : $"-{config.NotesMissed}");
				isStillFC = !gameManager.BasePlayers[0].FirstNoteMissed;
				if (!failedObjective && (config.FC && !isStillFC || config.NotesMissed < missedNotes)) {
					failedObjective = true;
					remainingTimeBeforeRestart = Math.Min(config.FailDelay, (float)(gameManager.SongLength - gameManager.SongTime));
				}
				if (failedObjective && (gameManager.PauseMenu is null || !gameManager.PauseMenu.activeInHierarchy)) {
					remainingTimeBeforeRestart -= Time.deltaTime;
					if (remainingTimeBeforeRestart < 0.0f && !invokedSceneChange) {
						//TODO: Double-check that multiplayer works fine with this.
						StartCoroutine(FadeBehaviourWrapper.Instance.InvokeSceneChange("Gameplay"));
						invokedSceneChange = true;
					}
				}

				if (config.DisplayImage.Visible || config.LayoutTest) {
					displayImageLabel.transform.localPosition = new Vector3(config.DisplayImage.X - Screen.width / 2, Screen.height / 2 - config.DisplayImage.Y);
					var text = displayImageLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.DisplayImage.Size;
					text.alignment = config.DisplayImage.Alignment;
					text.fontStyle = (config.DisplayImage.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.DisplayImage.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{target} mode active";
					text.color = config.DisplayImage.Color.Color;
				} else {
					displayImageLabel.GetComponent<Text>().enabled = false;
				}

				if ((config.RemainingNotesLeft.Visible && !config.FC) || config.LayoutTest) {
					remainingNotesLeftLabel.transform.localPosition = new Vector3(config.RemainingNotesLeft.X - Screen.width / 2, Screen.height / 2 - config.RemainingNotesLeft.Y);
					var text = remainingNotesLeftLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.RemainingNotesLeft.Size;
					text.alignment = config.RemainingNotesLeft.Alignment;
					text.fontStyle = (config.RemainingNotesLeft.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.RemainingNotesLeft.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = config.NotesMissed - missedNotes >= 0 ? $"{config.NotesMissed - missedNotes} note{(config.NotesMissed - missedNotes == 1 ? string.Empty : "s")} can be missed" : $"Too many notes missed";
					text.color = config.RemainingNotesLeft.Color.Color;
				} else {
					remainingNotesLeftLabel.GetComponent<Text>().enabled = false;
				}

				if ((config.RestartIndicator.Visible && failedObjective) || config.LayoutTest) {
					restartIndicatorLabel.transform.localPosition = new Vector3(config.RestartIndicator.X - Screen.width / 2, Screen.height / 2 - config.RestartIndicator.Y);
					var text = restartIndicatorLabel.GetComponent<Text>();
					text.enabled = true;
					text.fontSize = config.RestartIndicator.Size;
					text.alignment = config.RestartIndicator.Alignment;
					text.fontStyle = (config.RestartIndicator.Bold ? FontStyle.Bold : FontStyle.Normal) | (config.RestartIndicator.Italic ? FontStyle.Italic : FontStyle.Normal);
					text.text = $"{target} failed, restarting in {(int)remainingTimeBeforeRestart + 1}";
					text.color = new Color(config.RestartIndicator.Color.Color.r, config.RestartIndicator.Color.Color.g, config.RestartIndicator.Color.Color.b, config.RestartIndicator.Color.Color.a * Math.Min((config.FailDelay - remainingTimeBeforeRestart) * 2.0f, 1.0f));
				} else {
					restartIndicatorLabel.GetComponent<Text>().enabled = false;
				}
			} else if (sceneName == "Gameplay") {
				displayImageLabel.GetComponent<Text>().enabled = false;
				remainingNotesLeftLabel.GetComponent<Text>().enabled = false;
				restartIndicatorLabel.GetComponent<Text>().enabled = false;
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
				//TODO: Look into why the GUILayout just panics if this and Extra Song UI are both loaded.
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(187001001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Perfect Mode Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (!config.SeenChangelog && config.TweakVersion != versionCheck.AssemblyVersion) {
				changelogRect = GUILayout.Window(187001998, changelogRect, OnChangelogWindow, new GUIContent($"Perfect Mode Changelog"), settingsWindowStyle);
			}
		}

		#endregion

		internal void MissNote(NoteWrapper note) {
			++missedNotes;
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
			config.ConfigureGUI(new GUIConfigurationStyles {
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

			GUILayout.Label($"Perfect Mode v{versionCheck.AssemblyVersion}");
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
			GUILayout.Label("Thankyou for downloading Perfect Mode!", largeLabelStyle);
			GUILayout.Label("To get started, press F6 to enable/disable Perfect Mode.", smallLabelStyle);
			GUILayout.Label("Press Ctrl + Shift + F6 to enable/disable the config window.", smallLabelStyle);
			GUILayout.Label("The config window lets you change things such as the keys to enable/disable the mode, how many notes you can miss, and the layout of the UI.", smallLabelStyle);
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