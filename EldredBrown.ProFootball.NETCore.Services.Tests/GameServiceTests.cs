using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        private IGameUtility _gameUtility;
        private IGameRepository _gameRepository;
        private IProcessGameStrategyFactory _processGameStrategyFactory;

        [SetUp]
        public void Setup()
        {
            _gameUtility = A.Fake<IGameUtility>();
            _gameRepository = A.Fake<IGameRepository>();
            _processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
        }

        [Test]
        public async Task AddGame_AddsGameToRepositoryWhenNewGameIsPassed()
        {
            var service = new GameService(_gameUtility, _gameRepository, _processGameStrategyFactory);

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(strategy);

            var newGame = A.Fake<IGameDecorator>();

            await service.AddGame(newGame);

            A.CallTo(() => newGame.DecideWinnerAndLoser()).MustHaveHappened();
            A.CallTo(() => _gameRepository.Add(newGame as Game)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGame(newGame as Game)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task EditGame_EditsGameInRepositoryWhenNewGameAndOldGameArePassed()
        {
            var service = new GameService(_gameUtility, _gameRepository, _processGameStrategyFactory);

            var selectedGame = new Game();
            A.CallTo(() => _gameRepository.GetGame(A<int>.Ignored)).Returns(selectedGame);

            var downStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(downStrategy);

            var upStrategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).Returns(upStrategy);

            var newGame = A.Fake<IGameDecorator>();
            var oldGame = new Game();

            await service.EditGame(newGame, oldGame);

            A.CallTo(() => newGame.DecideWinnerAndLoser()).MustHaveHappened();
            A.CallTo(() => _gameRepository.GetGame(newGame.ID)).MustHaveHappened();
            A.CallTo(() => _gameUtility.Edit(selectedGame, newGame as Game)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Update(selectedGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => downStrategy.ProcessGame(oldGame)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
            A.CallTo(() => upStrategy.ProcessGame(newGame as Game)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeleteGame_DeletesGameFromRepositoryWhenGameIdIsPassed()
        {
            var service = new GameService(_gameUtility, _gameRepository, _processGameStrategyFactory);

            var oldGame = new Game();
            A.CallTo(() => _gameRepository.GetGame(A<int>.Ignored)).Returns(oldGame);

            var strategy = A.Fake<ProcessGameStrategyBase>();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).Returns(strategy);

            var id = 1;

            await service.DeleteGame(id);

            A.CallTo(() => _gameRepository.GetGame(id)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => strategy.ProcessGame(oldGame)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _gameRepository.Delete(id)).MustHaveHappened();
        }
    }
}
