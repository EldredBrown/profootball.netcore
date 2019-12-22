using System;
using System.Threading.Tasks;
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
    public class TeamSeasonScheduleProfileController : ControllerBase
    {
        private readonly ITeamSeasonScheduleProfileRepository _teamSeasonScheduleProfileRepository;
        private readonly IMapper _mapper;

        public TeamSeasonScheduleProfileController(
            ITeamSeasonScheduleProfileRepository teamSeasonScheduleProfileRepository, IMapper mapper)
        {
            _teamSeasonScheduleProfileRepository = teamSeasonScheduleProfileRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{teamName, seasonYear}")]
        public async Task<ActionResult<OpponentProfileModel[]>> Get(string teamName, int seasonYear)
        {
            try
            {
                var teamSeasonScheduleProfile = 
                    await _teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfile(teamName, seasonYear);
                if (teamSeasonScheduleProfile == null)
                {
                    return NotFound();
                }

                return _mapper.Map<OpponentProfileModel[]>(teamSeasonScheduleProfile);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
