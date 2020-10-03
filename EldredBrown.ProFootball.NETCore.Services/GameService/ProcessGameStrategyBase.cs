using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class ProcessGameStrategyBase
    {
        protected readonly ITeamSeasonRepository _teamSeasonRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessGameStrategyBase"/> class.
        /// </summary>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public ProcessGameStrategyBase(ITeamSeasonRepository teamSeasonRepository)
        {
            _teamSeasonRepository = teamSeasonRepository;
        }

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
                guestSeason.CalculateWinningPercentage();
            }

            if (hostSeason != null)
            {
                hostSeason.CalculateWinningPercentage();
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
