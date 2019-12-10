using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="TeamSeasonScheduleTotals"/> data store.
    /// </summary>
    public interface ITeamSeasonScheduleTotalsRepository
    {
        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleTotals"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleTotals"/> entity to fetch.</param>
        /// <param name="seasonId">The season ID of the <see cref="TeamSeasonScheduleTotals"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleTotals"/> entity.</returns>
        TeamSeasonScheduleTotals GetTeamSeasonScheduleTotals(string teamName, int seasonId);
    }
}
