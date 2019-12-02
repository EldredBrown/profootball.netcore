using System;
using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public class SeasonStandingsRepository : ISeasonStandingsRepository
    {
        public IEnumerable<SeasonStanding> GetSeasonStandings(bool groupByDivision)
        {
            // TODO: 2019-11-30 - Implement a stored procedure call.
            throw new NotImplementedException();
        }
    }
}
