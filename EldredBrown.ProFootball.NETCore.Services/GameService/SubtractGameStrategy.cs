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

        protected override async Task EditScoringDataForTeamSeason(string teamName, int seasonYear, int teamScore,
            int opponentScore)
        {
            var teamSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(teamName, seasonYear);
            if (teamSeason != null)
            {
                teamSeason.PointsFor -= teamScore;
                teamSeason.PointsAgainst -= opponentScore;

                teamSeason.CalculatePythagoreanWinsAndLosses();
            }
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
            TeamSeason hostSeason, Game game, int seasonYear)
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
    }
}
