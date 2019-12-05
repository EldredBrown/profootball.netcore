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
    public class SeasonTeamScheduleTotalsController : ControllerBase
    {
        private readonly ISeasonTeamScheduleTotalsRepository _seasonTeamScheduleTotalsRepository;
        private readonly IMapper _mapper;

        public SeasonTeamScheduleTotalsController(
            ISeasonTeamScheduleTotalsRepository seasonTeamScheduleTotalsRepository, IMapper mapper)
        {
            _seasonTeamScheduleTotalsRepository = seasonTeamScheduleTotalsRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{seasonId, teamName}")]
        public ActionResult<SeasonTeamScheduleTotalsModel> Get(int seasonId, string teamName)
        {
            try
            {
                var seasonTeamScheduleTotals =
                    _seasonTeamScheduleTotalsRepository.GetSeasonTeamScheduleTotals(seasonId, teamName);
                if (seasonTeamScheduleTotals == null)
                {
                    return NotFound();
                }

                return _mapper.Map<SeasonTeamScheduleTotalsModel>(seasonTeamScheduleTotals);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
