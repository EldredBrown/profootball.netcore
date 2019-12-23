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
    /// Provides control of access to team season schedule profile data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeamSeasonScheduleProfileController : ControllerBase
    {
        private readonly ITeamSeasonScheduleProfileRepository _teamSeasonScheduleProfileRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleProfileController"/> class.
        /// </summary>
        /// <param name="teamSeasonScheduleProfileRepository">The repository by which team season schedule profile data will be accessed.</param>
        /// <param name="mapper">The AutoMapper object used for object-object mapping.</param>
        public TeamSeasonScheduleProfileController(
            ITeamSeasonScheduleProfileRepository teamSeasonScheduleProfileRepository, IMapper mapper)
        {
            _teamSeasonScheduleProfileRepository = teamSeasonScheduleProfileRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a collection of all team opponent profiles from the data store by team name and season year.
        /// </summary>
        /// <param name="teamName">The name of the team for which team season schedule profile data will be fetched.</param>
        /// <param name="seasonYear">The year of the season for which team season schedule profile data will be fetched.</param>
        /// <returns>A response representing the result of the operation.</returns>
        [HttpGet("{teamName}/{seasonYear}")]
        public async Task<ActionResult<TeamSeasonOpponentProfileModel[]>> GetTeamSeasonScheduleProfile(string teamName,
            int seasonYear)
        {
            try
            {
                var teamSeasonScheduleProfile = 
                    await _teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfile(teamName, seasonYear);
                if (teamSeasonScheduleProfile == null)
                {
                    return NotFound();
                }

                return _mapper.Map<TeamSeasonOpponentProfileModel[]>(teamSeasonScheduleProfile);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
