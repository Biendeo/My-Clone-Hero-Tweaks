using ExtraSongUI.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExtraSongUI {
	public class SongUI : MonoBehaviour {
		private bool sceneChanged;

		// Song length
		private GameManagerWrapper gameManager;

		// Star progress
		private StarProgressWrapper starProgress;

		// Base player
		private BasePlayerWrapper[] basePlayers;
		private List<NoteWrapper> notes;

		#region Unity Methods

		void Start() {
			SceneManager.activeSceneChanged += delegate (Scene _, Scene __)
			{
				sceneChanged = true;
			};
		}

		void LateUpdate() {
			if (this.sceneChanged) {
				this.sceneChanged = false;
				if (SceneManager.GetActiveScene().name.Equals("Gameplay")) {
					// Song length
					var gameManagerObject = GameObject.Find("Game Manager");
					gameManager = new GameManagerWrapper(gameManagerObject.GetComponent<GameManager>());
					starProgress = gameManager.StarProgress;
					basePlayers = gameManager.BasePlayers;
					notes = gameManager.GetNotesFromChart(gameManager.BasePlayers[0].Player, false);
				}
			}
		}


		void OnGUI() {
			if (SceneManager.GetActiveScene().name.Equals("Gameplay") && gameManager != null) {
				var style = new GUIStyle {
					fontSize = 50,
					alignment = TextAnchor.MiddleRight,
					normal = new GUIStyleState {
						textColor = Color.white
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
				//GUI.Label(new Rect(800f, 1040f, 0.1f, 0.1f), new GUIContent($"{gameManager.StarProgress.BaseScore}"), style);

				// BasePlayer?
				//int hitNotes = notes.Count(n => n.WasHit);
				int hitNotes = basePlayers[0].HitNotes;
				//int passedNotes = notes.Count(n => n.WasHit || n.WasMissed);
				int passedNotes = notes.Count(n => n.Time < gameManager.SongTime); // Maybe more accurate.
				int notes1 = notes.Count(n => n.WasHit);
				int notes2 = notes.Count(n => n.WasMissed);
				int notes3 = notes.Count(n => n.ThirdBool);
				GUI.Label(new Rect(800f, 980f, 0.1f, 0.1f), new GUIContent($"{hitNotes} / {passedNotes} / {notes.Count} ({(hitNotes * 100.0 / notes.Count).ToString("0.00")}%, -{passedNotes - hitNotes})"), style);
				//GUI.Label(new Rect(800f, 1040f, 0.1f, 0.1f), new GUIContent($"{basePlayers[0].StarPowersHit} / 9999 ({(basePlayers[0].StarPowersHit * 100.0 / 9999).ToString("0.00")}%)"), style);

				GUI.Label(new Rect(800f, 1100f, 0.1f, 0.1f), new GUIContent($"{notes.Count} ({notes1} / {notes2} / {notes3} / {passedNotes})"), style);
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