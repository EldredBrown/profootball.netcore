using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamStanding"/> data store.
    /// </summary>
    public interface ISeasonStandingsRepository
    {
        /// <summary>
        /// Gets all <see cref="SeasonTeamStanding"/> entities in the data store.
        /// </summary>
        /// <param name="groupByDivision">Flag indicating whether to group the results by division.</param>
        /// <returns>An <see cref="IEnumerable{SeasonStanding}"/> of all fetched entities.</returns>
        Task<IEnumerable<SeasonTeamStanding>> GetSeasonStandings(int seasonId);
    }
}
