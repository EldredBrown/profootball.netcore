using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleTotals"/> data store.
    /// </summary>
    public interface ISeasonTeamScheduleTotalsRepository
    {
        /// <summary>
        /// Gets a single <see cref="SeasonTeamScheduleTotals"/> entity from the data store by season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonTeamScheduleTotals"/> entity to fetch.</param>
        /// <param name="teamName">The team name of the <see cref="SeasonTeamScheduleTotals"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleTotals"/> entity.</returns>
        SeasonTeamScheduleTotals GetSeasonTeamScheduleTotals(int seasonId, string teamName);
    }
}
