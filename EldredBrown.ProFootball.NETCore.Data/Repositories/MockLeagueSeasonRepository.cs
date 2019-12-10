using System.Collections.Generic;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockLeagueSeasonRepository : ILeagueSeasonRepository
    {
        private IEnumerable<LeagueSeason> _leagueSeasons;

        public MockLeagueSeasonRepository()
        {
            _leagueSeasons = InitializeData();
        }

        public LeagueSeason GetLeagueSeason(int id)
        {
            return _leagueSeasons.FirstOrDefault(ls => ls.ID == id);
        }

        public LeagueSeason GetLeagueSeasonByLeagueAndSeason(string leagueName, int seasonId)
        {
            return _leagueSeasons.FirstOrDefault(ls => ls.LeagueName == leagueName && ls.SeasonId == seasonId);
        }

        public IEnumerable<LeagueSeason> GetLeagueSeasons()
        {
            return _leagueSeasons;
        }

        private IEnumerable<LeagueSeason> InitializeData()
        {
            return new List<LeagueSeason>
            {
                new LeagueSeason
                {
                    ID = 1,
                    LeagueName = "APFA",
                    SeasonId = 1920
                },
                new LeagueSeason
                {
                    ID = 2,
                    LeagueName = "APFA",
                    SeasonId = 1921
                }
            };
        }
    }
}
