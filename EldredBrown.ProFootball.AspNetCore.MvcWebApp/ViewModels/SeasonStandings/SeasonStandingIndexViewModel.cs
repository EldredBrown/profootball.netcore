using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonStandings
{
    public class SeasonStandingIndexViewModel
    {
        public SelectList Seasons { get; set; }
        public IEnumerable<SeasonStanding> SeasonStandings { get; set; }
        public bool GroupByDivision { get; set; }
    }
}
