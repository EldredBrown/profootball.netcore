using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockSeasonTeamScheduleAveragesRepository : ISeasonTeamScheduleAveragesRepository
    {
        private SeasonTeamScheduleAverages _seasonTeamScheduleAverages;

        public MockSeasonTeamScheduleAveragesRepository()
        {
            _seasonTeamScheduleAverages = InitializeData();
        }

        public SeasonTeamScheduleAverages GetSeasonTeamScheduleAverages(int seasonId, string teamName)
        {
            return _seasonTeamScheduleAverages;
        }

        private SeasonTeamScheduleAverages InitializeData()
        {
            return new SeasonTeamScheduleAverages
            {
                PointsFor = 18.00m,
                PointsAgainst = 0.00m,
                SchedulePointsFor = 9.89m,
                SchedulePointsAgainst = 6.07m
            };
        }
    }
}
