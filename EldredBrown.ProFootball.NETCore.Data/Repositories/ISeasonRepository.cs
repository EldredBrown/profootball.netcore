using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Season"/> data store.
    /// </summary>
    public interface ISeasonRepository
    {
        /// <summary>
        /// Gets a single <see cref="Season"/> entity from the data store by ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Season"/> entity to fetch.</param>
        /// <returns>The fetched <see cref="Season"/> entity.</returns>
        Task<Season> GetSeason(int id);

        /// <summary>
        /// Gets all <see cref="Season"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Season}"/> of all fetched entities.</returns>
        Task<IEnumerable<Season>> GetSeasons();
    }
}
