using Common.Wrappers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Wrappers {
	[Wrapper(typeof(PauseMenu))]
	internal class PauseMenuWrapper {
		public readonly PauseMenu pauseMenu;

		public PauseMenuWrapper(PauseMenu pauseMenu) {
			this.pauseMenu = pauseMenu;
		}

		#region Methods

		public void RestartInPracticeMode() => restartInPracticeModeMethod.Invoke(pauseMenu, Array.Empty<object>());
		[WrapperMethod("\u0318\u031A\u0317\u030D\u031B\u031B\u0318\u0313\u030D\u030F\u0314")]
		private static readonly MethodInfo restartInPracticeModeMethod;

		public void RestartSong() => restartSongMethod.Invoke(pauseMenu, Array.Empty<object>());
		[WrapperMethod("\u030E\u0315\u0315\u0310\u0312\u030E\u0312\u031A\u0313\u0314\u0310")]
		private static readonly MethodInfo restartSongMethod;

		#endregion
	}
}
