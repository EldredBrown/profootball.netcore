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
        /// Gets a single <see cref="TeamSeason"/> entity from the data store by team name and season year.
        /// </summary>
        /// <param name="teamName">The team name of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <param name="seasonYear">The season year of the <see cref="TeamSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="TeamSeason"/> entity.</returns>
        public async Task<TeamSeason> GetTeamSeasonByTeamAndSeason(string teamName, int seasonYear)
        {
            return await _dbContext.TeamSeasons
                .FirstOrDefaultAsync(ts => ts.TeamName == teamName && ts.SeasonYear == seasonYear);
        }

        /// <summary>
        /// Gets all <see cref="TeamSeason "/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TeamSeason}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<TeamSeason>> GetTeamSeasons()
        {
            return await _dbContext.TeamSeasons.ToListAsync();
        }

        /// <summary>
        /// Adds a <see cref="TeamSeason"/> entity to the data store.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> entity to add.</param>
        /// <returns>The added <see cref="TeamSeason"/> entity.</returns>
        public async Task<TeamSeason> Add(TeamSeason teamSeason)
        {
            await _dbContext.AddAsync(teamSeason);

            return teamSeason;
        }

        /// <summary>
        /// Updates a <see cref="TeamSeason"/> entity in the data store.
        /// </summary>
        /// <param name="teamSeason">The <see cref="TeamSeason"/> to update.</param>
        /// <returns>The updated <see cref="TeamSeason"/> entity.</returns>
        public TeamSeason Edit(TeamSeason teamSeason)
        {
            var entity = _dbContext.TeamSeasons.Attach(teamSeason);
            entity.State = EntityState.Modified;

            return teamSeason;
        }

        /// <summary>
        /// Deletes a <see cref="TeamSeason"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TeamSeason"/> entity to delete.</param>
        /// <returns>The deleted <see cref="TeamSeason"/> entity.</returns>
        public async Task<TeamSeason> Delete(int id)
        {
            var teamSeason = await GetTeamSeason(id);

            if (teamSeason != null)
            {
                _dbContext.TeamSeasons.Remove(teamSeason);
            }

            return teamSeason;
        }

        /// <summary>
        /// Checks to verify whether a specific <see cref="TeamSeason"/> entity exists in the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="TeamSeason"/> entity to verify.</param>
        /// <returns><c>true</c> if the entity with the given ID exists in the data store; otherwise, <c>false</c>.</returns>
        public async Task<bool> TeamSeasonExists(int id)
        {
            return await _dbContext.TeamSeasons.AnyAsync(ls => ls.ID == id);
        }
    }
}
