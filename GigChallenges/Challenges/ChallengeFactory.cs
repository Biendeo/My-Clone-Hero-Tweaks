using BiendeoCHLib.Wrappers;
using GigChallenges.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigChallenges.Challenges {
	public class ChallengeFactory : IChallengeFactory {
		public IChallenge CreateChallenge(GameManagerWrapper gameManager) {
			return new ScoreChallenge(gameManager);
		}
	}
}
