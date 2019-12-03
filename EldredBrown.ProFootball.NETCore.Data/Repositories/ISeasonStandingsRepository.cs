using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonStanding"/> data store.
    /// </summary>
    public interface ISeasonStandingsRepository
    {
        /// <summary>
        /// Gets all <see cref="SeasonStanding"/> entities in the data store.
        /// </summary>
        /// <param name="groupByDivision">Flag indicating whether to group the results by division.</param>
        /// <returns>An <see cref="IEnumerable{SeasonStanding}"/> of all fetched entities.</returns>
        IEnumerable<SeasonStanding> GetSeasonStandings(bool groupByDivision);
    }
}
