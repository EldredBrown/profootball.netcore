﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to an external <see cref="Season"/> data store.
    /// </summary>
    public class SeasonRepository : ISeasonRepository
    {
        private readonly ProFootballDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="ProFootballDbContext"/> representing the database.</param>
        public SeasonRepository(ProFootballDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all <see cref="Season"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Season}"/> of all fetched entities.</returns>
        public async Task<IEnumerable<Season>> GetSeasons()
        {
            return await _dbContext.Seasons.ToListAsync();
        }

        /// <summary>
        /// Gets a single <see cref="Season"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Season"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="Season"/> entity.</returns>
        public async Task<Season> GetSeason(int id)
        {
            return await _dbContext.Seasons.FindAsync(id);
        }

        /// <summary>
        /// Adds a <see cref="Season"/> entity to the data store.
        /// </summary>
        /// <param name="season">The <see cref="Season"/> entity to add.</param>
        /// <returns>The added <see cref="Season"/> entity.</returns>
        public async Task<Season> Add(Season season)
        {
            await _dbContext.AddAsync(season);

            return season;
        }

        /// <summary>
        /// Updates a <see cref="Season"/> entity in the data store.
        /// </summary>
        /// <param name="season">The <see cref="Season"/> entity to update.</param>
        /// <returns>The updated <see cref="Season"/> entity.</returns>
        public Season Edit(Season season)
        {
            var entity = _dbContext.Seasons.Attach(season);
            entity.State = EntityState.Modified;

            return season;
        }

        /// <summary>
        /// Deletes a <see cref="Season"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Season"/> entity to delete.</param>
        /// <returns>The deleted <see cref="Season"/> entity.</returns>
        public async Task<Season> Delete(int id)
        {
            var season = await GetSeason(id);

            if (season != null)
            {
                _dbContext.Seasons.Remove(season);
            }

            return season;
        }
    }
}
