using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class SubtractGameStrategy : ProcessGameStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractGameStrategy"/> class.
        /// </summary>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public SubtractGameStrategy(ITeamSeasonRepository teamSeasonRepository)
            : base(teamSeasonRepository)
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
            if (game.IsTie())
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

                teamSeason.CalculatePythagoreanWinsAndLosses();
            }
        }
    }
}
