using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class MockSeasonStandingsRepository : ISeasonStandingsRepository
    {
        private IEnumerable<SeasonStanding> _seasonStandings;

        public MockSeasonStandingsRepository()
        {
            _seasonStandings = InitializeData();
        }

        public IEnumerable<SeasonStanding> GetSeasonStandings(bool groupByDivision)
        {
            return _seasonStandings;
        }

        private IEnumerable<SeasonStanding> InitializeData()
        {
            return new List<SeasonStanding>
            {
                new SeasonStanding
                {
                    SeasonId = 1920,
                    Team = "Team 1",
                    Conference = "Conference",
                    Wins = 2,
                    Losses = 0,
                    Ties = 0,
                    WinningPercentage = 1.000m,
                    PointsFor = 40,
                    PointsAgainst = 0,
                    AvgPointsFor = 20.00m,
                    AvgPointsAgainst = 0.00m
                },
                new SeasonStanding
                {
                    SeasonId = 1920,
                    Team = "Team 2",
                    Conference = "Conference",
                    Wins = 1,
                    Losses = 1,
                    Ties = 0,
                    WinningPercentage = 0.500m,
                    PointsFor = 20,
                    PointsAgainst = 20,
                    AvgPointsFor = 10.00m,
                    AvgPointsAgainst = 10.00m
                },
                new SeasonStanding
                {
                    SeasonId = 1920,
                    Team = "Team 3",
                    Conference = "Conference",
                    Wins = 0,
                    Losses = 2,
                    Ties = 0,
                    WinningPercentage = 0.000m,
                    PointsFor = 0,
                    PointsAgainst = 40,
                    AvgPointsFor = 0.00m,
                    AvgPointsAgainst = 20.00m
                }
            };
        }
    }
}
