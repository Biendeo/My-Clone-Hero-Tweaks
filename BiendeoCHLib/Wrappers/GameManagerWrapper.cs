using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(GameManager))]
	public struct GameManagerWrapper {
		public GameManager GameManager { get; private set; }

		public static GameManagerWrapper Wrap(GameManager gameManager) => new GameManagerWrapper {
			GameManager = gameManager
		};

		public override bool Equals(object obj) => GameManager.Equals(obj);

		public override int GetHashCode() => GameManager.GetHashCode();

		public bool IsNull() => GameManager == null;

		public BasePlayerWrapper[] BasePlayers => ((BasePlayer[])basePlayersField.GetValue(GameManager)).Select(bp => BasePlayerWrapper.Wrap(bp)).ToArray();
		[WrapperField("\u0316\u0319\u0314\u0316\u0315\u0313\u0311\u0315\u031C\u0312\u0315")]
		private static readonly FieldInfo basePlayersField;

		public BasePlayerWrapper UnknownBasePlayer => BasePlayerWrapper.Wrap((BasePlayer)unknownBasePlayerField.GetValue(GameManager)); //? It's null for me. 🤷‍
		[WrapperField("\u0316\u031C\u0312\u0312\u031C\u0315\u0314\u0310\u031A\u0314\u0317")]
		private static readonly FieldInfo unknownBasePlayerField;

		public double SongLength => (double)songLengthField.GetValue(GameManager);
		[WrapperField("\u031C\u0312\u0314\u0318\u0312\u030F\u0312\u031C\u0313\u030E\u0317")]
		private static readonly FieldInfo songLengthField;

		public double SongTime => (double)songTimeField.GetValue(GameManager);
		[WrapperField("\u031C\u030D\u030D\u0317\u0317\u0312\u031B\u031C\u0318\u0312\u030E")]
		private static readonly FieldInfo songTimeField;

		public bool IsPaused => (bool)isPausedField.GetValue(GameManager);
		[WrapperField("\u031B\u0318\u030E\u0310\u0319\u030E\u0312\u031C\u031A\u030E\u0313")]
		private static readonly FieldInfo isPausedField;

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(GameManager));
		[WrapperField("starProgress")]
		private static readonly FieldInfo starProgressField;

		public PracticeUIWrapper PracticeUI => new PracticeUIWrapper((PracticeUI)practiceUIField.GetValue(GameManager));
		[WrapperField("practiceUI")]
		private static readonly FieldInfo practiceUIField;

		public SongWrapper Song => new SongWrapper(songField.GetValue(GameManager));
		[WrapperField("\u031A\u0311\u031B\u0317\u0319\u031B\u0316\u030E\u0312\u030F\u031B")]
		private static readonly FieldInfo songField;

		public GameObject PauseMenu => (GameObject)pauseMenuField.GetValue(GameManager);
		[WrapperField("pauseMenu")]
		private static readonly FieldInfo pauseMenuField;

		public ScoreManagerWrapper ScoreManager => new ScoreManagerWrapper((ScoreManager)scoreManagerField.GetValue(GameManager));
		[WrapperField("\u0316\u031C\u031C\u0318\u0311\u0317\u0317\u030F\u0319\u0312\u030F")]
		private static readonly FieldInfo scoreManagerField;

		public GlobalVariablesWrapper GlobalVariables => new GlobalVariablesWrapper((GlobalVariables)globalVariablesField.GetValue(GameManager));
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
			var notes = (ICollection)getNotesFromChartMethod(GameManager, player.chPlayer, recomputeStars);
			return notes.Cast<object>().Select(o => NoteWrapper.Wrap(o)).ToList();
		}
		[WrapperMethod("\u0318\u030D\u031A\u031C\u031B\u0310\u031A\u030F\u030D\u0314\u031B")]
		private static readonly FastInvokeHandler getNotesFromChartMethod;

		[Obsolete("Use Wrap()")]
		public GameManagerWrapper(GameManager gameManager) {
			this.GameManager = gameManager;
		}
	}
}
