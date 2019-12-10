using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockTeamSeasonScheduleTotalsRepository : ITeamSeasonScheduleTotalsRepository
    {
        private TeamSeasonScheduleTotals _teamSeasonScheduleTotals;

        public MockTeamSeasonScheduleTotalsRepository()
        {
            _teamSeasonScheduleTotals = InitializeData();
        }

        public TeamSeasonScheduleTotals GetTeamSeasonScheduleTotals(string teamName, int seasonId)
        {
            return _teamSeasonScheduleTotals;
        }

        private TeamSeasonScheduleTotals InitializeData()
        {
            return new TeamSeasonScheduleTotals
            {
                Games = 3,
                PointsFor = 54,
                PointsAgainst = 0,
                ScheduleWins = 11,
                ScheduleLosses = 14,
                ScheduleTies = 6,
                ScheduleWinningPercentage = 0.452m,
                ScheduleGames = 28,
                SchedulePointsFor = 277,
                SchedulePointsAgainst = 170
            };
        }
    }
}
