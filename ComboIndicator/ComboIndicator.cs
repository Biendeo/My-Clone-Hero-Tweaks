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

		private SoloCounterWrapper soloCounter;
		private ScoreManagerWrapper scoreManager;

		private Font uiFont;

		private int lastCombo;

		public ComboIndicator() {

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
					var gameManager = GameObject.Find("Game Manager")?.GetComponent<GameManager>();
					if (gameManager != null) {
						soloCounter = new GameManagerWrapper(gameManager).BasePlayers[0].SoloCounter;
						scoreManager = new GameManagerWrapper(gameManager).ScoreManager;
					}
					lastCombo = 0;
				}
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && scoreManager != null) {
				int currentCombo = scoreManager.OverallCombo;
				if (currentCombo > 0 && currentCombo != lastCombo && (currentCombo == 50 || currentCombo % 100 /*100*/ == 0)) {
					var textElement = new GameObject(string.Empty, new Type[] {
						typeof(DancingText)
					});
					textElement.GetComponent<DancingText>().Text = $"{currentCombo} Note Streak!";
					textElement.GetComponent<DancingText>().Font = uiFont;
					textElement.GetComponent<DancingText>().RaisedForSolo = soloCounter.Bool2;
				}
				lastCombo = currentCombo;
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
		}

		#endregion
	}
}