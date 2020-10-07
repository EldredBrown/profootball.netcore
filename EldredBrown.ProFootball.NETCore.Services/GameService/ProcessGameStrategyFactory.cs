using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class ProcessGameStrategyFactory : IProcessGameStrategyFactory
    {
        private readonly IGameUtility _gameUtility;
        private readonly ITeamSeasonUtility _teamSeasonUtility;
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameStrategyFactory"/> class.
        /// </summary>
        /// <param name="gameUtility">The utility by which Game entity data will be accessed.</param>
        /// <param name="teamSeasonUtility">The utility by which TeamSeason entity data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public ProcessGameStrategyFactory(IGameUtility gameUtility, ITeamSeasonUtility teamSeasonUtility,
            ITeamSeasonRepository teamSeasonRepository)
        {
            _gameUtility = gameUtility;
            _teamSeasonUtility = teamSeasonUtility;
            _teamSeasonRepository = teamSeasonRepository;
        }

        /// <summary>
        /// Creates an instance of the <see cref="ProcessGameStrategyBase"/> class.
        /// </summary>
        /// <param name="direction">The <see cref="Direction"/> value used to determine which type of <see cref="ProcessGameStrategyBase"/> to create.</param>
        /// <returns>A <see cref="ProcessGameStrategyBase"/> object corresponding to the specified <see cref="Direction"/> value.</returns>
        public ProcessGameStrategyBase CreateStrategy(Direction direction)
        {
            ProcessGameStrategyBase processGameStrategy = direction switch
            {
                Direction.Up => new AddGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository),
                Direction.Down => new SubtractGameStrategy(_gameUtility, _teamSeasonUtility, _teamSeasonRepository),
                _ => NullGameStrategy.Instance
            };

            return processGameStrategy;
        }
    }
}
