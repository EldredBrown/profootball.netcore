namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public class SeasonStanding
    {
        public int SeasonId { get; set; }
        public string Team { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public decimal? WinningPercentage { get; set; }
        public int PointsFor { get; set; }
        public int PointsAgainst { get; set; }
        public decimal? AvgPointsFor { get; set; }
        public decimal? AvgPointsAgainst { get; set; }
    }
}
