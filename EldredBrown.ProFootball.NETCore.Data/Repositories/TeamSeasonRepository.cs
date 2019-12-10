using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="TeamSeason"/> data store.
    /// </summary>
    public class TeamSeasonRepository : ITeamSeasonRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public TeamSeasonRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single <see cref="TeamSeason"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeason"/> entity.</returns>
        public async Task<TeamSeason> GetTeamSeason(int id)
        {
            return await _dbContext.TeamSeasons.FindAsync(id);
        }

        /// <summary>
        /// Gets a single <see cref="TeamSeason"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <param name="seasonId">The season ID of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeason"/> entity.</returns>
        public async Task<TeamSeason> GetTeamSeasonByTeamAndSeason(string teamName, int seasonId)
        {
            return await _dbContext.TeamSeasons
                .FirstOrDefaultAsync(ts => ts.TeamName == teamName && ts.SeasonId == seasonId);
        }

        /// <summary>
        /// Gets all <see cref="TeamSeason "/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TeamSeason}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<TeamSeason>> GetTeamSeasons()
        {
            return await _dbContext.TeamSeasons.ToListAsync();
        }
    }
}
