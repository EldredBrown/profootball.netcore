using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Utilities
{
    public interface ILeagueSeasonUtility
    {
        /// <summary>
        /// Updates the games and points totals of a <see cref="LeagueSeason"/> entity."
        /// </summary>
        /// <param name="leagueSeason">The <see cref="LeagueSeason"/> entity to be updated.</param>
        /// <param name="totalGames">The value to be updated to the specified <see cref="LeagueSeason"/> entity's total games.</param>
        /// <param name="totalPoints">The value to be updated to the specified <see cref="LeagueSeason"/> entity's total points.</param>
        void UpdateGamesAndPoints(LeagueSeason leagueSeason, int totalGames, int totalPoints);
    }
}