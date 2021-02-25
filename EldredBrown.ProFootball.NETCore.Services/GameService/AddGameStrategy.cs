using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class AddGameStrategy : ProcessGameStrategyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddGameStrategy"/> class.
        /// </summary>
        /// <param name="teamSeasonUtility">The utility by which TeamSeason entity data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public AddGameStrategy(ITeamSeasonRepository teamSeasonRepository)
            : base(teamSeasonRepository)
        {}

        protected override void UpdateGamesForTeamSeasons(TeamSeasonDecorator guestSeasonDecorator,
            TeamSeasonDecorator hostSeasonDecorator)
        {
            if (guestSeasonDecorator != null)
            {
                guestSeasonDecorator.Games++;
            }

            if (hostSeasonDecorator != null)
            {
                hostSeasonDecorator.Games++;
            }
        }

        protected override async Task UpdateWinsLossesAndTiesForTeamSeasons(TeamSeasonDecorator guestSeasonDecorator,
            TeamSeasonDecorator hostSeasonDecorator, IGameDecorator gameDecorator)
        {
            if (gameDecorator.IsTie())
            {
                if (guestSeasonDecorator != null)
                {
                    guestSeasonDecorator.Ties++;
                }

                if (hostSeasonDecorator != null)
                {
                    hostSeasonDecorator.Ties++;
                }
            }
            else
            {
                var seasonYear = gameDecorator.SeasonYear;

                var winnerSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.WinnerName, seasonYear);
                if (winnerSeason != null)
                {
                    winnerSeason.Wins++;
                }

                var loserSeason =
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(gameDecorator.LoserName, seasonYear);
                if (loserSeason != null)
                {
                    loserSeason.Losses++;
                }
            }
        }

        protected override void EditScoringDataForTeamSeason(TeamSeasonDecorator teamSeasonDecorator, int teamScore,
            int opponentScore)
        {
            if (teamSeasonDecorator == null)
            {
                return;
            }

            teamSeasonDecorator.PointsFor += teamScore;
            teamSeasonDecorator.PointsAgainst += opponentScore;
            teamSeasonDecorator.CalculatePythagoreanWinsAndLosses();
        }
    }
}
