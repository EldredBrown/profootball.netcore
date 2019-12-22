using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class GameModel
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="GameModel"/> object's season.
        /// </summary>
        [Required]
        public int SeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="GameModel"/> object's week.
        /// </summary>
        [Required]
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="GameModel"/> object's guest.
        /// </summary>
        [Required]
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="GameModel"/> object's guest.
        /// </summary>
        [Required]
        public int GuestScore { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="GameModel"/> object's host.
        /// </summary>
        [Required]
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="GameModel"/> object's host.
        /// </summary>
        [Required]
        public int HostScore { get; set; }
    }
}
