using BiendeoCHLib.Wrappers.Attributes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiendeoCHLib.Wrappers {
	[Wrapper(typeof(PauseMenu))]
	public struct PauseMenuWrapper {
		public readonly PauseMenu pauseMenu;

		public PauseMenuWrapper(PauseMenu pauseMenu) {
			this.pauseMenu = pauseMenu;
		}

		#region Methods

		public void RestartInPracticeMode() => restartInPracticeModeMethod(pauseMenu);
		[WrapperMethod("\u0318\u031A\u0317\u030D\u031B\u031B\u0318\u0313\u030D\u030F\u0314")]
		private static readonly FastInvokeHandler restartInPracticeModeMethod;

		public void RestartSong() => restartSongMethod(pauseMenu);
		[WrapperMethod("\u030E\u0315\u0315\u0310\u0312\u030E\u0312\u031A\u0313\u0314\u0310")]
		private static readonly FastInvokeHandler restartSongMethod;

		#endregion
	}
}
