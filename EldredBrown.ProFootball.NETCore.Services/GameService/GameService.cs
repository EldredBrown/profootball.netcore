using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    /// <summary>
    /// Service to handle the more complicated actions of adding, editing, or deleting games in the data store.
    /// </summary>
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IProcessGameStrategyFactory _processGameStrategyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="gameUtility">The utility by which game entity object data will be accessed.</param>
        /// <param name="gameRepository">The repository by which game data will be accessed.</param>
        /// <param name="processGameStrategyFactory">The factory that will initialize the needed <see cref="ProcessGameStrategyBase"/> subclass.</param>
        public GameService(
            IGameRepository gameRepository,
            IProcessGameStrategyFactory processGameStrategyFactory)
        {
            _gameRepository = gameRepository;
            _processGameStrategyFactory = processGameStrategyFactory;
        }

        /// <summary>
        /// Adds a <see cref="Game"/> entity to the data store.
        /// </summary>
        /// <param name="newGame">The <see cref="Game"/> entity to add to the data store.</param>
        public async Task AddGame(Game newGame)
        {
            var newGameDecorator = new GameDecorator(newGame);
            newGameDecorator.DecideWinnerAndLoser();

            await _gameRepository.Add(newGame);

            await EditTeams(Direction.Up, newGameDecorator);
        }

        /// <summary>
        /// Edits a <see cref="Game"/> entity in the data store.
        /// </summary>
        /// <param name="newGame">The <see cref="Game"/> entity containing data to add to the data store.</param>
        /// <param name="oldGame">The <see cref="Game"/> entity containing data to remove from the data store.</param>
        public async Task EditGame(Game newGame, Game oldGame)
        {
            var newGameDecorator = new GameDecorator(newGame);
            newGameDecorator.DecideWinnerAndLoser();

            var selectedGame = await _gameRepository.GetGame(newGameDecorator.ID);
            GameDecorator selectedGameDecorator = null;
            if (selectedGame != null)
            {
                selectedGameDecorator = new GameDecorator(selectedGame);
            }

            selectedGameDecorator.Edit(newGameDecorator);

            _gameRepository.Update(selectedGame);

            var oldGameDecorator = new GameDecorator(oldGame);
            await EditTeams(Direction.Down, oldGameDecorator);
            await EditTeams(Direction.Up, newGameDecorator);
        }

        /// <summary>
        /// Deletes a <see cref="Game"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to delete.</param>
        public async Task DeleteGame(int id)
        {
            var oldGame = await _gameRepository.GetGame(id);
            GameDecorator oldGameDecorator = null;
            if (oldGame != null)
            {
                oldGameDecorator = new GameDecorator(oldGame);
            }

            await EditTeams(Direction.Down, oldGameDecorator);

            await _gameRepository.Delete(id);
        }

        private async Task EditTeams(Direction direction, GameDecorator gameDecorator)
        {
            var processGameStrategy = _processGameStrategyFactory.CreateStrategy(direction);

            // TODO - 2020-09-25: Implement ObjectNotFoundException class so it can be caught and used here.
            //try
            //{
            await processGameStrategy.ProcessGame(gameDecorator);
            //}
            //catch (ObjectNotFoundException ex)
            //{
            //    Log.Error("ObjectNotFoundException in GamesService.EditTeams: " + ex.Message);
            //    _sharedService.ShowExceptionMessage(ex, "ObjectNotFoundException");
            //}
        }
    }
}
