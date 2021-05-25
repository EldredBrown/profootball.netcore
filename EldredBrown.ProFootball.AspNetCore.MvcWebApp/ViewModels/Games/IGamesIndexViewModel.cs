using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games
{
    public interface IGamesIndexViewModel
    {
        IEnumerable<Game> Games { get; set; }
        SelectList Seasons { get; set; }
        int SelectedSeasonYear { get; set; }
        int? SelectedWeek { get; set; }
        SelectList Weeks { get; set; }
    }
}
