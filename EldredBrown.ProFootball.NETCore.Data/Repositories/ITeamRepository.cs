using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Interface for classes that provide access to a <see cref="Team"/> data source.
    /// </summary>
    public interface ITeamRepository
    {
        /// <summary>
        /// Gets all the <see cref="Team"/> objects in the data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Team}"/> of all the fetched objects.</returns>
        Task<IEnumerable<Team>> GetTeams();
    }
}
