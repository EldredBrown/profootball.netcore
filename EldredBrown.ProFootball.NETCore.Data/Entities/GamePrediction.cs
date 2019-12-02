using System.ComponentModel;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public class GamePrediction
    {
        [DisplayName("Guest Season")]
        public int GuestSeasonId { get; set; }

        [DisplayName("Guest Name")]
        public string GuestName { get; set; }

        [DisplayName("Guest Score")]
        public int GuestScore { get; set; }

        [DisplayName("Host Season")]
        public int HostSeasonId { get; set; }

        [DisplayName("Host Name")]
        public string HostName { get; set; }

        [DisplayName("Host Score")]
        public int HostScore { get; set; }
    }
}
