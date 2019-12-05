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
    public class SeasonTeamScheduleAveragesController : ControllerBase
    {
        private readonly ISeasonTeamScheduleAveragesRepository _seasonTeamScheduleAveragesRepository;
        private readonly IMapper _mapper;

        public SeasonTeamScheduleAveragesController(
            ISeasonTeamScheduleAveragesRepository seasonTeamScheduleAveragesRepository, IMapper mapper)
        {
            _seasonTeamScheduleAveragesRepository = seasonTeamScheduleAveragesRepository;
            _mapper = mapper;
        }

        // TODO: Figure out how to bind to multiple parameters.
        [HttpGet("{seasonId, teamName}")]
        public ActionResult<SeasonTeamScheduleAveragesModel> Get(int seasonId, string teamName)
        {
            try
            {
                var seasonTeamScheduleAverages =
                    _seasonTeamScheduleAveragesRepository.GetSeasonTeamScheduleAverages(seasonId, teamName);
                if (seasonTeamScheduleAverages == null)
                {
                    return NotFound();
                }

                return _mapper.Map<SeasonTeamScheduleAveragesModel>(seasonTeamScheduleAverages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
