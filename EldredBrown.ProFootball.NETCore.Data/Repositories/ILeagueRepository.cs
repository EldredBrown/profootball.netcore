using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    /// <summary>
    /// Provides CRUD access to a Leagues data source.
    /// </summary>
    public interface ILeagueRepository
    {
        /// <summary>
        /// Adds a league to the Leagues data source.
        /// </summary>
        /// <param name="newLeague">The <see cref="League"/> object to be added.</param>
        /// <returns>The <see cref="League"/> object added.</returns>
        League Add(League newLeague);

        /// <summary>
        /// Commits changes to the Leagues data source.
        /// </summary>
        /// <returns>The number of entities affected in the data source.</returns>
        Task<int> Commit();

        /// <summary>
        /// Deletes a league from the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be deleted.</param>
        /// <returns>A <see cref="League"/> object representing the deleted league.</returns>
        Task<League> Delete(int id);

        /// <summary>
        /// Gets all the leagues in the Leagues data source.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{League}"/> of all the fetched leagues.</returns>
        Task<IEnumerable<League>> GetLeagues();

        /// <summary>
        /// Gets the league with the given ID from the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be fetched.</param>
        /// <returns>The fetched <see cref="League"/>.</returns>
        Task<League> GetLeague(int id);

        /// <summary>
        /// Gets the league with the given long name from the Leagues data source.
        /// </summary>
        /// <param name="longName">The long name of the league to be fetched.</param>
        /// <returns>The fetched <see cref="League"/>.</returns>
        Task<League> GetLeagueByLongName(string longName);

        /// <summary>
        /// Checks to verify whether a league with the given ID exists in the Leagues data source.
        /// </summary>
        /// <param name="id">The ID of the league to be verified.</param>
        /// <returns><c>true</c> if the league with the given ID exists in the data source; otherwise, <c>false</c>.</returns>
        Task<bool> LeagueExists(int id);

        /// <summary>
        /// Updates an individual league in the Leagues data source.
        /// </summary>
        /// <param name="updatedLeague">The <see cref="League"/> to be updated.</param>
        /// <returns>The updated <see cref="League"/>.</returns>
        League Update(League updatedLeague);
    }
}
