﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="League"/> data store.
    /// </summary>
    public interface ILeagueRepository
    {
        /// <summary>
        /// Adds a <see cref="League"/> entity to the data store.
        /// </summary>
        /// <param name="league">The <see cref="League"/> entity to add.</param>
        /// <returns>The added <see cref="League"/> entity.</returns>
        League Add(League league);

        /// <summary>
        /// Commits changes to the data store.
        /// </summary>
        /// <returns>The number of entities affected.</returns>
        Task<int> Commit();

        /// <summary>
        /// Deletes a <see cref="League"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to delete.</param>
        /// <returns>The deleted <see cref="League"/> entity.</returns>
        Task<League> Delete(int id);

        /// <summary>
        /// Gets a single <see cref="League"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="League"/> entity.</returns>
        Task<League> GetLeague(int id);

        /// <summary>
        /// Gets a single <see cref="League"/> entity from the data store by long name.
        /// </summary>
        /// <param name="longName">The long name of the <see cref="League"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="League"/> entity.</returns>
        Task<League> GetLeagueByLongName(string longName);

        /// <summary>
        /// Gets all <see cref="League"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{League}"/> of all fetched entities.</returns>
        Task<IEnumerable<League>> GetLeagues();

        /// <summary>
        /// Checks to verify whether a <see cref="League"/> entity exists in the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to verify.</param>
        /// <returns><c>true</c> if the entity with the given ID exists in the data store; otherwise, <c>false</c>.</returns>
        Task<bool> LeagueExists(int id);

        /// <summary>
        /// Updates a <see cref="League"/> entity in the data store.
        /// </summary>
        /// <param name="league">The <see cref="League"/> to update.</param>
        /// <returns>The updated <see cref="League"/> entity.</returns>
        League Update(League league);
    }
}
