using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    /// <summary>
    /// Represents a pro football league.
    /// </summary>
    public class League
    {
        /// <summary>
        /// Gets or sets the ID of the current <see cref="League"/> entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the long name of the current <see cref="League"/> entity.
        /// </summary>
        [Required(ErrorMessage = "Please enter a long name.")]
        [StringLength(50)]
        [Display(Name = "Long Name")]
        public string LongName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the current <see cref="League"/> entity.
        /// </summary>
        [Required(ErrorMessage = "Please enter a short name.")]
        [StringLength(5)]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the year of the current <see cref="League"/> entity's first season.
        /// </summary>
        [Required(ErrorMessage = "Please enter a first season.")]
        [Display(Name = "First Season")]
        public int FirstSeasonYear { get; set; }

        /// <summary>
        /// Gets or sets the year of the current <see cref="League"/> entity's last season.
        /// </summary>
        [Display(Name = "Last Season")]
        public int? LastSeasonYear { get; set; }
    }
}
