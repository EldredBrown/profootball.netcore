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
    /// Provides control of access to team season schedule totals data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeamSeasonScheduleTotalsController : ControllerBase
    {
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonScheduleTotalsController"/> class.
        /// </summary>
        /// <param name="teamSeasonScheduleTotalsRepository">The repository by which team season schedule totals data will be accessed.</param>
        /// <param name="mapper">The AutoMapper object used for object-object mapping.</param>
        public TeamSeasonScheduleTotalsController(
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository, IMapper mapper)
        {
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a single team season schedule totals entity from the data store by team name and season year.
        /// </summary>
        /// <param name="teamName">The name of the team for which team season schedule totals data will be fetched.</param>
        /// <param name="seasonYear">The year of the season for which team season schedule totals data will be fetched.</param>
        /// <returns>A response representing the result of the operation.</returns>
        [HttpGet("{teamName}/{seasonYear}")]
        public async Task<ActionResult<TeamSeasonScheduleTotalsModel>> GetTeamSeasonScheduleTotals(string teamName,
            int seasonYear)
        {
            try
            {
                var teamSeasonScheduleTotals =
                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear);

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
