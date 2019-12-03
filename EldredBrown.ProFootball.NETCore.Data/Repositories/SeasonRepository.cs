using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Season"/> data store.
    /// </summary>
    public class SeasonRepository : ISeasonRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public SeasonRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single <see cref="Season"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Season"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="Season"/> entity.</returns>
        public async Task<Season> GetSeason(int id)
        {
            return await _dbContext.Seasons.FindAsync(id);
        }

        /// <summary>
        /// Gets all <see cref="Season"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Season}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _dbContext.Seasons.ToListAsync();
        }
    }
}
