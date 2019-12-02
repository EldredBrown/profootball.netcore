using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external Leagues data source.
    /// </summary>
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the data source.</param>
        public LeagueRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a league to the Leagues data source.
        /// </summary>
        /// <param name="newLeague">The <see cref="League"/> object to be added.</param>
        /// <returns>The <see cref="League"/> object added.</returns>
        public League Add(League newLeague)
        {
            _dbContext.Add(newLeague);

            return newLeague;
        }

        /// <summary>
        /// Commits changes to the Leagues data source.
        /// </summary>
        /// <returns>The number of entities affected in the data source.</returns>
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a league from the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be deleted.</param>
        /// <returns>A <see cref="League"/> object representing the deleted league.</returns>
        public async Task<League> Delete(int id)
        {
            var league = await GetLeague(id);

            if (league != null)
            {
                _dbContext.Leagues.Remove(league);
            }

            return league;
        }

        /// <summary>
        /// Gets all the leagues in the Leagues data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{League}"/> of all the fetched leagues.</returns>
        public async Task<IEnumerable<League>> GetLeagues()
        {
            return await _dbContext.Leagues.ToListAsync();
        }

        /// <summary>
        /// Gets the league with the given ID from the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be fetched.</param>
        /// <returns>The fetched <see cref="League"/>.</returns>
        public async Task<League> GetLeague(int id)
        {
            return await _dbContext.Leagues.FindAsync(id);
        }

        /// <summary>
        /// Gets the league with the given long name from the Leagues data source.
        /// </summary>
        /// <param name="longName">The long name of the league to be fetched.</param>
        /// <returns>The fetched <see cref="League"/>.</returns>
        public async Task<League> GetLeagueByLongName(string longName)
        {
            return await _dbContext.Leagues.FirstOrDefaultAsync(league => league.LongName == longName);
        }

        /// <summary>
        /// Checks to verify whether a league with the given ID exists in the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be verified.</param>
        /// <returns><c>true</c> if the league with the given ID exists in the data source; otherwise, <c>false</c>.</returns>
        public async Task<bool> LeagueExists(int id)
        {
            return await _dbContext.Leagues.AnyAsync(league => league.ID == id);
        }

        /// <summary>
        /// Updates an individual league in the Leagues data source.
        /// </summary>
        /// <param name="updatedLeague">The <see cref="League"/> to be updated.</param>
        /// <returns>The updated <see cref="League"/>.</returns>
        public League Update(League updatedLeague)
        {
            var entity = _dbContext.Leagues.Attach(updatedLeague);
            entity.State = EntityState.Modified;

            return updatedLeague;
        }
    }
}
