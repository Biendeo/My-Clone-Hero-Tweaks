using Common;
using Common.Wrappers;
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

namespace ComboIndicator {
	public class ComboIndicator : MonoBehaviour {
		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private SoloCounterWrapper soloCounter;
		private ScoreManagerWrapper scoreManager;

		private Font uiFont;

		private int lastCombo;

		private GUIStyle settingsWindowStyle;
		private GUIStyle settingsToggleStyle;
		private GUIStyle settingsButtonStyle;
		private GUIStyle settingsTextAreaStyle;
		private GUIStyle settingsTextFieldStyle;
		private GUIStyle settingsLabelStyle;
		private GUIStyle settingsBoxStyle;
		private GUIStyle settingsHorizontalSliderStyle;
		private GUIStyle settingsHorizontalSliderThumbStyle;

		private readonly VersionCheck VersionCheck;

		public ComboIndicator() {
			VersionCheck = new VersionCheck(187003999);
		}

		#region Unity Methods

		void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					gameManager = new GameManagerWrapper(GameObject.Find("Game Manager")?.GetComponent<GameManager>());
					if (gameManager != null) {

						soloCounter = gameManager.BasePlayers[0].SoloCounter;
						scoreManager = gameManager.ScoreManager;
					}
					lastCombo = 0;
				}
			}
			if (SceneManager.GetActiveScene().name == "Main Menu" && !VersionCheck.HasVersionBeenChecked) {
				string detectedVersion = GlobalVariablesWrapper.instance.buildVersion;
				VersionCheck.CheckVersion(detectedVersion);
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && scoreManager != null) {
				int currentCombo = scoreManager.OverallCombo;
				if (currentCombo > 0 && currentCombo != lastCombo && (currentCombo == 50 || currentCombo % 100 /*100*/ == 0)) {
					var textElement = new GameObject(string.Empty, new Type[] {
						typeof(DancingText)
					});
					textElement.GetComponent<DancingText>().GameManager = gameManager;
					textElement.GetComponent<DancingText>().Text = $"{currentCombo} Note Streak!";
					textElement.GetComponent<DancingText>().Font = uiFont;
					textElement.GetComponent<DancingText>().RaisedForSolo = true; // Could be soloCounter.Bool2 but I want to gauge how people respond first.
				}
				lastCombo = currentCombo;
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
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
			if (VersionCheck.IsShowingUpdateWindow) {
				VersionCheck.DrawUpdateWindow(settingsWindowStyle, settingsLabelStyle, settingsButtonStyle);
			}
		}

		#endregion
	}
}