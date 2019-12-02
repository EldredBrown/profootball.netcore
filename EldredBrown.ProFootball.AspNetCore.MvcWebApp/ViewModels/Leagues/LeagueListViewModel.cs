using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Leagues
{
    public class LeagueListViewModel
    {
        public string Title { get; set; }
        public IEnumerable<League> Leagues { get; set; }
    }
}
