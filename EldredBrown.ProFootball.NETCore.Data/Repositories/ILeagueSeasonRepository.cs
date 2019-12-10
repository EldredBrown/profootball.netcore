using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="LeagueSeason"/> data store.
    /// </summary>
    public interface ILeagueSeasonRepository
    {
        /// <summary>
        /// Gets a single <see cref="LeagueSeason"/> entity in the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="LeagueSeason"/> entity.</returns>
        LeagueSeason GetLeagueSeason(int id);

        /// <summary>
        /// Gets a single <see cref="LeagueSeason"/> entity in the data store by league name and season ID.
        /// </summary>
        /// <param name="leagueName">The league name of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <param name="seasonId">The season id of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="LeagueSeason"/> entity.</returns>
        LeagueSeason GetLeagueSeasonByLeagueAndSeason(string leagueName, int seasonId);

        /// <summary>
        /// Gets all <see cref="LeagueSeason"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{LeagueSeason}"/> of all fetched entities.</returns>
        IEnumerable<LeagueSeason> GetLeagueSeasons();
    }
}
