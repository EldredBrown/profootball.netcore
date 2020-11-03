using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class ProcessGameStrategyBase
    {
        protected readonly IGameUtility _gameUtility;
        protected readonly ITeamSeasonUtility _teamSeasonUtility;
        protected readonly ITeamSeasonRepository _teamSeasonRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameStrategyBase"/> class.
        /// </summary>
        /// <param name="gameUtility">The utility by which Game entity data will be accessed.</param>
        /// <param name="teamSeasonUtility">The utility by which TeamSeason entity data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public ProcessGameStrategyBase(IGameUtility gameUtility, ITeamSeasonUtility teamSeasonUtility,
            ITeamSeasonRepository teamSeasonRepository)
        {
            _gameUtility = gameUtility;
            _teamSeasonUtility = teamSeasonUtility;
            _teamSeasonRepository = teamSeasonRepository;
        }

        /// <summary>
        /// Processes a <see cref="Game"/> entity into the Teams data store.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public virtual async Task ProcessGame(Game game)
        {
            var seasonYear = game.SeasonYear;
            var guestSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear);
            var hostSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear);

            await EditWinLossData(guestSeason, hostSeason, game);
            EditScoringData(guestSeason, hostSeason, game.GuestScore, game.HostScore);
        }

        protected async Task EditWinLossData(TeamSeason guestSeason, TeamSeason hostSeason, Game game)
        {
            UpdateGamesForTeamSeasons(guestSeason, hostSeason);
            await UpdateWinsLossesAndTiesForTeamSeasons(guestSeason, hostSeason, game);
            UpdateWinningPercentageForTeamSeasons(guestSeason, hostSeason);
        }

        protected virtual void UpdateGamesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason)
        {
            throw new NotImplementedException(
                nameof(UpdateGamesForTeamSeasons) + " must be implemented in a subclass.");
        }

        protected virtual Task UpdateWinsLossesAndTiesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason,
            Game game)
        {
            throw new NotImplementedException(
                nameof(UpdateWinsLossesAndTiesForTeamSeasons) + " must be implemented in a subclass.");
        }

        protected void UpdateWinningPercentageForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason)
        {
            if (guestSeason != null)
            {
                _teamSeasonUtility.CalculateWinningPercentage(guestSeason);
            }

            if (hostSeason != null)
            {
                _teamSeasonUtility.CalculateWinningPercentage(hostSeason);
            }
        }

        protected void EditScoringData(TeamSeason guestSeason, TeamSeason hostSeason, int guestScore,
            int hostScore)
        {
            EditScoringDataForTeamSeason(guestSeason, guestScore, hostScore);
            EditScoringDataForTeamSeason(hostSeason, hostScore, guestScore);
        }

        protected virtual void EditScoringDataForTeamSeason(TeamSeason teamSeason, int teamScore, int opponentScore)
        {
            throw new NotImplementedException(
                nameof(EditScoringDataForTeamSeason) + " must be implemented in a subclass.");
        }
    }
}
