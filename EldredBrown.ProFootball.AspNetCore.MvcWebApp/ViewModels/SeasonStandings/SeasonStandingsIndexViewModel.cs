using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonStandings
{
    /// <summary>
    /// Represents the model for a season standings view.
    /// </summary>
    public class SeasonStandingsIndexViewModel
    {
        /// <summary>
        /// Gets or sets the list that lets users select a season.
        /// </summary>
        public SelectList Seasons { get; set; }

        /// <summary>
        /// Gets or sets the year of the selected season for the current view model.
        /// </summary>
        public int SelectedSeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the collection of season standings for the current view model.
        /// </summary>
        public IEnumerable<SeasonTeamStanding> SeasonStandings { get; set; }
    }
}
