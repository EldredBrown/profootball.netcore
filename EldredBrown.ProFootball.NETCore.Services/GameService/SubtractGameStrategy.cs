using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Data.Utilities;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class SubtractGameStrategy : ProcessGameStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractGameStrategy"/> class.
        /// </summary>
        /// <param name="gameUtility">The <see cref="IGameUtility"/> object that will modify <see cref="Game"/> entity data.</param>
        /// <param name="teamSeasonUtility">The <see cref="ITeamSeasonUtility"/> object that will modify <see cref="TeamSeason"/> entity data.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public SubtractGameStrategy(IGameUtility gameUtility, ITeamSeasonUtility teamSeasonUtility,
            ITeamSeasonRepository teamSeasonRepository)
            : base(gameUtility, teamSeasonUtility, teamSeasonRepository)
        {
        }

        protected override void UpdateGamesForTeamSeasons(TeamSeason guestSeason, TeamSeason hostSeason)
        {
            if (guestSeason != null)
            {
                guestSeason.Games--;
            }

            if (hostSeason != null)
            {
                hostSeason.Games--;
            }
        }

        protected override async Task UpdateWinsLossesAndTiesForTeamSeasons(TeamSeason guestSeason,
            TeamSeason hostSeason, Game game)
        {
            if (_gameUtility.IsTie(game))
            {
                if (guestSeason != null)
                {
                    guestSeason.Ties--;
                }

                if (hostSeason != null)
                {
                    hostSeason.Ties--;
                }
            }
            else
            {
                var seasonYear = game.SeasonYear;

                var winnerSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.WinnerName, seasonYear);
                if (winnerSeason != null)
                {
                    winnerSeason.Wins--;
                }

                var loserSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.LoserName, seasonYear);
                if (loserSeason != null)
                {
                    loserSeason.Losses--;
                }
            }
        }

        protected override void EditScoringDataForTeamSeason(TeamSeason teamSeason, int teamScore, int opponentScore)
        {
            if (teamSeason != null)
            {
                teamSeason.PointsFor -= teamScore;
                teamSeason.PointsAgainst -= opponentScore;

                _teamSeasonUtility.CalculatePythagoreanWinsAndLosses(teamSeason);
            }
        }
    }
}
