using Common.Wrappers.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Wrappers {
	[Wrapper(typeof(GameManager))]
	internal class GameManagerWrapper {
		public readonly GameManager gameManager;

		public BasePlayerWrapper[] BasePlayers => ((BasePlayer[])basePlayersField.GetValue(gameManager)).Select(bp => new BasePlayerWrapper(bp)).ToArray();
		[WrapperField("\u0316\u0319\u0314\u0316\u0315\u0313\u0311\u0315\u031C\u0312\u0315")]
		private static readonly FieldInfo basePlayersField;

		public BasePlayerWrapper UnknownBasePlayer => new BasePlayerWrapper((BasePlayer)unknownBasePlayerField.GetValue(gameManager)); //? It's null for me. 🤷‍
		[WrapperField("\u0316\u031C\u0312\u0312\u031C\u0315\u0314\u0310\u031A\u0314\u0317")]
		private static readonly FieldInfo unknownBasePlayerField;

		public double SongLength => (double)songLengthField.GetValue(gameManager);
		[WrapperField("\u031C\u0312\u0314\u0318\u0312\u030F\u0312\u031C\u0313\u030E\u0317")]
		private static readonly FieldInfo songLengthField;

		public double SongTime => (double)songTimeField.GetValue(gameManager);
		[WrapperField("\u031C\u030D\u030D\u0317\u0317\u0312\u031B\u031C\u0318\u0312\u030E")]
		private static readonly FieldInfo songTimeField;

		public bool IsPaused => (bool)isPausedField.GetValue(gameManager);
		[WrapperField("\u031B\u0318\u030E\u0310\u0319\u030E\u0312\u031C\u031A\u030E\u0313")]
		private static readonly FieldInfo isPausedField;

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(gameManager));
		[WrapperField("starProgress")]
		private static readonly FieldInfo starProgressField;

		public PracticeUIWrapper PracticeUI => new PracticeUIWrapper((PracticeUI)practiceUIField.GetValue(gameManager));
		[WrapperField("practiceUI")]
		private static readonly FieldInfo practiceUIField;

		public SongWrapper Song => new SongWrapper(songField.GetValue(gameManager));
		[WrapperField("\u031A\u0311\u031B\u0317\u0319\u031B\u0316\u030E\u0312\u030F\u031B")]
		private static readonly FieldInfo songField;

		public GameObject PauseMenu => (GameObject)pauseMenuField.GetValue(gameManager);
		[WrapperField("pauseMenu")]
		private static readonly FieldInfo pauseMenuField;

		public ScoreManagerWrapper ScoreManager => new ScoreManagerWrapper((ScoreManager)scoreManagerField.GetValue(gameManager));
		[WrapperField("\u0316\u031C\u031C\u0318\u0311\u0317\u0317\u030F\u0319\u0312\u030F")]
		private static readonly FieldInfo scoreManagerField;

		public GlobalVariablesWrapper GlobalVariables => new GlobalVariablesWrapper((GlobalVariables)globalVariablesField.GetValue(gameManager));
		[WrapperField("\u030E\u0317\u0317\u030E\u030D\u0315\u0319\u0314\u0317\u030D\u030F")]
		private static readonly FieldInfo globalVariablesField;

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
		[WrapperMethod("\u0318\u030D\u031A\u031C\u031B\u0310\u031A\u030F\u030D\u0314\u031B")]
		private static readonly MethodInfo getNotesFromChartMethod;

		public GameManagerWrapper(GameManager gameManager) {
			this.gameManager = gameManager;
		}
	}
}
