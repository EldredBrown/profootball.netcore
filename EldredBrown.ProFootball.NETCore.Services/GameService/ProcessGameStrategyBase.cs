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
            await EditWinLossData(game);
            await EditScoringData(game);
        }

        protected async Task EditScoringData(Game game)
        {
            await EditScoringDataForTeamSeason(game.GuestName, game.SeasonYear, game.GuestScore, game.HostScore);
            await EditScoringDataForTeamSeason(game.HostName, game.SeasonYear, game.HostScore, game.GuestScore);
        }

        protected virtual Task EditScoringDataForTeamSeason(string teamName, int seasonYear, int teamScore,
            int opponentScore)
        {
            throw new NotImplementedException(
                nameof(EditScoringDataForTeamSeason) + " must be implemented in a subclass.");
        }

        protected async Task EditWinLossData(Game game)
        {
            var seasonYear = game.SeasonYear;

            // Update games for the guest's season and the host's season.
            var guestSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear);
            var hostSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear);

            UpdateGamesForTeamSeasons(guestSeason, hostSeason);
            await UpdateWinsLossesAndTiesForTeamSeasons(guestSeason, hostSeason, game, seasonYear);
            UpdateWinningPercentageForTeamSeasons(guestSeason, hostSeason);
        }

        protected virtual void UpdateGamesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason)
        {
            throw new NotImplementedException(
                nameof(UpdateGamesForTeamSeasons) + " must be implemented in a subclass.");
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

        protected virtual Task UpdateWinsLossesAndTiesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason,
            Game game, int seasonYear)
        {
            throw new NotImplementedException(
                nameof(UpdateWinsLossesAndTiesForTeamSeasons) + " must be implemented in a subclass.");
        }
    }
}
