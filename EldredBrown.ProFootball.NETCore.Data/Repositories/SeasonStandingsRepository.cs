using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides read access to the GetSeasonStandings stored procedure.
    /// </summary>
    public class SeasonStandingsRepository : ISeasonStandingsRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonStandingsRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public SeasonStandingsRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all <see cref="SeasonTeamStanding"/> entities in the data store.
        /// </summary>
        /// <param name="seasonId">The ID of the season for which standings will be fetched.</param>
        /// <param name="groupByDivision">Flag indicating whether to group the results by division.</param>
        /// <returns>An <see cref="IEnumerable{SeasonStanding}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<SeasonTeamStanding>> GetSeasonStandings(int seasonId)
        {
            return (await _dbContext.SeasonStandings.FromSqlInterpolated(
                $"sp_GetSeasonStandings {seasonId}").ToListAsync());
        }
    }
}
