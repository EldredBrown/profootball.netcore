using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides read access to the TeamSeasonScheduleAverages stored procedure.
    /// </summary>
    public class TeamSeasonScheduleAveragesRepository : ITeamSeasonScheduleAveragesRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleAveragesRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public TeamSeasonScheduleAveragesRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleAverages"/> entity from the data store by team name and season
        /// year.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleAverages"/> entity.</returns>
        public TeamSeasonScheduleAverages? GetTeamSeasonScheduleAverages(string teamName, int seasonYear)
        {
            return _dbContext.TeamSeasonScheduleAverages.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleAverages {teamName}, {seasonYear}").ToList().FirstOrDefault();
        }

        /// <summary>
        /// Gets a single <see cref="TeamSeasonScheduleAverages"/> entity asynchronously from the data store by team
        /// name and season year.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeasonScheduleAverages"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeasonScheduleAverages"/> entity.</returns>
        public async Task<TeamSeasonScheduleAverages?> GetTeamSeasonScheduleAveragesAsync(string teamName,
            int seasonYear)
        {
            return (await _dbContext.TeamSeasonScheduleAverages.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleAverages {teamName}, {seasonYear}").ToListAsync()).FirstOrDefault();
        }
    }
}
