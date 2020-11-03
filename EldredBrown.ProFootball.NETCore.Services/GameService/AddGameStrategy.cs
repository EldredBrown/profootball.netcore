using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class AddGameStrategy : ProcessGameStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddGameStrategy"/> class.
        /// </summary>
        /// <param name="gameUtility">The utility by which Game entity data will be accessed.</param>
        /// <param name="teamSeasonUtility">The utility by which TeamSeason entity data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public AddGameStrategy(IGameUtility gameUtility, ITeamSeasonUtility teamSeasonUtility,
            ITeamSeasonRepository teamSeasonRepository)
            : base(gameUtility, teamSeasonUtility, teamSeasonRepository)
        {}

        protected override void UpdateGamesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason)
        {
            if (guestSeason != null)
            {
                guestSeason.Games++;
            }

            if (hostSeason != null)
            {
                hostSeason.Games++;
            }
        }

        protected override async Task UpdateWinsLossesAndTiesForTeamSeasons(TeamSeason guestSeason,
            TeamSeason hostSeason, Game game)
        {
            if (_gameUtility.IsTie(game))
            {
                if (guestSeason != null)
                {
                    guestSeason.Ties++;
                }

                if (hostSeason != null)
                {
                    hostSeason.Ties++;
                }
            }
            else
            {
                var seasonYear = game.SeasonYear;

                var winnerSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.WinnerName, seasonYear);
                if (winnerSeason != null)
                {
                    winnerSeason.Wins++;
                }

                var loserSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.LoserName, seasonYear);
                if (loserSeason != null)
                {
                    loserSeason.Losses++;
                }
            }
        }

        protected override void EditScoringDataForTeamSeason(TeamSeason teamSeason, int teamScore, int opponentScore)
        {
            if (teamSeason != null)
            {
                teamSeason.PointsFor += teamScore;
                teamSeason.PointsAgainst += opponentScore;

                _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);
            }
        }
    }
}
