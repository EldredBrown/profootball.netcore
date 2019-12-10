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
        /// Gets or sets the title for the current view model.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the list that lets users select a season.
        /// </summary>
        public SelectList Seasons { get; set; }

        /// <summary>
        /// Gets or sets the collection of season standings for the current view model.
        /// </summary>
        public IEnumerable<SeasonStanding> SeasonStandings { get; set; }

        /// <summary>
        /// Gets or sets the flag that indicates whether the standings are to be sorted by division.
        /// </summary>
        public bool GroupByDivision { get; set; }
    }
}
