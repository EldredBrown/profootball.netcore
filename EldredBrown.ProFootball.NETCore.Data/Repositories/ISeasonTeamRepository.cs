using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeam"/> data source.
    /// </summary>
    public interface ISeasonTeamRepository
    {
        /// <summary>
        /// Gets the <see cref="SeasonTeam"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> object.</returns>
        Task<SeasonTeam> GetSeasonTeam(int id);

        /// <summary>
        /// Gets the <see cref="SeasonTeam"/> object with the given season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the object to fetch.</param>
        /// <param name="teamName">The team name of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> object.</returns>
        Task<SeasonTeam> GetSeasonTeamBySeasonAndTeam(int seasonId, string teamName);

        /// <summary>
        /// Gets all the <see cref="SeasonTeam "/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{SeasonTeam}"/> of all the fetched objects.</returns>
        Task<IEnumerable<SeasonTeam>> GetSeasonTeams();
    }
}
