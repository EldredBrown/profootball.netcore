using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public interface ITeamSeasonUtility
    {
        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Final Pythagorean Winning Percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        void CalculateFinalPythagoreanWinningPercentage(TeamSeason teamSeason);

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's Pythagorean wins and losses.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        void CalculatePythagoreanWinsAndLosses(TeamSeason teamSeason);

        /// <summary>
        /// Calculates and updates the current <see cref="TeamSeason"/> entity's winning percentage.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to be modified.</param>
        void CalculateWinningPercentage(TeamSeason teamSeason);

        /// <summary>
        /// Updates the offensive and defensive averages, factors, and indices for the attached entity.
        /// </summary>
        /// <param name="teamSeasonScheduleAveragePointsFor"></param>
        /// <param name="teamSeasonScheduleAveragePointsAgainst"></param>
        /// <param name="leagueSeasonAveragePoints"></param>
        void UpdateRankings(TeamSeason teamSeason, double teamSeasonScheduleAveragePointsFor,
            double teamSeasonScheduleAveragePointsAgainst, double leagueSeasonAveragePoints);
    }
}