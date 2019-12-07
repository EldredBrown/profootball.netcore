using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="SeasonLeague"/> data store.
    /// </summary>
    public interface ISeasonLeagueRepository
    {
        /// <summary>
        /// Gets all <see cref="SeasonLeague"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{SeasonLeague}"/> of all fetched entities.</returns>
        IEnumerable<SeasonLeague> GetSeasonLeagues();
    }
}
