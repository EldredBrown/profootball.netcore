using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleProfile"/> data store.
    /// </summary>
    public interface ISeasonTeamScheduleProfileRepository
    {
        /// <summary>
        /// Gets a single <see cref="SeasonTeamScheduleProfile"/> entity from the data store by season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonTeamScheduleProfile"/> entity to fetch.</param>
        /// <param name="teamName">The team name of the <see cref="SeasonTeamScheduleProfile"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleProfile"/> entity.</returns>
        SeasonTeamScheduleProfile GetSeasonTeamScheduleProfile(int seasonId, string teamName);
    }
}
