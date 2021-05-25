using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.TeamSeasons
{
    public interface ITeamSeasonsIndexViewModel
    {
        SelectList Seasons { get; set; }
        int SelectedSeasonYear { get; set; }
        IEnumerable<TeamSeason> TeamSeasons { get; set; }
    }
}
