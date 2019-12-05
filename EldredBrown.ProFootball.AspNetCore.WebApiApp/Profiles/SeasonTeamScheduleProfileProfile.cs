using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.WebApi.Profiles
{
    public class SeasonTeamScheduleProfileProfile : Profile
    {
        public SeasonTeamScheduleProfileProfile()
        {
            CreateMap<SeasonTeamScheduleProfile, SeasonTeamScheduleProfileModel>().ReverseMap();
        }
    }
}
