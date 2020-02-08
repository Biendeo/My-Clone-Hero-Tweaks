using ExtraSongUI.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExtraSongUI {
	public class SongUI : MonoBehaviour {
		private bool sceneChanged;

		// Song length
		private GameManagerWrapper gameManager;

		// Star progress
		private StarProgressWrapper starProgress;

		// Note count
		private BasePlayerWrapper[] basePlayers;
		private int totalNoteCount;
		private int totalStarPowers;
		private List<NoteWrapper> noteSet;

		private Font uiFont;

		#region Unity Methods

		void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __)
			{
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (uiFont is null && SceneManager.GetActiveScene().name.Equals("Main Menu")) {
				//var queue = new Queue<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
				//var objects = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
				//while (queue.Count > 0) {
				//	var o = queue.Dequeue();
				//	for (int i = 0; i < o.transform.childCount; ++i) {
				//		queue.Enqueue(o.transform.GetChild(i).gameObject);
				//		objects.Add(o.transform.GetChild(i).gameObject);
				//	}
				//}
				//foreach (var o in objects.Where(o => o.GetComponent<Text>() != null)) {
				//	Debug.LogError($"Object name: {o.name}, text: {o.GetComponent<Text>().text}");
				//}
				uiFont = GameObject.Find("Profile Title").GetComponent<Text>().font;
			}
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					// Song length
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					starProgress = gameManager.StarProgress;
					basePlayers = gameManager.BasePlayers;
					//? The player difficulty seems to go easy to expert rather than expert to easy, so this 3 - inverts that for this usage only. That should be looked into.
					var chart = gameManager.Song.GetChart(gameManager.BasePlayers[0].Player.PlayerProfile.Instrument, (sbyte)(3 - gameManager.BasePlayers[0].Player.PlayerProfile.Difficulty));
					totalNoteCount = chart.UnknownInt1;
					totalStarPowers = chart.StarPower.Length;
					noteSet = new List<NoteWrapper>();
				}
			}
		}


		void OnGUI() {
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null) {
				var style = new GUIStyle {
					font = uiFont,
					fontStyle = FontStyle.Bold,
					fontSize = 50,
					alignment = TextAnchor.MiddleRight,
					normal = new GUIStyleState {
						textColor = Color.white,
					}
				};

				// Song length
				// TODO: The times are a bit incorrect for practice mode (they're offset by the timestamp at the beginning of the practise section).
				GUI.Label(new Rect(800f, 800f, 0.1f, 0.1f), new GUIContent($"{DoubleToTimeString(gameManager.SongTime)} / {DoubleToTimeString(gameManager.SongLength)}"), style);

				// Star progress
				int currentScore = starProgress.LastScore;
				int previousStarScore = starProgress.CurrentStar == 0 ? 0 : starProgress.StarScores[starProgress.CurrentStar - 1];
				int nextStarScore = starProgress.StarScores[starProgress.CurrentStar];
				double nextStarPercentage = starProgress.CurrentStar < 7 ? (currentScore - previousStarScore) * 100.0 / (nextStarScore - previousStarScore) : 100.0;
				int sevenStarScore = starProgress.StarScores[6];
				double sevenStarPercentage = Math.Min(100.0, currentScore * 100.0 / sevenStarScore);
				GUI.Label(new Rect(160f, 860f, 0.1f, 0.1f), new GUIContent($"{starProgress.CurrentStar} → {Math.Min(7, starProgress.CurrentStar + 1)}:"), style);
				GUI.Label(new Rect(800f, 860f, 0.1f, 0.1f), new GUIContent($"{currentScore - previousStarScore} / {nextStarScore - previousStarScore} ({nextStarPercentage.ToString("0.00")}%)"), style);
				GUI.Label(new Rect(160f, 920f, 0.1f, 0.1f), new GUIContent($"0 → 7:"), style);
				GUI.Label(new Rect(800f, 920f, 0.1f, 0.1f), new GUIContent($"{currentScore} / {sevenStarScore} ({sevenStarPercentage.ToString("0.00")}%)"), style);

				// Note count
				var newNotes = gameManager.BasePlayers[0].HittableNotes.ToList();
				foreach (var nonNull in newNotes.Where(n => !(n.note is null))) {
					if (!noteSet.Exists(n => n.note == nonNull.note)) {
						noteSet.Add(nonNull);
					}
				}
				int hitNotes = noteSet.Count(n => n.WasHit);
				int missedNotes = noteSet.Count(n => n.WasMissed);
				int seenNotes = hitNotes + missedNotes;
				GUI.Label(new Rect(160f, 980f, 0.1f, 0.1f), new GUIContent($"Notes:"), style);
				GUI.Label(new Rect(800f, 980f, 0.1f, 0.1f), new GUIContent($"{hitNotes} / {seenNotes} / {totalNoteCount} ({(hitNotes * 100.0 / totalNoteCount).ToString("0.00")}%{(seenNotes == hitNotes ? (!gameManager.BasePlayers[0].FirstNoteMissed ? ", FC": string.Empty) : $", -{missedNotes}")})"), style);
				GUI.Label(new Rect(160f, 1040f, 0.1f, 0.1f), new GUIContent($"SP:"), style);
				GUI.Label(new Rect(800f, 1040f, 0.1f, 0.1f), new GUIContent($"{basePlayers[0].StarPowersHit} / {totalStarPowers} ({(basePlayers[0].StarPowersHit * 100.0 / totalStarPowers).ToString("0.00")}%)"), style);
			}
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