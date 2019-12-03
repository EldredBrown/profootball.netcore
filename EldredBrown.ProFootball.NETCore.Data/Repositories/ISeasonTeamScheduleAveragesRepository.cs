using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleAverages"/> data store.
    /// </summary>
    public interface ISeasonTeamScheduleAveragesRepository
    {
        /// <summary>
        /// Gets a single <see cref="SeasonTeamScheduleAverages"/> entity from the data store by season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonTeamScheduleAverages"/> entity to fetch.</param>
        /// <param name="teamName">The team name of the <see cref="SeasonTeamScheduleAverages"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleAverages"/> entity.</returns>
        SeasonTeamScheduleAverages GetSeasonTeamScheduleAverages(int seasonId, string teamName);
    }
}
