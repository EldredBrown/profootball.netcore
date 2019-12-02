using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.Data.Repositories
{
    public interface ISeasonStandingsRepository
    {
        IEnumerable<SeasonStanding> GetSeasonStandings(bool groupByDivision);
    }
}
