using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="TeamSeasonScheduleProfile"/> data store.
    /// </summary>
    public interface ITeamSeasonScheduleProfileRepository
    {
        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleProfile"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <param name="seasonId">The season ID of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleProfile"/> entity.</returns>
        TeamSeasonScheduleProfile GetTeamSeasonScheduleProfile(string teamName, int seasonId);
    }
}
