using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="SeasonTeam"/> data source.
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
        /// Gets the <see cref="SeasonTeam"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> object.</returns>
        public async Task<SeasonTeam> GetSeasonTeam(int id)
        {
            return await _dbContext.SeasonTeams.FindAsync(id);
        }

        /// <summary>
        /// Gets the <see cref="SeasonTeam"/> object with the given season ID and team name.
        /// </summary>
        /// <param name="seasonId">The season ID of the object to fetch.</param>
        /// <param name="teamName">The team name of the object to fetch.</param>
        /// <returns>The fetched <see cref="SeasonTeam"/> object.</returns>
        public async Task<SeasonTeam> GetSeasonTeamBySeasonAndTeam(int seasonId, string teamName)
        {
            return await _dbContext.SeasonTeams
                .FirstOrDefaultAsync(st => st.SeasonId == seasonId && st.TeamName == teamName);
        }

        /// <summary>
        /// Gets all the <see cref="SeasonTeam "/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{SeasonTeam}"/> of all the fetched objects.</returns>
        public async Task<IEnumerable<SeasonTeam>> GetSeasonTeams()
        {
            return await _dbContext.SeasonTeams.ToListAsync();
        }
    }
}
