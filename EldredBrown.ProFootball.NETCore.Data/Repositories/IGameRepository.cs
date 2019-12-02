using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Game"/> data source.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Adds a new game to the data source.
        /// </summary>
        /// <param name="newGame">The game to add to the data source.</param>
        /// <returns>The <see cref="Game"/> added to the data source.</returns>
        Task<Game> Add(Game newGame);

        /// <summary>
        /// Deletes an existing game from the data source.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>The <see cref="Game"/> deleted from the data source.</returns>
        Task<Game> Delete(int id);

        /// <summary>
        /// Updates an existing game in the data source.
        /// </summary>
        /// <param name="updatedGame">The game to update.</param>
        /// <returns>The <see cref="Game"/> updated in the data source.</returns>
        Game Edit(Game updatedGame);

        /// <summary>
        /// Gets from the data source the <see cref="Game"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> object to fetch.</param>
        /// <returns>The fetched <see cref="Game"/> object.</returns>
        Task<Game> GetGame(int id);

        /// <summary>
        /// Gets all <see cref="Game"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Game}"/> of all the fetched objects.</returns>
        Task<IEnumerable<Game>> GetGames();

        /// <summary>
        /// Saves all changes made to the data source since the last save.
        /// </summary>
        /// <returns>The number of records affected.</returns>
        Task<int> SaveChanges();
    }
}
