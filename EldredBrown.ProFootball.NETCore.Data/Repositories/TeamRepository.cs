using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Team"/> data source.
    /// </summary>
    public class TeamRepository : ITeamRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public TeamRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all the <see cref="Team"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Team}"/> of all the fetched objects.</returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }
    }
}
