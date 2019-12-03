using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="SeasonTeam"/> data store.
    /// </summary>
    public class SeasonTeamRepository : ISeasonTeamRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonTeamRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public SeasonTeamRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single <see cref="SeasonTeam"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> entity.</returns>
        public async Task<SeasonTeam> GetSeasonTeam(int id)
        {
            return await _dbContext.SeasonTeams.FindAsync(id);
        }

        /// <summary>
        /// Gets a single <see cref="SeasonTeam"/> entity from the data store by season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <param name="teamName">The team name of the <see cref="SeasonTeam"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> entity.</returns>
        public async Task<SeasonTeam> GetSeasonTeamBySeasonAndTeam(int seasonId, string teamName)
        {
            return await _dbContext.SeasonTeams
                .FirstOrDefaultAsync(st => st.SeasonId == seasonId && st.TeamName == teamName);
        }

        /// <summary>
        /// Gets all <see cref="SeasonTeam "/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{SeasonTeam}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<SeasonTeam>> GetSeasonTeams()
        {
            return await _dbContext.SeasonTeams.ToListAsync();
        }
    }
}
