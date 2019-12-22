namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class SeasonStandingsModel
    {
        public int SeasonYear { get; set; }
        public string Team { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public decimal? WinningPercentage { get; set; }
    }
}
