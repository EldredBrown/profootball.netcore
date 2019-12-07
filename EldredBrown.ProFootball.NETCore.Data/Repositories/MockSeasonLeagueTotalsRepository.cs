using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockSeasonLeagueTotalsRepository : ISeasonLeagueTotalsRepository
    {
        private SeasonLeagueTotals _seasonLeagueTotals;

        public MockSeasonLeagueTotalsRepository()
        {
            _seasonLeagueTotals = InitializeData();
        }

        public SeasonLeagueTotals GetSeasonLeagueTotals(int seasonId, string leagueName)
        {
            return _seasonLeagueTotals;
        }

        private SeasonLeagueTotals InitializeData()
        {
            return new SeasonLeagueTotals
            {
                TotalGames = 256,
                TotalPoints = 5120
            };
        }
    }
}
