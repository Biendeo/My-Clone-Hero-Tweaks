using BiendeoCHLib.Wrappers;
using GigChallenges.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GigChallenges.Challenges {
	public class ScoreChallenge : IChallenge {
		private GameManagerWrapper gameManager;
		private int score;

		private int bronzeScore = 30000;
		private int silverScore = 40000;
		private int goldScore = 50000;

		public ScoreChallenge(GameManagerWrapper gameManager) {
			this.gameManager = gameManager;
			score = 0;
		}

		public bool IsFeasible => true;

		public string ChallengeName => "Get The Points";

		public string ChallengeGoal => "Get a score of 50,000!";

		public float PercentageToBronze => Mathf.Clamp(score * 1.0f / bronzeScore, 0.0f, 1.0f);

		public float PercentageToSilver => Mathf.Clamp(score * 1.0f / silverScore, 0.0f, 1.0f);

		public float PercentageToGold => Mathf.Clamp(score * 1.0f / goldScore, 0.0f, 1.0f);

		public float PercentageFromBronzeToSilver => Mathf.Clamp((score - bronzeScore) * 1.0f / (silverScore - bronzeScore), 0.0f, 1.0f);

		public float PercentageFromSilverToGold => Mathf.Clamp((score - silverScore) * 1.0f / (goldScore - silverScore), 0.0f, 1.0f);

		public float PercentageOfGoldIsBronze => bronzeScore * 1.0f / goldScore;

		public float PercentageOfGoldIsSilver => silverScore * 1.0f / goldScore;

		public void Update() {
			score = gameManager.BasePlayers[0].Score;
		}
	}
}
