using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraSongUI.Wrappers {
	internal class GameManagerWrapper : WrapperBase {
		public readonly GameManager gameManager;

		public BasePlayerWrapper[] BasePlayers => ((BasePlayer[])basePlayersField.GetValue(gameManager)).Select(bp => new BasePlayerWrapper(bp)).ToArray();
		private static FieldInfo basePlayersField;
		private const string basePlayersFieldName = "̖̙̖̜̔̓̑̒̕̕̕";

		public BasePlayerWrapper UnknownBasePlayer => new BasePlayerWrapper((BasePlayer)unknownBasePlayerField.GetValue(gameManager)); //? It's null for me. 🤷‍
		private static FieldInfo unknownBasePlayerField;
		private const string unknownBasePlayerFieldName = "̖̜̜̗̒̒̔̐̔̕̚";

		public double SongLength => (double)songLengthField.GetValue(gameManager);
		private static FieldInfo songLengthField;
		private const string songLengthFieldName = "̜̘̜̗̒̔̒̏̒̓̎";

		public double SongTime => (double)songTimeField.GetValue(gameManager);
		private static FieldInfo songTimeField;
		private const string songTimeFieldName = "̛̜̗̗̜̘̍̍̒̒̎";

		public StarProgressWrapper StarProgress => new StarProgressWrapper((StarProgress)starProgressField.GetValue(gameManager));
		private static FieldInfo starProgressField;
		private const string starProgressFieldName = "starProgress";

		public SongWrapper Song => new SongWrapper(songField.GetValue(gameManager));
		private static FieldInfo songField;
		private const string songFieldName = "̛̛̛̗̙̖̑̎̒̏̚";

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
		private const string getNotesFromChartMethodName = "̛̛̘̜̍̐̏̍̔̚̚";

		public GameManagerWrapper(GameManager gameManager) {
			InitializeSingletonFields();
			this.gameManager = gameManager;
		}

		private static void InitializeSingletonFields() {
			RegisterField(ref basePlayersField, typeof(GameManager), basePlayersFieldName);
			RegisterField(ref unknownBasePlayerField, typeof(GameManager), unknownBasePlayerFieldName);
			RegisterField(ref songLengthField, typeof(GameManager), songLengthFieldName);
			RegisterField(ref songTimeField, typeof(GameManager), songTimeFieldName);
			RegisterField(ref starProgressField, typeof(GameManager), starProgressFieldName);
			RegisterField(ref songField, typeof(GameManager), songFieldName);
			RegisterMethod(ref getNotesFromChartMethod, typeof(GameManager), getNotesFromChartMethodName);
		}
	}
}
