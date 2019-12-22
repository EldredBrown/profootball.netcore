using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class LeagueSeasonRepository : ILeagueSeasonRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueSeasonRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the data store.</param>
        public LeagueSeasonRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all <see cref="LeagueSeason"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{LeagueSeason}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<LeagueSeason>> GetLeagueSeasons()
        {
            return await _dbContext.LeagueSeasons.ToListAsync();
        }

        /// <summary>
        /// Gets a single <see cref="LeagueSeason"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="LeagueSeason"/> entity.</returns>
        public async Task<LeagueSeason> GetLeagueSeason(int id)
        {
            return await _dbContext.LeagueSeasons.FindAsync(id);
        }

        /// <summary>
        /// Gets a single <see cref="LeagueSeason"/> entity from the data store by league name and season year.
        /// </summary>
        /// <param name="leagueName">The name of the league of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <param name="seasonYear">The year of the season of the <see cref="LeagueSeason"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="LeagueSeason"/> entity.</returns>
        public LeagueSeason GetLeagueSeasonByLeagueAndSeason(string leagueName, int seasonYear)
        {
            return _dbContext.LeagueSeasons
                .FirstOrDefault(ls => ls.LeagueName == leagueName && ls.SeasonYear == seasonYear);
        }

        /// <summary>
        /// Adds a <see cref="LeagueSeason"/> entity to the data store.
        /// </summary>
        /// <param name="leagueSeason">The <see cref="LeagueSeason"/> entity to add.</param>
        /// <returns>The added <see cref="LeagueSeason"/> entity.</returns>
        public async Task<LeagueSeason> Add(LeagueSeason leagueSeason)
        {
            await _dbContext.AddAsync(leagueSeason);

            return leagueSeason;
        }

        /// <summary>
        /// Updates a <see cref="LeagueSeason"/> entity in the data store.
        /// </summary>
        /// <param name="leagueSeason">The <see cref="LeagueSeason"/> to update.</param>
        /// <returns>The updated <see cref="LeagueSeason"/> entity.</returns>
        public LeagueSeason Edit(LeagueSeason leagueSeason)
        {
            var entity = _dbContext.LeagueSeasons.Attach(leagueSeason);
            entity.State = EntityState.Modified;

            return leagueSeason;
        }

        /// <summary>
        /// Deletes a <see cref="LeagueSeason"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="LeagueSeason"/> entity to delete.</param>
        /// <returns>The deleted <see cref="LeagueSeason"/> entity.</returns>
        public async Task<LeagueSeason> Delete(int id)
        {
            var leagueSeason = await GetLeagueSeason(id);

            if (leagueSeason != null)
            {
                _dbContext.LeagueSeasons.Remove(leagueSeason);
            }

            return leagueSeason;
        }

        /// <summary>
        /// Checks to verify whether a specific <see cref="LeagueSeason"/> entity exists in the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="LeagueSeason"/> entity to verify.</param>
        /// <returns><c>true</c> if the entity with the given ID exists in the data store; otherwise, <c>false</c>.</returns>
        public async Task<bool> LeagueSeasonExists(int id)
        {
            return await _dbContext.LeagueSeasons.AnyAsync(ls => ls.ID == id);
        }
    }
}
