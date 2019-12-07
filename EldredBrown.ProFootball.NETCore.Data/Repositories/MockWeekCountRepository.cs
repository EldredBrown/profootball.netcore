using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockWeekCountRepository : IWeekCountRepository
    {
        private WeekCount _weekCount;

        public MockWeekCountRepository()
        {
            _weekCount = InitializeData();
        }

        public WeekCount GetWeekCount(int seasonId)
        {
            return _weekCount;
        }

        private WeekCount InitializeData()
        {
            return new WeekCount
            {
                SeasonId = 1920,
                Count = 10
            };
        }
    }
}
