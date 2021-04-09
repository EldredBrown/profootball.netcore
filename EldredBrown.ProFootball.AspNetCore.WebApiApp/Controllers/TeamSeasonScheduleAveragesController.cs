using System;
using System.Threading.Tasks;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Controllers
{
    /// <summary>
    /// Provides control of access to team season schedule averages data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeamSeasonScheduleAveragesController : ControllerBase
    {
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleAveragesController"/> class.
        /// </summary>
        /// <param name="teamSeasonScheduleAveragesRepository">The repository by which team season schedule averages data will be accessed.</param>
        /// <param name="mapper">The AutoMapper object used for object-object mapping.</param>
        public TeamSeasonScheduleAveragesController(
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository, IMapper mapper)
        {
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a single team season schedule averages entity from the data store by team name and season year.
        /// </summary>
        /// <param name="teamName">The name of the team for which team season schedule averages data will be fetched.</param>
        /// <param name="seasonYear">The year of the season for which team season schedule averages data will be fetched.</param>
        /// <returns>A response representing the result of the operation.</returns>
        [HttpGet("{teamName}/{seasonYear}")]
        public async Task<ActionResult<TeamSeasonScheduleAveragesModel>> GetTeamSeasonScheduleAverages(string teamName,
            int seasonYear)
        {
            try
            {
                var teamSeasonScheduleAverages =
                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear);

                if (teamSeasonScheduleAverages is null)
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
