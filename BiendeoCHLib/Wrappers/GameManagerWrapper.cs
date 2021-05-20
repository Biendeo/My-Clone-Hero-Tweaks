using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using Rewired;
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

		#region Fields

		public GlobalVariablesWrapper GlobalVariables {
			get => GlobalVariablesWrapper.Wrap(globalVariablesField(GameManager));
			set => globalVariablesField(GameManager) = value.GlobalVariables;
		}
		[WrapperField("\u030E\u0317\u0317\u030E\u030D\u0315\u0319\u0314\u0317\u030D\u030F")]
		private static readonly AccessTools.FieldRef<GameManager, GlobalVariables> globalVariablesField;

		public PracticeUIWrapper PracticeUI {
			get => PracticeUIWrapper.Wrap(practiceUIField(GameManager));
			set => practiceUIField(GameManager) = value.PracticeUI;
		}
		[WrapperField("practiceUI")]
		private static readonly AccessTools.FieldRef<GameManager, PracticeUI> practiceUIField;

		public StarProgressWrapper StarProgress {
			get => StarProgressWrapper.Wrap(starProgressField(GameManager));
			set => starProgressField(GameManager) = value.StarProgress;
		}
		[WrapperField("starProgress")]
		private static readonly AccessTools.FieldRef<GameManager, StarProgress> starProgressField;

		public double SongLength {
			get => songLengthField(GameManager);
			set => songLengthField(GameManager) = value;
		}
		[WrapperField("\u031C\u0312\u0314\u0318\u0312\u030F\u0312\u031C\u0313\u030E\u0317")]
		private static readonly AccessTools.FieldRef<GameManager, double> songLengthField;

		public BasePlayerWrapper[] BasePlayers {
			get => basePlayersField(GameManager).Select(o => BasePlayerWrapper.Wrap(o)).ToArray();
			set => basePlayersField(GameManager) = value.Select(o => o.BasePlayer).ToArray();
		}
		[WrapperField("\u0316\u0319\u0314\u0316\u0315\u0313\u0311\u0315\u031C\u0312\u0315")]
		private static readonly AccessTools.FieldRef<GameManager, BasePlayer[]> basePlayersField;

		public double SongTime {
			get => songTimeField(GameManager);
			set => songTimeField(GameManager) = value;
		}
		[WrapperField("\u031C\u030D\u030D\u0317\u0317\u0312\u031B\u031C\u0318\u0312\u030E")]
		private static readonly AccessTools.FieldRef<GameManager, double> songTimeField;

		public bool IsPaused {
			get => isPausedField(GameManager);
			set => isPausedField(GameManager) = value;
		}
		[WrapperField("\u031B\u0318\u030E\u0310\u0319\u030E\u0312\u031C\u031A\u030E\u0313")]
		private static readonly AccessTools.FieldRef<GameManager, bool> isPausedField;

		public SongWrapper Song {
			get => SongWrapper.Wrap(songField(GameManager));
			set => songField(GameManager) = value.Song;
		}
		[WrapperField("\u031A\u0311\u031B\u0317\u0319\u031B\u0316\u030E\u0312\u030F\u031B")]
		private static readonly AccessTools.FieldRef<GameManager, object> songField;

		public GameObject PauseMenu {
			get => pauseMenuField(GameManager);
			set => pauseMenuField(GameManager) = value;
		}
		[WrapperField("pauseMenu")]
		private static readonly AccessTools.FieldRef<GameManager, GameObject> pauseMenuField;

		public ScoreManagerWrapper ScoreManager {
			get => ScoreManagerWrapper.Wrap(scoreManagerField(GameManager));
			set => scoreManagerField(GameManager) = value.ScoreManager;
		}
		[WrapperField("\u0316\u031C\u031C\u0318\u0311\u0317\u0317\u030F\u0319\u0312\u030F")]
		private static readonly AccessTools.FieldRef<GameManager, ScoreManager> scoreManagerField;

		public BasePlayerWrapper UnknownBasePlayer {
			get => BasePlayerWrapper.Wrap(unknownBasePlayerField(GameManager));
			set => unknownBasePlayerField(GameManager) = value.BasePlayer;
		} //? It's null for me. 🤷‍
		[WrapperField("\u0316\u031C\u0312\u0312\u031C\u0315\u0314\u0310\u031A\u0314\u0317")]
		private static readonly AccessTools.FieldRef<GameManager, BasePlayer> unknownBasePlayerField;

		public bool ErrorWhileLoading {
			get => errorWhileLoadingField(GameManager);
			set => errorWhileLoadingField(GameManager) = value;
		}
		[WrapperField("\u0311\u0312\u0310\u031C\u0311\u031A\u031B\u030D\u0313\u030F\u030D")]
		private static readonly AccessTools.FieldRef<GameManager, bool> errorWhileLoadingField;

		#endregion

		#region Methods

		/// <summary>
		/// Seems to create a brand new list of notes based on the chart. It probably shouldn't be called mid-game
		/// because performance is iffy and it has side effects.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="recomputeStars"></param>
		/// <returns></returns>
		public List<NoteWrapper> GetNotesFromChart(CHPlayerWrapper player, bool recomputeStars) {
			var notes = (ICollection)getNotesFromChartMethod(GameManager, player.CHPlayer, recomputeStars);
			return notes.Cast<object>().Select(o => NoteWrapper.Wrap(o)).ToList();
		}
		[WrapperMethod("\u0318\u030D\u031A\u031C\u031B\u0310\u031A\u030F\u030D\u0314\u031B")]
		private static readonly FastInvokeHandler getNotesFromChartMethod;

		public void OnControllerDisconnected(ControllerStatusChangedEventArgs args) => onControllerDisconnectedMethod(GameManager, args);
		[WrapperMethod("\u0310\u030F\u0315\u030F\u0311\u0314\u0311\u0314\u0315\u0318\u0316")]
		private static readonly FastInvokeHandler onControllerDisconnectedMethod;

		public void LoadSong() => loadSongMethod(GameManager);
		[WrapperMethod("\u0316\u0319\u0312\u031B\u0316\u0316\u031A\u0316\u030D\u0318\u0318")]
		private static readonly FastInvokeHandler loadSongMethod;

		#endregion
	}
}
