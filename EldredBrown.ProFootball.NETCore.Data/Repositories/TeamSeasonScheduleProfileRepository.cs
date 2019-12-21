﻿using System.Collections.Generic;
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
        /// Gets a single team season schedule profile (<see cref="IEnumerable{OpponentProfile}"/>) from the data store by team name and season ID.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <param name="seasonId">The season ID of the <see cref="TeamSeasonScheduleProfile"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="IEnumerable{OpponentProfile}"/> collection.</returns>
        public async Task<IEnumerable<TeamSeasonOpponentProfile>> GetTeamSeasonScheduleProfile(
            string teamName, int seasonId)
        {
            return await _dbContext.TeamSeasonScheduleProfile.FromSqlInterpolated(
                $"sp_GetTeamSeasonScheduleProfile {teamName}, {seasonId}").ToListAsync();
        }
    }
}