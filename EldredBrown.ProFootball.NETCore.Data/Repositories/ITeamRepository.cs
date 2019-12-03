using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Team"/> data store.
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Gets all <see cref="Team"/> entities in the data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Team}"/> of all fetched entities.</returns>
        Task<IEnumerable<Team>> GetTeams();
    }
}
