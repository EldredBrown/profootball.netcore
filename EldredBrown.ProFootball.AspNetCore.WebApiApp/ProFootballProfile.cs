using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.WebApi
{
    public class ProFootballProfile : Profile
    {
        public ProFootballProfile()
        {
            CreateMap<Team, TeamModel>().ReverseMap();
            CreateMap<Season, SeasonModel>().ReverseMap();
            CreateMap<Game, GameModel>().ReverseMap();
            CreateMap<TeamSeason, TeamSeasonModel>().ReverseMap();
            CreateMap<TeamSeasonScheduleProfile, TeamSeasonScheduleProfileModel>().ReverseMap();
            CreateMap<TeamSeasonScheduleTotals, TeamSeasonScheduleTotalsModel>().ReverseMap();
            CreateMap<TeamSeasonScheduleAverages, TeamSeasonScheduleAveragesModel>().ReverseMap();
        }
    }
}
