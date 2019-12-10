using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class TeamSeasonModel
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="TeamSeasonModel"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeasonModel"/> object's team.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="TeamSeasonModel"/> object's season.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="TeamSeasonModel"/> object's league.
        /// </summary>
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the number of games played by the current <see cref="TeamSeasonModel"/>.
        /// </summary>
        public int Games { get; set; }

        /// <summary>
        /// Gets or sets the number of games won by the current <see cref="TeamSeasonModel"/>.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the number of games lost by the current <see cref="TeamSeasonModel"/>.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Gets or sets the number of games tied by the current <see cref="TeamSeasonModel"/>.
        /// </summary>
        public int Ties { get; set; }

        /// <summary>
        /// Gets or sets the winning percentage of the current <see cref="TeamSeasonModel"/>.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public decimal? WinningPercentage { get; set; }
    }
}
