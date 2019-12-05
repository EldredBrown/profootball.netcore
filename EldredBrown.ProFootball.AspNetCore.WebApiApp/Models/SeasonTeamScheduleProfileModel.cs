using System.Collections.Generic;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Models
{
    public class SeasonTeamScheduleProfileModel
    {
        public IEnumerable<Opponent> Opponents { get; set; }
    }
}
