﻿namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football conference.
    /// </summary>
    public class Conference
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="Conference"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the long name of the current <see cref="Conference"/> entity.
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the current <see cref="Conference"/> entity.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Conference"/> entity's league.
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Conference"/> entity's first season.
        /// </summary>
        public int FirstSeasonId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Conference"/> entity's last season.
        /// </summary>
        public int? LastSeasonId { get; set; }
    }
}
