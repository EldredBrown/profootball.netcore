using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    /// <summary>
    /// Interface for services that will add, edit, and delete game data in the data store.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Adds a <see cref="IGameDecorator"/> entity to the data store.
        /// </summary>
        /// <param name="newGame">The <see cref="IGameDecorator/> entity to add to the data store.</param>
        Task AddGame(IGameDecorator game);

        /// <summary>
        /// Edits a <see cref="Game"/> entity in the data store.
        /// </summary>
        /// <param name="newGame">The <see cref="IGameDecorator"/> entity containing data to add to the data store.</param>
        /// <param name="oldGame">The <see cref="Game"/> entity containing data to remove from the data store.</param>
        Task EditGame(IGameDecorator newGame, Game oldGame);

        /// <summary>
        /// Deletes a <see cref="Game"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to delete.</param>
        Task DeleteGame(int id);
    }
}
