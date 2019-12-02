using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games
{
    public class GameListViewModel
    {
        public SelectList Seasons { get; set; }
        public SelectList Weeks { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
