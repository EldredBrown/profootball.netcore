﻿using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class ProcessGameStrategyFactory : IProcessGameStrategyFactory
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameStrategyFactory"/> class.
        /// </summary>
        /// <param name="teamSeasonRepository">The repository by which teamSeason data will be accessed.</param>
        public ProcessGameStrategyFactory(ITeamSeasonRepository teamSeasonRepository)
        {
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
                Direction.Up => new AddGameStrategy(_teamSeasonRepository),
                Direction.Down => new SubtractGameStrategy(_teamSeasonRepository),
                _ => NullGameStrategy.Instance
            };

            return processGameStrategy;
        }
    }
}
