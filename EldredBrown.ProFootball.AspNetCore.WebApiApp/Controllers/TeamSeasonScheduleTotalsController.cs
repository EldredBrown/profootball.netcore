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
    public class TeamSeasonScheduleTotalsController : ControllerBase
    {
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly IMapper _mapper;

        public TeamSeasonScheduleTotalsController(
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository, IMapper mapper)
        {
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{seasonYear, teamName}")]
        public ActionResult<TeamSeasonScheduleTotalsModel> Get(string teamName, int seasonYear)
        {
            try
            {
                var teamSeasonScheduleTotals =
                    _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear);
                if (teamSeasonScheduleTotals == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TeamSeasonScheduleTotalsModel>(teamSeasonScheduleTotals);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
