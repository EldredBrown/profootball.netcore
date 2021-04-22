using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services.Exceptions;
using FakeItEasy;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class GameServiceTest
    {
        private readonly IGameRepository _gameRepository;
        private readonly IProcessGameStrategyFactory _processGameStrategyFactory;

        public GameServiceTest()
        {
            _gameRepository = A.Fake<IGameRepository>();
            _processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
        }

        [Fact]
        public async Task AddGame_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            Game? newGame = null;

            // Act
            Func<Task> func = new Func<Task>(async () => await service.AddGameAsync(newGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.AddGameAsync: newGame");
        }

        [Fact]
        public async Task AddGame_WhenNewGameIsPassed_ShouldAddGameToRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(strategy);

            // Act
            await service.AddGameAsync(newGame);

            // Assert
            A.CallTo(() => _gameRepository.AddAsync(newGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditGame_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            Game? newGame = null;
            Game? oldGame = null;

            // Act
            Func<Task> func = new Func<Task>(async () => await service.EditGameAsync(newGame!, oldGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGameAsync: newGame");
        }

        [Fact]
        public async Task EditGame_WhenOldGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();
            Game? oldGame = null;

            // Act
            Func<Task> func = new Func<Task>(async () => await service.EditGameAsync(newGame, oldGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGameAsync: oldGame");
        }

        [Fact]
        public async Task EditGame_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            A.CallTo(() => _gameRepository.GetGameAsync(A<int>.Ignored)).Returns<Game?>(null);            

            // Act
            Func<Task> func = new Func<Task>(async () => await service.EditGameAsync(newGame, oldGame));

            // Assert
            var ex = await func.ShouldThrowAsync<EntityNotFoundException>();
            ex.Message.ShouldBe<string>($"{service.GetType()}.EditGameAsync: The selected Game entity could not be found.");
        }

        [Fact]
        public async Task EditGame_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsFound_ShouldEditGameInRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            var selectedGame = new Game();
            A.CallTo(() => _gameRepository.GetGameAsync(A<int>.Ignored)).Returns(selectedGame);

            var downStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(downStrategy);

            var upStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(upStrategy);

            // Act
            await service.EditGameAsync(newGame, oldGame);

            // Assert
            A.CallTo(() => _gameRepository.GetGameAsync(newGame.ID)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Update(selectedGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => downStrategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => upStrategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteGame_WhenGameWithIdIsNotFoundInRepository_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var id = 1;

            A.CallTo(() => _gameRepository.GetGameAsync(id)).Returns<Game?>(null);

            // Act
            Func<Task> func = new Func<Task>(async () => await service.DeleteGameAsync(id));

            // Assert
            var ex = await func.ShouldThrowAsync<EntityNotFoundException>();
            ex.Message.ShouldBe<string>(
                $"{service.GetType()}.DeleteGameAsync: A Game entity with ID={id} could not be found.");
        }

        [Fact]
        public async Task DeleteGame_WhenGameWithIdIsFoundInRepository_ShouldDeleteGameFromRepository()
        {
            // Arrange
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var id = 1;

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(strategy);

            // Act
            await service.DeleteGameAsync(id);

            // Assert
            A.CallTo(() => _gameRepository.GetGameAsync(id)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => _gameRepository.DeleteAsync(id)).MustHaveHappened();
        }
    }
}
