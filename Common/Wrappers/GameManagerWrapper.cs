using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	internal class GameManagerWrapper : WrapperBase {
		public readonly GameManager gameManager;

		public BasePlayerWrapper[] BasePlayers => ((BasePlayer[])basePlayersField.GetValue(gameManager)).Select(bp => new BasePlayerWrapper(bp)).ToArray();
		private static FieldInfo basePlayersField;
		private const string basePlayersFieldName = "\u0316\u0319\u0314\u0316\u0315\u0313\u0311\u0315\u031C\u0312\u0315";

		public BasePlayerWrapper UnknownBasePlayer => new BasePlayerWrapper((BasePlayer)unknownBasePlayerField.GetValue(gameManager)); //? It's null for me. 🤷‍
		private static FieldInfo unknownBasePlayerField;
		private const string unknownBasePlayerFieldName = "\u0316\u031C\u0312\u0312\u031C\u0315\u0314\u0310\u031A\u0314\u0317";

		public double SongLength => (double)songLengthField.GetValue(gameManager);
		private static FieldInfo songLengthField;
		private const string songLengthFieldName = "\u031C\u0312\u0314\u0318\u0312\u030F\u0312\u031C\u0313\u030E\u0317";

		public double SongTime => (double)songTimeField.GetValue(gameManager);
		private static FieldInfo songTimeField;
		private const string songTimeFieldName = "\u031C\u030D\u030D\u0317\u0317\u0312\u031B\u031C\u0318\u0312\u030E";

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(gameManager));
		private static FieldInfo starProgressField;
		private const string starProgressFieldName = "starProgress";

		public PracticeUIWrapper PracticeUI => new PracticeUIWrapper((PracticeUI)practiceUIField.GetValue(gameManager));
		private static FieldInfo practiceUIField;
		private const string practiceUIFieldName = "practiceUI";

		public SongWrapper Song => new SongWrapper(songField.GetValue(gameManager));
		private static FieldInfo songField;
		private const string songFieldName = "\u031A\u0311\u031B\u0317\u0319\u031B\u0316\u030E\u0312\u030F\u031B";

		public GameObject PauseMenu => (GameObject)pauseMenuField.GetValue(gameManager);
		private static FieldInfo pauseMenuField;
		private const string pauseMenuFieldName = "pauseMenu";

		public ScoreManagerWrapper ScoreManager => new ScoreManagerWrapper((ScoreManager)scoreManagerField.GetValue(gameManager));
		private static FieldInfo scoreManagerField;
		private const string scoreManagerFieldName = "\u0316\u031C\u031C\u0318\u0311\u0317\u0317\u030F\u0319\u0312\u030F";

		/// <summary>
		/// Seems to create a brand new list of notes based on the chart. It probably shouldn't be called mid-game
		/// because performance is iffy and it has side effects.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="recomputeStars"></param>
		/// <returns></returns>
		public List<NoteWrapper> GetNotesFromChart(CHPlayerWrapper player, bool recomputeStars) {
			var notes = (ICollection)getNotesFromChartMethod.Invoke(gameManager, new object[] { player.chPlayer, recomputeStars });
			return notes.Cast<object>().Select(o => new NoteWrapper(o)).ToList();
		}
		private static MethodInfo getNotesFromChartMethod;
		private const string getNotesFromChartMethodName = "\u0318\u030D\u031A\u031C\u031B\u0310\u031A\u030F\u030D\u0314\u031B";

		public GameManagerWrapper(GameManager gameManager) {
			this.gameManager = gameManager;
		}

		public static void InitializeSingletonFields() {
			RegisterField(ref basePlayersField, typeof(GameManager), basePlayersFieldName);
			RegisterField(ref unknownBasePlayerField, typeof(GameManager), unknownBasePlayerFieldName);
			RegisterField(ref songLengthField, typeof(GameManager), songLengthFieldName);
			RegisterField(ref songTimeField, typeof(GameManager), songTimeFieldName);
			RegisterField(ref starProgressField, typeof(GameManager), starProgressFieldName);
			RegisterField(ref practiceUIField, typeof(GameManager), practiceUIFieldName);
			RegisterField(ref songField, typeof(GameManager), songFieldName);
			RegisterField(ref pauseMenuField, typeof(GameManager), pauseMenuFieldName);
			RegisterField(ref scoreManagerField, typeof(GameManager), scoreManagerFieldName);
			RegisterMethod(ref getNotesFromChartMethod, typeof(GameManager), getNotesFromChartMethodName);
		}
	}
}
