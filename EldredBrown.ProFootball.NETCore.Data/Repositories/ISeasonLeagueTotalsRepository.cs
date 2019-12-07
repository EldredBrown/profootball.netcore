using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonLeagueTotals"/> data store.
    /// </summary>
    public interface ISeasonLeagueTotalsRepository
    {
        /// <summary>
        /// Gets a single <see cref="SeasonLeagueTotals"/> entity from the data store by season ID and league name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonLeagueTotals"/> entity to fetch.</param>
        /// <param name="leagueName">The league name of the <see cref="SeasonLeagueTotals"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonLeagueTotals"/> entity.</returns>
        SeasonLeagueTotals GetSeasonLeagueTotals(int seasonId, string leagueName);
    }
}
