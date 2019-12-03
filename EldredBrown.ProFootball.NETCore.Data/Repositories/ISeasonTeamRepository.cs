using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeam"/> data store.
    /// </summary>
    public interface ISeasonTeamRepository
    {
        /// <summary>
        /// Gets a single <see cref="SeasonTeam"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> entity.</returns>
        Task<SeasonTeam> GetSeasonTeam(int id);

        /// <summary>
        /// Gets a single <see cref="SeasonTeam"/> entity from the data store by season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <param name="teamName">The team name of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> entity.</returns>
        Task<SeasonTeam> GetSeasonTeamBySeasonAndTeam(int seasonId, string teamName);

        /// <summary>
        /// Gets all <see cref="SeasonTeam "/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{SeasonTeam}"/> of all fetched entities.</returns>
        Task<IEnumerable<SeasonTeam>> GetSeasonTeams();
    }
}
