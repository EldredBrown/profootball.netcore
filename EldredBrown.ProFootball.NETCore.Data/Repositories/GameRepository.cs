using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Game"/> data store.
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
        /// Adds a <see cref="Game"/> entity to the data store.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to add.</param>
        /// <returns>The added <see cref="Game"/> entity.</returns>
        public async Task<Game> Add(Game game)
        {
            await _dbContext.AddAsync(game);

            return game;
        }

        /// <summary>
        /// Deletes a <see cref="Game"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to delete.</param>
        /// <returns>The deleted <see cref="Game"/> entity.</returns>
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
        /// Updates a <see cref="Game"/> entity in the data store.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to update.</param>
        /// <returns>The updated <see cref="Game"/> entity.</returns>
        public Game Edit(Game game)
        {
            var entity = _dbContext.Games.Attach(game);
            entity.State = EntityState.Modified;

            return game;
        }

        /// <summary>
        /// Gets a single <see cref="Game"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="Game"/> entity.</returns>
        public async Task<Game> GetGame(int id)
        {
            return await _dbContext.Games.FindAsync(id);
        }

        /// <summary>
        /// Gets all <see cref="Game"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Game}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _dbContext.Games.ToListAsync();
        }

        /// <summary>
        /// Saves changes made to the data store.
        /// </summary>
        /// <returns>The number of entities affected.</returns>
        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
