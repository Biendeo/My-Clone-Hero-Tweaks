using BiendeoCHLib.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigChallenges.Interfaces {
	/// <summary>
	/// Describes a challenge the player can take. Challenges should be given a <see cref="GameManagerWrapper"/> so that
	/// they can observe the game state.
	/// </summary>
	public interface IChallenge {
		/// <summary>
		/// Returns whether this kind of challenge is practical for the given chart. This would rule out things such as
		/// charts with no sustains requiring whammy time, or charts with no notes having...anything.
		/// </summary>
		/// <returns></returns>
		bool IsFeasible { get; }

		/// <summary>
		/// The name of this challenge (human readable).
		/// </summary>
		/// <returns></returns>
		string ChallengeName { get; }

		/// <summary>
		/// A description of this challenge that should indicate to the user what they need to achieve.
		/// </summary>
		/// <returns></returns>
		string ChallengeGoal { get; }

		/// <summary>
		/// The percentage from zero to the bronze target [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageToBronze { get; }

		/// <summary>
		/// The percentage from zero to the silver target [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageToSilver { get; }

		/// <summary>
		/// The percentage from the bronze target to the silver target [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageFromBronzeToSilver { get; }

		/// <summary>
		/// The percentage from zero to the gold target [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageToGold { get; }

		/// <summary>
		/// The percentage from the silver target to the gold target [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageFromSilverToGold { get; }

		/// <summary>
		/// The percentage from zero to the bronze target where gold is 1 [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageOfGoldIsBronze { get; }

		/// <summary>
		/// The percentage from zero to the silver target where gold is 1 [0, 1].
		/// </summary>
		/// <returns></returns>
		float PercentageOfGoldIsSilver { get; }

		/// <summary>
		/// Tells the challenge to update itself; useful if performance needs to be watched.
		/// </summary>
		void Update();
	}
}
