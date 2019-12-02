using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleProfile"/> data source.
    /// </summary>
    public interface ISeasonTeamScheduleProfileRepository
    {
        /// <summary>
        /// Gets the <see cref="SeasonTeamScheduleProfile"/> object with the given season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the object to fetch.</param>
        /// <param name="teamName">The team name of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleProfile"/> object.</returns>
        SeasonTeamScheduleProfile GetSeasonTeamScheduleProfile(int seasonId, string teamName);
    }
}
