using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.AspNetCore.WebApi.Profiles
{
    public class SeasonTeamProfile : Profile
    {
        public SeasonTeamProfile()
        {
            CreateMap<SeasonTeam, SeasonTeamModel>().ReverseMap();
        }
    }
}
