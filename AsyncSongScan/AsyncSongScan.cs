using Common.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GUI;

namespace AsyncSongScan {
	public class AsyncSongScan : MonoBehaviour {
		private bool sceneChanged;

		private Font uiFont;

		private GUIStyle settingsWindowStyle;
		private GUIStyle settingsToggleStyle;
		private GUIStyle settingsButtonStyle;
		private GUIStyle settingsTextAreaStyle;
		private GUIStyle settingsTextFieldStyle;
		private GUIStyle settingsLabelStyle;
		private GUIStyle settingsBoxStyle;
		private GUIStyle settingsHorizontalSliderStyle;
		private GUIStyle settingsHorizontalSliderThumbStyle;

		private bool scanning;
		private bool configWindowEnabled;
		private Rect configWindowRect;

		private string mainText;
		private string folderText;
		private string countText;
		private string errorText;
		private string badSongsText;

		public AsyncSongScan() {
			scanning = false;
			configWindowEnabled = false;
			configWindowRect = new Rect(100.0f, 100.0f, 250.0f, 250.0f);
		}

		#region Unity Methods

		private void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		private void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			if (Input.GetKeyDown(KeyCode.F8) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {
				configWindowEnabled = !configWindowEnabled;
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
				configWindowRect = GUILayout.Window(187002001, configWindowRect, OnWindow, new GUIContent("Async Song Scan"), settingsWindowStyle);
			}
		}

		#endregion

		private IEnumerator ScanSongs() {
			scanning = true;
			var cache = new CacheWrapper();
			var thread = new Thread(cache.ScanSongsFull);
			thread.Start();
			while (thread.IsAlive) {
				mainText = cache.cacheState.ToString();
				folderText = $"{cache.Int4 - cache.Int1} Folders";
				countText = $"{SongDirectoryWrapper.setlistSongEntries.Count} Songs Scanned";
				errorText = $"{cache.exceptions.Count} Errors";
				badSongsText = $"{cache.Int3} Bad Songs";
				yield return null;
			}
			SongDirectoryWrapper.sortCounter = -1;
			SongDirectoryWrapper.Sort(null, false);
			scanning = false;
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
			if (!scanning) {
				scanning = GUILayout.Button("Scan for songs", settingsButtonStyle);
				if (scanning) {
					StartCoroutine(ScanSongs());
				}
			} else {
				GUILayout.Label(mainText, largeLabelStyle);
				GUILayout.Label(folderText, smallLabelStyle);
				GUILayout.Label(countText, smallLabelStyle);
				GUILayout.Label(errorText, smallLabelStyle);
				GUILayout.Label(badSongsText, smallLabelStyle);
			}
			GUILayout.Space(25.0f);

			GUILayout.Label($"Async Song Scan v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUI.DragWindow();
		}
	}
}