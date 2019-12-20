using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="Game"/> object.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Game"/> object's season.
        /// </summary>
        [Required(ErrorMessage = "A season is required.")]
        [DisplayName("Season")]
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current <see cref="Game"/> object's week.
        /// </summary>
        [Required(ErrorMessage = "A week is required.")]
        [DisplayName("Week")]
        public int Week { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Game"/> object's guest.
        /// </summary>
        [Required(ErrorMessage = "A guest is required.")]
        [DisplayName("Guest")]
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="Game"/> object's guest.
        /// </summary>
        [Required(ErrorMessage = "The guest's score is required.")]
        [DisplayName("Guest Score")]
        public int GuestScore { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Game"/> object's host.
        /// </summary>
        [Required(ErrorMessage = "A host is required.")]
        [DisplayName("Host")]
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="Game"/> object's host.
        /// </summary>
        [Required(ErrorMessage = "The host's score is required.")]
        [DisplayName("Host Score")]
        public int HostScore { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Game"/> object's winner.
        /// </summary>
        [DisplayName("Winner")]
        public string WinnerName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="Game"/> object's winner.
        /// </summary>
        [DisplayName("Winner Score")]
        public int? WinnerScore { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="Game"/> object's loser.
        /// </summary>
        [DisplayName("Loser")]
        public string LoserName { get; set; }

        /// <summary>
        /// Gets or sets the points scored by the current <see cref="Game"/> object's loser.
        /// </summary>
        [DisplayName("Loser Score")]
        public int? LoserScore { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the current <see cref="Game"/> object is a playoff game.
        /// </summary>
        [DisplayName("Playoff Game?")]
        [DefaultValue(false)]
        public bool IsPlayoffGame { get; set; }

        /// <summary>
        /// Gets or sets any notes for the current <see cref="Game"/> object.
        /// </summary>
        public string Notes { get; set; }
    }
}
