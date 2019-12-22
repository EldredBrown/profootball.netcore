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
    public class SeasonStandingsController : ControllerBase
    {
        private readonly ISeasonStandingsRepository _seasonStandingsRepository;
        private readonly IMapper _mapper;

        public SeasonStandingsController(ISeasonStandingsRepository seasonStandingsRepository, IMapper mapper)
        {
            _seasonStandingsRepository = seasonStandingsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<SeasonStandingsModel[]> Get(int seasonYear)
        {
            try
            {
                var teamSeasons = _seasonStandingsRepository.GetSeasonStandings(seasonYear);

                return _mapper.Map<SeasonStandingsModel[]>(teamSeasons);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
