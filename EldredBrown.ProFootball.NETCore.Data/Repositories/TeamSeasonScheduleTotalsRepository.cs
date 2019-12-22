using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides read access to the GetTeamSeasonScheduleTotals stored procedure.
    /// </summary>
    public class TeamSeasonScheduleTotalsRepository : ITeamSeasonScheduleTotalsRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleTotalsRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public TeamSeasonScheduleTotalsRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleTotals"/> entity from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleTotals"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeasonScheduleTotals"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleTotals"/> entity.</returns>
        public async Task<TeamSeasonScheduleTotals> GetTeamSeasonScheduleTotals(string teamName, int seasonYear)
        {
            return (await _dbContext.TeamSeasonScheduleTotals.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleTotals {teamName}, {seasonYear}").ToListAsync()).FirstOrDefault();
        }
    }
}
