using System;
using System.Threading.Tasks;
using FakeItEasy;
using Shouldly;
using Xunit;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services.Exceptions;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    public class GameServiceTest
    {
        [Fact]
        public void AddGame_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            Game? newGame = null;

            // Act
            var action = new Action(() => service.AddGame(newGame!));

            // Assert
            var ex = action.ShouldThrow<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.AddGame: newGame");
        }

        [Fact]
        public void AddGame_WhenNewGameIsPassed_ShouldAddGameToRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(strategy);

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();

            // Act
            service.AddGame(newGame);

            // Assert
            A.CallTo(() => gameRepository.Add(newGame)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddGameAsync_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            Game? newGame = null;

            // Act
            var func = new Func<Task>(async () => await service.AddGameAsync(newGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.AddGameAsync: newGame");
        }

        [Fact]
        public async Task AddGameAsync_WhenNewGameIsPassed_ShouldAddGameToRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();

            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(strategy);

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();

            // Act
            await service.AddGameAsync(newGame);

            // Assert
            A.CallTo(() => gameRepository.AddAsync(newGame)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void EditGame_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            Game? newGame = null;
            Game? oldGame = null;

            // Act
            var action = new Action(() => service.EditGame(newGame!, oldGame!));

            // Assert
            var ex = action.ShouldThrow<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGame: newGame");
        }

        [Fact]
        public void EditGame_WhenOldGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            Game? oldGame = null;

            // Act
            var func = new Action(() => service.EditGame(newGame, oldGame!));

            // Assert
            var ex = func.ShouldThrow<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGame: oldGame");
        }

        [Fact]
        public void EditGame_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            A.CallTo(() => gameRepository.GetGame(A<int>.Ignored)).Returns<Game?>(null);

            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            // Act
            var func = new Action(() => service.EditGame(newGame, oldGame));

            // Assert
            var ex = func.ShouldThrow<EntityNotFoundException>();
            ex.Message.ShouldBe<string>($"{service.GetType()}.EditGame: The selected Game entity could not be found.");
        }

        [Fact]
        public void EditGame_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsFound_ShouldEditGameInRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var selectedGame = new Game();
            A.CallTo(() => gameRepository.GetGame(A<int>.Ignored)).Returns(selectedGame);

            var sharedRepository = A.Fake<ISharedRepository>();

            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var downStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(downStrategy);
            var upStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(upStrategy);

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            // Act
            service.EditGame(newGame, oldGame);

            // Assert
            A.CallTo(() => gameRepository.GetGame(newGame.ID)).MustHaveHappened();
            A.CallTo(() => gameRepository.Update(selectedGame)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => downStrategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => upStrategy.ProcessGame(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditGameAsync_WhenNewGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            Game? newGame = null;
            Game? oldGame = null;

            // Act
            var func = new Func<Task>(async () => await service.EditGameAsync(newGame!, oldGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGameAsync: newGame");
        }

        [Fact]
        public async Task EditGameAsync_WhenOldGameArgIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            Game? oldGame = null;

            // Act
            var func = new Func<Task>(async () => await service.EditGameAsync(newGame, oldGame!));

            // Assert
            var ex = await func.ShouldThrowAsync<ArgumentNullException>();
            ex.ParamName.ShouldBe<string>($"{service.GetType()}.EditGameAsync: oldGame");
        }

        [Fact]
        public async Task EditGameAsync_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            A.CallTo(() => gameRepository.GetGameAsync(A<int>.Ignored)).Returns<Game?>(null);

            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            // Act
            var func = new Func<Task>(async () => await service.EditGameAsync(newGame, oldGame));

            // Assert
            var ex = await func.ShouldThrowAsync<EntityNotFoundException>();
            ex.Message.ShouldBe<string>($"{service.GetType()}.EditGameAsync: The selected Game entity could not be found.");
        }

        [Fact]
        public async Task EditGameAsync_WhenNewGameAndOldGameArgsAreNotNullAndSelectedGameIsFound_ShouldEditGameInRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var selectedGame = new Game();
            A.CallTo(() => gameRepository.GetGameAsync(A<int>.Ignored)).Returns(selectedGame);

            var sharedRepository = A.Fake<ISharedRepository>();

            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var downStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(downStrategy);
            var upStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(upStrategy);

            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();

            // Act
            await service.EditGameAsync(newGame, oldGame);

            // Assert
            A.CallTo(() => gameRepository.GetGameAsync(newGame.ID)).MustHaveHappened();
            A.CallTo(() => gameRepository.Update(selectedGame)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => downStrategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => upStrategy.ProcessGameAsync(A<IGameDecorator>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void DeleteGame_WhenGameWithIdIsNotFoundInRepository_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var id = 1;

            A.CallTo(() => gameRepository.GetGame(id)).Returns<Game?>(null);

            // Act
            var action = new Action(() => service.DeleteGame(id));

            // Assert
            var ex = action.ShouldThrow<EntityNotFoundException>();
            ex.Message.ShouldBe<string>(
                $"{service.GetType()}.DeleteGame: A Game entity with ID={id} could not be found.");
        }

        [Fact]
        public void DeleteGame_WhenGameWithIdIsFoundInRepository_ShouldDeleteGameFromRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var id = 1;

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(strategy);

            // Act
            service.DeleteGame(id);

            // Assert
            A.CallTo(() => gameRepository.GetGame(id)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => gameRepository.Delete(id)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChanges()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteGameAsync_WhenGameWithIdIsNotFoundInRepository_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var id = 1;

            A.CallTo(() => gameRepository.GetGameAsync(id)).Returns<Game?>(null);

            // Act
            var func = new Func<Task>(async () => await service.DeleteGameAsync(id));

            // Assert
            var ex = await func.ShouldThrowAsync<EntityNotFoundException>();
            ex.Message.ShouldBe<string>(
                $"{service.GetType()}.DeleteGameAsync: A Game entity with ID={id} could not be found.");
        }

        [Fact]
        public async Task DeleteGameAsync_WhenGameWithIdIsFoundInRepository_ShouldDeleteGameFromRepository()
        {
            // Arrange
            var gameRepository = A.Fake<IGameRepository>();
            var sharedRepository = A.Fake<ISharedRepository>();
            var processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
            var service = new GameService(gameRepository, sharedRepository, processGameStrategyFactory);

            var id = 1;

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(strategy);

            // Act
            await service.DeleteGameAsync(id);

            // Assert
            A.CallTo(() => gameRepository.GetGameAsync(id)).MustHaveHappened();
            A.CallTo(() => processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => gameRepository.DeleteAsync(id)).MustHaveHappened();
            A.CallTo(() => sharedRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
