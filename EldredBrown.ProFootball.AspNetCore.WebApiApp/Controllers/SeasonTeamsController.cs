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
    public class SeasonTeamsController : ControllerBase
    {
        private readonly ISeasonTeamRepository _seasonTeamRepository;
        private readonly IMapper _mapper;

        public SeasonTeamsController(ISeasonTeamRepository seasonTeamRepository, IMapper mapper)
        {
            _seasonTeamRepository = seasonTeamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SeasonTeamModel[]>> Get()
        {
            try
            {
                var seasonTeams = await _seasonTeamRepository.GetSeasonTeams();

                return _mapper.Map<SeasonTeamModel[]>(seasonTeams);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonTeamModel>> Get(int id)
        {
            try
            {
                var seasonTeam = await _seasonTeamRepository.GetSeasonTeam(id);
                if (seasonTeam == null)
                {
                    return NotFound();
                }

                return _mapper.Map<SeasonTeamModel>(seasonTeam);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
