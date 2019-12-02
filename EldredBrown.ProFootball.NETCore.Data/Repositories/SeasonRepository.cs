using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Season"/> data source.
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
        /// Gets the <see cref="Season"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object to fetch.</param>
        /// <returns>The fetched <see cref="Season"/> object.</returns>
        public async Task<Season> GetSeason(int id)
        {
            return await _dbContext.Seasons.FindAsync(id);
        }

        /// <summary>
        /// Gets all <see cref="Season"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Season}"/> of all the fetched objects.</returns>
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _dbContext.Seasons.ToListAsync();
        }
    }
}
