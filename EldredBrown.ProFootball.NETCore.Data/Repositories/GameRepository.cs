using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Game"/> data source.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public GameRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new game to the data source.
        /// </summary>
        /// <param name="newGame">The game to add to the data source.</param>
        /// <returns>The <see cref="Game"/> added to the data source.</returns>
        public async Task<Game> Add(Game newGame)
        {
            await _dbContext.AddAsync(newGame);

            return newGame;
        }

        /// <summary>
        /// Deletes an existing game from the data source.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>The <see cref="Game"/> deleted from the data source.</returns>
        public async Task<Game> Delete(int id)
        {
            var game = await GetGame(id);

            if (game != null)
            {
                _dbContext.Games.Remove(game);
            }

            return game;
        }

        /// <summary>
        /// Updates an existing game in the data source.
        /// </summary>
        /// <param name="updatedGame">The game to update.</param>
        /// <returns>The <see cref="Game"/> updated in the data source.</returns>
        public Game Edit(Game updatedGame)
        {
            var entity = _dbContext.Games.Attach(updatedGame);
            entity.State = EntityState.Modified;

            return updatedGame;
        }

        /// <summary>
        /// Gets the <see cref="Game"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> object to fetch.</param>
        /// <returns>The fetched <see cref="Game"/> object.</returns>
        public async Task<Game> GetGame(int id)
        {
            return await _dbContext.Games.FindAsync(id);
        }

        /// <summary>
        /// Gets all <see cref="Game"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Game}"/> of all the fetched objects.</returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _dbContext.Games.ToListAsync();
        }

        /// <summary>
        /// Saves all changes made to the data source since the last save.
        /// </summary>
        /// <returns>The number of records affected.</returns>
        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
