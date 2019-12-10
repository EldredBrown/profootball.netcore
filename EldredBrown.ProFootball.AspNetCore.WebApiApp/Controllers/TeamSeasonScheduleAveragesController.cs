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
    public class TeamSeasonScheduleAveragesController : ControllerBase
    {
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private readonly IMapper _mapper;

        public TeamSeasonScheduleAveragesController(
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository, IMapper mapper)
        {
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{seasonId, teamName}")]
        public ActionResult<TeamSeasonScheduleAveragesModel> Get(string teamName, int seasonId)
        {
            try
            {
                var teamSeasonScheduleAverages =
                    _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonId);
                if (teamSeasonScheduleAverages == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TeamSeasonScheduleAveragesModel>(teamSeasonScheduleAverages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
