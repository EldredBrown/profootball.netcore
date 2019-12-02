using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleTotals"/> data source.
    /// </summary>
    public interface ISeasonTeamScheduleTotalsRepository
    {
        /// <summary>
        /// Gets the <see cref="SeasonTeamScheduleTotals"/> object with the given season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the object to fetch.</param>
        /// <param name="teamName">The team name of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleTotals"/> object.</returns>
        SeasonTeamScheduleTotals GetSeasonTeamScheduleTotals(int seasonId, string teamName);
    }
}
