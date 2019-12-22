using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="League"/> data store.
    /// </summary>
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the data store.</param>
        public LeagueRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all <see cref="League"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{League}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<League>> GetLeagues()
        {
            return await _dbContext.Leagues.ToListAsync();
        }

        /// <summary>
        /// Gets a single <see cref="League"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="League"/> entity.</returns>
        public async Task<League> GetLeague(int id)
        {
            return await _dbContext.Leagues.FindAsync(id);
        }

        /// <summary>
        /// Adds a <see cref="League"/> entity to the data store.
        /// </summary>
        /// <param name="league">The <see cref="League"/> entity to add.</param>
        /// <returns>The added <see cref="League"/> entity.</returns>
        public async Task<League> Add(League league)
        {
            await _dbContext.AddAsync(league);

            return league;
        }

        /// <summary>
        /// Updates a <see cref="League"/> entity in the data store.
        /// </summary>
        /// <param name="league">The <see cref="League"/> to update.</param>
        /// <returns>The updated <see cref="League"/> entity.</returns>
        public League Edit(League league)
        {
            var entity = _dbContext.Leagues.Attach(league);
            entity.State = EntityState.Modified;

            return league;
        }

        /// <summary>
        /// Deletes a <see cref="League"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to delete.</param>
        /// <returns>The deleted <see cref="League"/> entity.</returns>
        public async Task<League> Delete(int id)
        {
            var league = await GetLeague(id);

            if (league != null)
            {
                _dbContext.Leagues.Remove(league);
            }

            return league;
        }

        /// <summary>
        /// Checks to verify whether a specific <see cref="League"/> entity exists in the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="League"/> entity to verify.</param>
        /// <returns><c>true</c> if the entity with the given ID exists in the data store; otherwise, <c>false</c>.</returns>
        public async Task<bool> LeagueExists(int id)
        {
            return await _dbContext.Leagues.AnyAsync(l => l.ID == id);
        }
    }
}
