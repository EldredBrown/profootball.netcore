using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockTeamSeasonScheduleProfileRepository : ITeamSeasonScheduleProfileRepository
    {
        private TeamSeasonScheduleProfile _teamSeasonScheduleProfile;

        public MockTeamSeasonScheduleProfileRepository()
        {
            _teamSeasonScheduleProfile = InitializeData();
        }

        public TeamSeasonScheduleProfile GetTeamSeasonScheduleProfile(string teamName, int seasonId)
        {
            return _teamSeasonScheduleProfile;
        }

        private TeamSeasonScheduleProfile InitializeData()
        {
            return new TeamSeasonScheduleProfile
            {
                Opponents = new List<Opponent>
                {
                    new Opponent
                    {
                        Name = "Columbia Panhandles",
                        GamePointsFor = 37,
                        GamePointsAgainst = 0,
                        Wins = 2,
                        Losses = 6,
                        Ties = 2,
                        WinningPercentage = 0.300m,
                        WeightedGames = 9,
                        WeightedPointsFor = 41,
                        WeightedPointsAgainst = 84
                    },
                    new Opponent
                    {
                        Name = "Cleveland Tigers",
                        GamePointsFor = 7,
                        GamePointsAgainst = 0,
                        Wins = 2,
                        Losses = 4,
                        Ties = 2,
                        WinningPercentage = 0.375m,
                        WeightedGames = 7,
                        WeightedPointsFor = 28,
                        WeightedPointsAgainst = 39
                    },
                    new Opponent
                    {
                        Name = "Canton Bulldogs",
                        GamePointsFor = 10,
                        GamePointsAgainst = 0,
                        Wins = 7,
                        Losses = 4,
                        Ties = 2,
                        WinningPercentage = 0.615m,
                        WeightedGames = 12,
                        WeightedPointsFor = 208,
                        WeightedPointsAgainst = 47
                    }
                }
            };
        }
    }
}
