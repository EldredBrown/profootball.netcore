using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class GameServiceTest
    {
        private IGameRepository _gameRepository;
        private IProcessGameStrategyFactory _processGameStrategyFactory;

        public GameServiceTest()
        {
            _gameRepository = A.Fake<IGameRepository>();
            _processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
        }

        [Fact]
        public async Task AddGame_WhenNewGameIsPassed_ShouldAddGameToRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(strategy);

            var newGame = new Game();

            // Act
            await service.AddGame(newGame);

            // Assert
            A.CallTo(() => _gameRepository.Add(newGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditGame_WhenNewGameAndOldGameArePassed_ShouldEditGameInRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var selectedGame = new Game();
            A.CallTo(() => _gameRepository.GetGame(A<int>.Ignored)).Returns(selectedGame);

            var downStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(downStrategy);

            var upStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(upStrategy);

            var newGame = new Game();
            var oldGame = new Game();

            // Act
            await service.EditGame(newGame, oldGame);

            // Assert
            A.CallTo(() => _gameRepository.GetGame(newGame.ID)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Update(selectedGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => downStrategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => upStrategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteGame_WhenGameIdIsPassed_ShouldDeleteGameFromRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(strategy);

            var id = 1;

            // Act
            await service.DeleteGame(id);

            // Assert
            A.CallTo(() => _gameRepository.GetGame(id)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Delete(id)).MustHaveHappened();
        }
    }
}
