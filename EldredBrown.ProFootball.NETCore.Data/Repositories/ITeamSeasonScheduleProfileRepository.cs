using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="TeamSeasonScheduleProfile"/> data store.
    /// </summary>
    public interface ITeamSeasonScheduleProfileRepository
    {
        /// <summary>
        /// Gets a single team season schedule profile (<see cref="IEnumerable{OpponentProfile}"/>) from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="IEnumerable{OpponentProfile}"/> collection.</returns>
        Task<IEnumerable<TeamSeasonOpponentProfile>> GetTeamSeasonScheduleProfile(string teamName, int seasonYear);
    }
}
