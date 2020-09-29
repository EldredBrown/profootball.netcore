using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a season for a pro football league.
    /// </summary>
    public class LeagueSeason
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="LeagueSeason"/> entity's league.
        /// </summary>
        [DisplayName("League")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter a league name.")]
        public string LeagueName { get; set; }

        /// <summary>
        /// Gets or sets the year of the current <see cref="LeagueSeason"/> entity's season.
        /// </summary>
        [DisplayName("Season")]
        [Required(ErrorMessage = "Please enter a year.")]
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the total games of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Total Games")]
        [DefaultValue(0)]
        public int TotalGames { get; set; }

        /// <summary>
        /// Gets or sets the total points of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Total Points")]
        [DefaultValue(0)]
        public int TotalPoints { get; set; }

        /// <summary>
        /// Gets or sets the average points of the current <see cref="LeagueSeason"/> entity.
        /// </summary>
        [DisplayName("Average Points")]
        public double? AveragePoints { get; set; }

        /// <summary>
        /// Updates the games and points totals of the current <see cref="LeagueSeason"/> entity."
        /// </summary>
        /// <param name="totalGames">The value to be updated to the current <see cref="LeagueSeason"/> entity's total games.</param>
        /// <param name="totalPoints">The value to be updated to the current <see cref="LeagueSeason"/> entity's total points.</param>
        public void UpdateGamesAndPoints(int totalGames, int totalPoints)
        {
            TotalGames = totalGames;
            TotalPoints = totalPoints;

            AveragePoints = null;
            if (totalGames != 0)
            {
                AveragePoints = totalPoints / totalGames;
            }
        }
    }
}
