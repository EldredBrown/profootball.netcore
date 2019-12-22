﻿using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Seasons
{
    /// <summary>
    /// Represents the model for a season list view.
    /// </summary>
    public class SeasonsIndexViewModel
    {
        /// <summary>
        /// Gets or sets the title for the current <see cref="SeasonsIndexViewModel"/> object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the collection of seasons for the current <see cref="SeasonsIndexViewModel"/> object.
        /// </summary>
        public IEnumerable<Season> Seasons { get; set; }
    }
}
