using AccuracyIndicator.Components;
using AccuracyIndicator.Settings;
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

namespace AccuracyIndicator {
	public class AccuracyIndicator : MonoBehaviour {
		private bool sceneChanged;

		private GameManagerWrapper gameManager;
		private BasePlayerWrapper[] basePlayers;
		private List<NoteWrapper> notes;

		private Font uiFont;

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
		private int currentNoteIndex;

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

		public AccuracyIndicator() {
			lastSongTime = -5.0;
		}

		#region Unity Methods

		void Start() {
			config = Config.LoadConfig();
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __) {
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					basePlayers = gameManager.BasePlayers;
					notes = basePlayers[0].Notes;
					currentNoteIndex = 0;
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
				} else if (SceneManager.GetActiveScene().name.Equals("EndOfSong") && config.Enabled) {
					Transform canvasTransform = FadeBehaviourWrapper.instance.fadeGraphic.canvas.transform;
					foreach (var x in Enumerable.Range(0, 8)) {
						var gameObjects = new GameObject[3];
						var textComponents = new Text[3];
						foreach (var y in Enumerable.Range(0, 3)) {
							gameObjects[y] = new GameObject($"My element {x}-{y}", new Type[] {
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
							textComponents[y].font = uiFont;
							textComponents[y].fontSize = Screen.height * 30 / 1440;
							textComponents[y].alignment = TextAnchor.MiddleRight;
							textComponents[y].fontStyle = FontStyle.Bold;
							textComponents[y].horizontalOverflow = HorizontalWrapMode.Overflow;
							textComponents[y].verticalOverflow = VerticalWrapMode.Overflow;
						}
						if (x == 0) {
							textComponents[0].text = $"Very early ({(highestVeryEarly * 1000.0f).ToString("0.00")}ms):";
							int veryEarlies = noteHits.Count(nh => nh < highestVeryEarly);
							textComponents[1].text = veryEarlies.ToString();
							textComponents[2].text = $"({(veryEarlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorVeryEarly.Color;
						} else if (x == 1) {
							textComponents[0].text = $"Early ({(highestEarly * 1000.0f).ToString("0.00")}ms):";
							int earlies = noteHits.Count(nh => nh < highestEarly && nh >= highestVeryEarly);
							textComponents[1].text = earlies.ToString();
							textComponents[2].text = $"({(earlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorEarly.Color;
						} else if (x == 2) {
							textComponents[0].text = $"Slightly early ({(highestSlightlyEarly * 1000.0f).ToString("0.00")}ms):";
							int slightlyEarlies = noteHits.Count(nh => nh < highestSlightlyEarly && nh >= highestEarly);
							textComponents[1].text = slightlyEarlies.ToString();
							textComponents[2].text = $"({(slightlyEarlies * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorSlightlyEarly.Color;
						} else if (x == 3) {
							textComponents[0].text = $"Perfect:";
							int perfects = noteHits.Count(nh => nh <= highestSlightlyLate && nh >= highestSlightlyEarly);
							textComponents[1].text = perfects.ToString();
							textComponents[2].text = $"({(perfects * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorPerfect.Color;
						} else if (x == 4) {
							textComponents[0].text = $"Slightly late ({(highestSlightlyLate * 1000.0f).ToString("0.00")}ms):";
							int slightlyLates = noteHits.Count(nh => nh <= highestLate && nh > highestSlightlyLate);
							textComponents[1].text = slightlyLates.ToString();
							textComponents[2].text = $"({(slightlyLates * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorSlightlyLate.Color;
						} else if (x == 5) {
							textComponents[0].text = $"Late ({(highestLate * 1000.0f).ToString("0.00")}ms):";
							int lates = noteHits.Count(nh => nh <= highestVeryLate && nh > highestLate);
							textComponents[1].text = lates.ToString();
							textComponents[2].text = $"({(lates * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorLate.Color;
						} else if (x == 6) {
							textComponents[0].text = $"Very late ({(highestVeryLate * 1000.0f).ToString("0.00")}ms):";
							int veryLates = noteHits.Count(nh => nh > highestVeryLate);
							textComponents[1].text = veryLates.ToString();
							textComponents[2].text = $"({(veryLates * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorVeryLate.Color;
						} else if (x == 7) {
							textComponents[0].text = $"Missed:";
							int misses = totalNoteCount - noteHits.Count();
							textComponents[1].text = misses.ToString();
							textComponents[2].text = $"({(misses * 100.0 / totalNoteCount).ToString("0.00")}%)";
							textComponents[0].color = config.ColorMissed.Color;
						}
						float height = Screen.height * 50.0f / 1440.0f - (x * 37.0f);
						gameObjects[0].transform.localPosition = new Vector3(Screen.width * 200.0f / 1440.0f - Screen.width / 2, height, 50);
						gameObjects[1].transform.localPosition = new Vector3(Screen.width * 250.0f / 1440.0f - Screen.width / 2, height, 50);
						gameObjects[2].transform.localPosition = new Vector3(Screen.width * 350.0f / 1440.0f - Screen.width / 2, height, 50);
					}
				}
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
				//! In practice mode, the song time is set to 1.5s before the section or A/B. If it is looping, it is
				//! initially set to 0, then to the appropriate time. As long as the user isn't on less than 10FPS, this should work.
				if (Math.Abs(gameManager.SongTime - lastSongTime) > 0.1) {
					notes = basePlayers[0].Notes;
					currentNoteIndex = 0;
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
				highestVeryLate = Math.Max(highestVeryLate, config.CutoffVeryLate);
				highestLate = Math.Max(highestLate, config.CutoffLate);
				highestSlightlyLate = Math.Max(highestSlightlyLate, config.CutoffSlightlyLate);
				highestSlightlyEarly = Math.Min(highestSlightlyEarly, -config.CutoffSlightlyEarly);
				highestEarly = Math.Min(highestEarly, -config.CutoffEarly);
				highestVeryEarly = Math.Min(highestVeryEarly, -config.CutoffVeryEarly);
				if (notes != null) {
					while (currentNoteIndex < totalNoteCount && (notes[currentNoteIndex].WasHit || notes[currentNoteIndex].WasMissed)) {
						lastNoteActualTime = notes[currentNoteIndex].Time;
						lastNoteHitTime = gameManager.SongTime;
						if (notes[currentNoteIndex].WasHit) {
							if (lastNoteHitTime > lastNoteActualTime && (lastNoteHitTime - Time.deltaTime) < lastNoteActualTime) {
								lastNoteHitDifference = 0.0f;
							} else if (lastNoteHitTime > lastNoteActualTime) {
								lastNoteHitDifference = (float)(lastNoteHitTime - Time.deltaTime - lastNoteActualTime);
							} else if (lastNoteHitTime < lastNoteActualTime) {
								lastNoteHitDifference = (float)(lastNoteHitTime - lastNoteActualTime);
							} else {
								Debug.LogError($"Panic?");
							}
							noteHits.Add(lastNoteHitDifference);
							if (hitNotes == 0) {
								hitAccuracy = lastNoteHitDifference;
							} else {
								hitAccuracy = (hitAccuracy * hitNotes + lastNoteHitDifference) / (hitNotes + 1);
							}
							++hitNotes;
						} else if (notes[currentNoteIndex].WasMissed) {
							lastNoteHitDifference = 0.07f;
						}
						++currentNoteIndex;
					}
				}
			}
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//TODO: Get the font directly from the bundle?
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			config.HandleInput();
			if (gameManager != null) {
				lastSongTime = gameManager.SongTime;
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
			if (config.ConfigWindowEnabled) {
				config.DrawLabelWindows();
				var outputRect = GUILayout.Window(5318010, new Rect(config.ConfigX, config.ConfigY, 320.0f, 500.0f), OnWindow, new GUIContent("Accuracy Indicator Settings"), settingsWindowStyle);
				config.ConfigX = outputRect.x;
				config.ConfigY = outputRect.y;
			}
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && notes != null) {
				if (config.Enabled) {
					Color labelColor = Color.white;
					string message = string.Empty;
					if (lastNoteHitDifference < -config.CutoffVeryEarly) {
						labelColor = config.ColorVeryEarly.Color;
						message = "Very Early";
					} else if (lastNoteHitDifference < -config.CutoffEarly) {
						labelColor = config.ColorEarly.Color;
						message = "Early";
					} else if (lastNoteHitTime < -config.CutoffSlightlyEarly) {
						labelColor = config.ColorSlightlyEarly.Color;
						message = "Slightly Early!";
					} else if (lastNoteHitDifference == 0.07f) {
						labelColor = config.ColorMissed.Color;
						message = "Missed";
					} else if (lastNoteHitDifference > config.CutoffVeryLate) {
						labelColor = config.ColorVeryLate.Color;
						message = "Very Late";
					} else if (lastNoteHitDifference > config.CutoffLate) {
						labelColor = config.ColorLate.Color;
						message = "Late";
					} else if (lastNoteHitDifference > config.CutoffSlightlyLate) {
						labelColor = config.ColorSlightlyLate.Color;
						message = "Slightly Late!";
					} else {
						labelColor = config.ColorPerfect.Color;
						message = "Perfect!";
					}
					if (config.AccuracyTime.Visible) GUI.Label(config.AccuracyTime.Rect, new GUIContent($"{(lastNoteHitDifference * 1000.0f).ToString("0.00")}ms"), new GUIStyle(config.AccuracyTime.Style) {
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						},
						font = uiFont
					});
					if (config.AccuracyMessage.Visible) GUI.Label(config.AccuracyMessage.Rect, new GUIContent(message), new GUIStyle(config.AccuracyMessage.Style) {
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						},
						font = uiFont
					});
					if (config.AverageAccuracy.Visible) GUI.Label(config.AverageAccuracy.Rect, new GUIContent($"{(hitAccuracy * 1000.0).ToString("0.00")}ms"), new GUIStyle(config.AverageAccuracy.Style) {
						normal = new GUIStyleState {
							textColor = new Color(labelColor.r, labelColor.g, labelColor.b, labelColor.a * (config.LayoutTest ? 1.0f : Math.Max(0.0f, Math.Min(1.0f, (float)(config.TimeOnScreen + lastNoteHitTime - gameManager.SongTime)) * 2.0f)))
						},
						font = uiFont
					});
				}
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

			GUILayout.Label($"Accuracy Indicator v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			GUILayout.Label("Tweak by Biendeo");
			GUILayout.Label("Thankyou for using this!");
			GUILayout.EndScrollView();
			GUI.DragWindow();
		}
	}
}