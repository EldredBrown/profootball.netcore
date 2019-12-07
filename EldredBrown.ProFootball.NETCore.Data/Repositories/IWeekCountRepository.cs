using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="WeekCount"/> data store.
    /// </summary>
    public interface IWeekCountRepository
    {
        /// <summary>
        /// Gets a single <see cref="WeekCount"/> entity from the data store by ID.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="WeekCount"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="WeekCount"/> entity.</returns>
        WeekCount GetWeekCount(int seasonId);
    }
}
