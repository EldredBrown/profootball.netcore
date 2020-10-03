using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        private IGameRepository _gameRepository;
        private IProcessGameStrategyFactory _processGameStrategyFactory;

        [SetUp]
        public void Setup()
        {
            _gameRepository = A.Fake<IGameRepository>();
            _processGameStrategyFactory = A.Fake<IProcessGameStrategyFactory>();
        }

        [Test]
        public async Task AddGame_AddsGameToRepositoryWhenNewGameIsPassed()
        {
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();
            await service.AddGame(newGame);

            A.CallTo(() => _gameRepository.Add(newGame)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
        }

        [Test]
        public async Task EditGame_EditsGameInRepositoryWhenNewGameAndOldGameArePassed()
        {
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var newGame = new Game();
            var oldGame = new Game();
            await service.EditGame(newGame, oldGame);

            A.CallTo(() => _gameRepository.GetGame(newGame.ID)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Update(A<Game>.Ignored)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Up)).MustHaveHappened();
        }

        [Test]
        public async Task DeleteGame_DeletesGameFromRepositoryWhenGameIdIsPassed()
        {
            var service = new GameService(_gameRepository, _processGameStrategyFactory);

            var id = 1;
            await service.DeleteGame(1);

            A.CallTo(() => _gameRepository.GetGame(id)).MustHaveHappened();
            A.CallTo(() => _processGameStrategyFactory.CreateStrategy(Direction.Down)).MustHaveHappened();
            A.CallTo(() => _gameRepository.Delete(id)).MustHaveHappened();
        }
    }
}
