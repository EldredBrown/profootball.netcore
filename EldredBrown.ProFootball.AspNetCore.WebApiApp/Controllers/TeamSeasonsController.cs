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
    public class TeamSeasonsController : ControllerBase
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly IMapper _mapper;

        public TeamSeasonsController(ITeamSeasonRepository teamSeasonRepository, IMapper mapper)
        {
            _teamSeasonRepository = teamSeasonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TeamSeasonModel[]>> Get()
        {
            try
            {
                var teamSeasons = await _teamSeasonRepository.GetTeamSeasons();

                return _mapper.Map<TeamSeasonModel[]>(teamSeasons);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamSeasonModel>> Get(int id)
        {
            try
            {
                var teamSeason = await _teamSeasonRepository.GetTeamSeason(id);
                if (teamSeason == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TeamSeasonModel>(teamSeason);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
