using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="TeamSeasonScheduleAverages"/> data store.
    /// </summary>
    public interface ITeamSeasonScheduleAveragesRepository
    {
        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleAverages"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleAverages"/> entity.</returns>
        Task<TeamSeasonScheduleAverages> GetTeamSeasonScheduleAverages(string teamName, int seasonYear);
    }
}
