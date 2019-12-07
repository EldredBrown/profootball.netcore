using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockSeasonLeagueRepository : ISeasonLeagueRepository
    {
        private IEnumerable<SeasonLeague> _seasonLeagues;

        public MockSeasonLeagueRepository()
        {
            _seasonLeagues = InitializeData();
        }

        public IEnumerable<SeasonLeague> GetSeasonLeagues()
        {
            return _seasonLeagues;
        }

        private IEnumerable<SeasonLeague> InitializeData()
        {
            return new List<SeasonLeague>
            {
                new SeasonLeague
                {
                    ID = 1,
                    SeasonId = 1920,
                    LeagueName = "APFA"
                },
                new SeasonLeague
                {
                    ID = 2,
                    SeasonId = 1921,
                    LeagueName = "APFA"
                },
                new SeasonLeague
                {
                    ID = 3,
                    SeasonId = 1923,
                    LeagueName = "NFL"
                }
            };
        }
    }
}
