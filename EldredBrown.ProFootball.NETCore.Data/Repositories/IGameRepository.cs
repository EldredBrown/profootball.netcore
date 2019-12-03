using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Game"/> data store.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Adds a <see cref="Game"/> entity to the data store.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to add.</param>
        /// <returns>The added <see cref="Game"/> entity.</returns>
        Task<Game> Add(Game game);

        /// <summary>
        /// Deletes a <see cref="Game"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to delete.</param>
        /// <returns>The deleted <see cref="Game"/> entity.</returns>
        Task<Game> Delete(int id);

        /// <summary>
        /// Updates a <see cref="Game"/> entity in the data store.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to update.</param>
        /// <returns>The updated <see cref="Game"/> entity.</returns>
        Game Edit(Game game);

        /// <summary>
        /// Gets a single <see cref="Game"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="Game"/> entity.</returns>
        Task<Game> GetGame(int id);

        /// <summary>
        /// Gets all <see cref="Game"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Game}"/> of all fetched entities.</returns>
        Task<IEnumerable<Game>> GetGames();

        /// <summary>
        /// Saves changes made to the data store.
        /// </summary>
        /// <returns>The number of entities affected.</returns>
        Task<int> SaveChanges();
    }
}
