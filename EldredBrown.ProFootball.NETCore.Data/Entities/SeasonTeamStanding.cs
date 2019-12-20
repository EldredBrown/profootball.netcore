using System.ComponentModel.DataAnnotations;

namespace EldredBrown.ProFootball.NETCore.Data.Entities
{
    public class SeasonTeamStanding
    {
        public string Team { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.000}")]
        public double? WinningPercentage { get; set; }

        public int PointsFor { get; set; }
        public int PointsAgainst { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? AvgPointsFor { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? AvgPointsAgainst { get; set; }
    }
}
