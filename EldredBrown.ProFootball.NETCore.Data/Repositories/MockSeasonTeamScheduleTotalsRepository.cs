using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockSeasonTeamScheduleTotalsRepository : ISeasonTeamScheduleTotalsRepository
    {
        private SeasonTeamScheduleTotals _seasonTeamScheduleTotals;

        public MockSeasonTeamScheduleTotalsRepository()
        {
            _seasonTeamScheduleTotals = InitializeData();
        }

        public SeasonTeamScheduleTotals GetSeasonTeamScheduleTotals(int seasonId, string teamName)
        {
            return _seasonTeamScheduleTotals;
        }

        private SeasonTeamScheduleTotals InitializeData()
        {
            return new SeasonTeamScheduleTotals
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
