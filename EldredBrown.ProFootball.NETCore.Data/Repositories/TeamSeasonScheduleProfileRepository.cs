using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides read access to the GetTeamSeasonScheduleProfile stored procedure.
    /// </summary>
    public class TeamSeasonScheduleProfileRepository : ITeamSeasonScheduleProfileRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleProfileRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public TeamSeasonScheduleProfileRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a single team season schedule profile (<see cref="IEnumerable{OpponentProfile}"/>) from the data store
        /// by team name and season year.
        /// </summary>
        /// <param name="teamName">
        /// The team name of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.
        /// </param>
        /// <param name="seasonYear">
        /// The season year of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.
        /// </param>
        /// <returns>The fetched <see cref="IEnumerable{OpponentProfile}"/> collection.</returns>
        public IEnumerable<TeamSeasonOpponentProfile> GetTeamSeasonScheduleProfile(string teamName, int seasonYear)
        {
            return _dbContext.TeamSeasonScheduleProfile.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleProfile {teamName}, {seasonYear}").ToList();
        }

        /// <summary>
        /// Gets a single team season schedule profile (<see cref="IEnumerable{OpponentProfile}"/>) asynchronously from
        /// the data store by team name and season year.
        /// </summary>
        /// <param name="teamName">
        /// The team name of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.
        /// </param>
        /// <param name="seasonYear">
        /// The season year of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.
        /// </param>
        /// <returns>The fetched <see cref="IEnumerable{OpponentProfile}"/> collection.</returns>
        public async Task<IEnumerable<TeamSeasonOpponentProfile>> GetTeamSeasonScheduleProfileAsync(string teamName,
            int seasonYear)
        {
            return await _dbContext.TeamSeasonScheduleProfile.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleProfile {teamName}, {seasonYear}").ToListAsync();
        }
    }
}
