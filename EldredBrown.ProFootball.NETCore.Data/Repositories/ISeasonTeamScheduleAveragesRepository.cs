using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonTeamScheduleAverages"/> data source.
    /// </summary>
    public interface ISeasonTeamScheduleAveragesRepository
    {
        /// <summary>
        /// Gets the <see cref="SeasonTeamScheduleAverages"/> object with the given season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the object to fetch.</param>
        /// <param name="teamName">The team name of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeamScheduleAverages"/> object.</returns>
        SeasonTeamScheduleAverages GetSeasonTeamScheduleAverages(int seasonId, string teamName);
    }
}
