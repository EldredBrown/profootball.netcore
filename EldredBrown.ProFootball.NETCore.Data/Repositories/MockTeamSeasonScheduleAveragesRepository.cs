using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockTeamSeasonScheduleAveragesRepository : ITeamSeasonScheduleAveragesRepository
    {
        private TeamSeasonScheduleAverages _teamSeasonScheduleAverages;

        public MockTeamSeasonScheduleAveragesRepository()
        {
            _teamSeasonScheduleAverages = InitializeData();
        }

        public TeamSeasonScheduleAverages GetTeamSeasonScheduleAverages(string teamName, int seasonId)
        {
            return _teamSeasonScheduleAverages;
        }

        private TeamSeasonScheduleAverages InitializeData()
        {
            return new TeamSeasonScheduleAverages
            {
                PointsFor = 18.00m,
                PointsAgainst = 0.00m,
                SchedulePointsFor = 9.89m,
                SchedulePointsAgainst = 6.07m
            };
        }
    }
}
