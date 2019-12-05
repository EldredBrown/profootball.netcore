using System;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EldredBrown.ProFootball.AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonTeamScheduleProfileController : ControllerBase
    {
        private readonly ISeasonTeamScheduleProfileRepository _seasonTeamScheduleProfileRepository;
        private readonly IMapper _mapper;

        public SeasonTeamScheduleProfileController(
            ISeasonTeamScheduleProfileRepository seasonTeamScheduleProfileRepository, IMapper mapper)
        {
            _seasonTeamScheduleProfileRepository = seasonTeamScheduleProfileRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{seasonId, teamName}")]
        public ActionResult<SeasonTeamScheduleProfileModel> Get(int seasonId, string teamName)
        {
            try
            {
                var seasonTeamScheduleProfile = 
                    _seasonTeamScheduleProfileRepository.GetSeasonTeamScheduleProfile(seasonId, teamName);
                if (seasonTeamScheduleProfile == null)
                {
                    return NotFound();
                }

                return _mapper.Map<SeasonTeamScheduleProfileModel>(seasonTeamScheduleProfile);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
