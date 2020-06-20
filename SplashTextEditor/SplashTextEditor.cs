using Common;
using Common.Wrappers;
using SplashTextEditor.Settings;
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

namespace SplashTextEditor {
	public class SplashTextEditor : MonoBehaviour {
		private bool sceneChanged;

		private Config config;

		private TextMeshProUGUI splashTextComponent;
		private string currentSplashMessage;
		private System.Random randomGenerator;

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

		private readonly VersionCheck VersionCheck;
		private Rect ChangelogRect;

		public SplashTextEditor() {
			VersionCheck = new VersionCheck(187004999);
			ChangelogRect = new Rect(500.0f, 500.0f, 100.0f, 100.0f);
			randomGenerator = new System.Random();
			splashTextComponent = null;
			currentSplashMessage = string.Empty;
		}

		#region Unity Methods

		void Start() {
			config = Config.LoadConfig();
			config.ChangeActiveTextFunction = (string s) => currentSplashMessage = s;
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				splashTextComponent = null;
				currentSplashMessage = string.Empty;
				this.sceneChanged = false;
			}
			if (splashTextComponent == null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				var splashTextObject = GameObject.Find("Tag");
				if (splashTextObject != null) {
					splashTextComponent = splashTextObject.GetComponent<TextMeshProUGUI>();
					if (config.Messages.Count > 0) {
						currentSplashMessage = config.Messages[randomGenerator.Next(0, config.Messages.Count)];
					}
				}
			}
			if (config.Enabled && splashTextComponent != null && currentSplashMessage != string.Empty) {
				splashTextComponent.text = currentSplashMessage;
			}
			if (SceneManager.GetActiveScene().name == "Main Menu" && !VersionCheck.HasVersionBeenChecked) {
				if (config.SilenceUpdates) {
					VersionCheck.HasVersionBeenChecked = true;
				} else {
					string detectedVersion = GlobalVariablesWrapper.instance.buildVersion;
					VersionCheck.CheckVersion(detectedVersion);
				}
			}
			config.HandleInput();
		}

		void OnGUI() {
			if (settingsWindowStyle is null) {
				settingsWindowStyle = new GUIStyle(GUI.skin.window);
				settingsWindowStyle.
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
				var outputRect = GUILayout.Window(187004001, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Splash Text Editor Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (VersionCheck.IsShowingUpdateWindow) {
				VersionCheck.DrawUpdateWindow(settingsWindowStyle, settingsLabelStyle, settingsButtonStyle);
			}
			if (config.TweakVersion != FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion && !config.SeenChangelog) {
				ChangelogRect = GUILayout.Window(187004998, ChangelogRect, OnChangelogWindow, new GUIContent($"Splash Text Editor Changelog"), settingsWindowStyle);
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
			config.ConfigureGUI(new Common.Settings.GUIConfigurationStyles {
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

			GUILayout.Label($"Splash Text Editor v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
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
			GUILayout.Label("Thankyou for downloading Splash Text Editor!", largeLabelStyle);
			GUILayout.Label("Simply press Ctrl + Shift + F9 to bring up the config menu. From there you can add new splash messages that will randomly be shown on the title screen.", smallLabelStyle);
			GUILayout.Label("In the config menu, unticking \"Enable\" will result in the original splash text appearing instead of your own. If your messages aren't showing, make sure you've got that ticked!", smallLabelStyle);
			GUILayout.Label("To make a splash message, just click insert in the config menu. You can then edit the text of that message. You can add more splash messages by clicking insert more times (it'll add a new message directly underneath your last message). You can also click delete to remove that message, and shift up and shift down to organise the messages (note that the order does not affect how frequently the messages will appear).", smallLabelStyle);
			GUILayout.Label("If you are on the main menu, you can also click \"Preview\" which immediately replaces the current splash text with that item. Do note the text doesn't update as you type, so you'll need to click \"Preview\" again to update it. However, when you return to the title screen, the changed splash text can be chosen.", smallLabelStyle);
			GUILayout.Label("Make sure you press \"Save\" at the bottom of the config menu to ensure your messages persist when you reopen Clone Hero!", smallLabelStyle);
			GUILayout.Label("Please refer to the README.md on the Github for more details or to submit bugs/new features.", smallLabelStyle);

			GUILayout.Space(15.0f);

			if (GUILayout.Button("Close this window", settingsButtonStyle)) {
				config.SeenChangelog = true;
				config.TweakVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
				config.SaveConfig();
			}
			GUI.DragWindow();
		}
	}
}