using System;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Controllers
{
    /// <summary>
    /// Provides control of access to season standings data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonStandingsController : ControllerBase
    {
        private readonly ISeasonStandingsRepository _seasonStandingsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonStandingsController"/> class.
        /// </summary>
        /// <param name="seasonStandingsRepository">The repository by which season standings data will be accessed.</param>
        /// <param name="mapper">The AutoMapper object used for object-object mapping.</param>
        public SeasonStandingsController(ISeasonStandingsRepository seasonStandingsRepository, IMapper mapper)
        {
            _seasonStandingsRepository = seasonStandingsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the season standings from the data store by season year.
        /// </summary>
        /// <param name="seasonYear">The year of the season for which season standings data will be fetched.</param>
        /// <returns>A response representing the result of the operation.</returns>
        [HttpGet("{seasonYear}")]
        public ActionResult<SeasonTeamStandingModel[]> GetSeasonStandings(int seasonYear)
        {
            try
            {
                var seasonStandings = _seasonStandingsRepository.GetSeasonStandings(seasonYear);

                return _mapper.Map<SeasonTeamStandingModel[]>(seasonStandings);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}
