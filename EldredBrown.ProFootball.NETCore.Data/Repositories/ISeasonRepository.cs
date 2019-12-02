using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Season"/> data source.
    /// </summary>
    public interface ISeasonRepository
    {
        /// <summary>
        /// Gets the <see cref="Season"/> object with the given ID.
        /// </summary>
        /// <param name="id">The ID of the object to fetch.</param>
        /// <returns>The fetched <see cref="Season"/> object.</returns>
        Task<Season> GetSeason(int id);

        /// <summary>
        /// Gets all the <see cref="Season"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Season}"/> of all the fetched objects.</returns>
        Task<IEnumerable<Season>> GetSeasons();
    }
}
