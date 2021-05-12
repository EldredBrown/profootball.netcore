using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonStandings
{
    public interface ISeasonStandingsIndexViewModel
    {
        SelectList? Seasons { get; set; }
        int SelectedSeasonYear { get; set; }
        IEnumerable<SeasonTeamStanding>? SeasonStandings { get; set; }
    }
}