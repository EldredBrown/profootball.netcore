using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="TeamSeason"/> data store.
    /// </summary>
    public interface ITeamSeasonRepository
    {
        /// <summary>
        /// Gets a single <see cref="TeamSeason"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeason"/> entity.</returns>
        Task<TeamSeason> GetTeamSeason(int id);

        /// <summary>
        /// Gets a single <see cref="TeamSeason"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <param name="seasonId">The season ID of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeason"/> entity.</returns>
        Task<TeamSeason> GetTeamSeasonByTeamAndSeason(string teamName, int seasonId);

        /// <summary>
        /// Gets all <see cref="TeamSeason "/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TeamSeason}"/> of all fetched entities.</returns>
        Task<IEnumerable<TeamSeason>> GetTeamSeasons();
    }
}
