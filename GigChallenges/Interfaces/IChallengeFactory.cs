using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigChallenges.Interfaces {
	interface IChallengeFactory {
		/// <summary>
		/// Creates a challenge given the game state. This should be run once after the game enters the Gameplay scene.
		/// </summary>
		/// <param name="gameManager"></param>
		/// <returns></returns>
		IChallenge CreateChallenge(GameManagerWrapper gameManager);
	}
}
