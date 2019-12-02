using System;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonStandings;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of season standings data.
    /// </summary>
    public class SeasonStandingsController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ISeasonStandingsRepository _seasonStandingsRepository;

        private static int _selectedSeasonId = DateTime.Now.Year;
        private static bool _groupByDivision = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonStandingsController"/> class.
        /// </summary>
        /// <param name="seasonRepository"></param>
        /// <param name="seasonStandingsRepository"></param>
        public SeasonStandingsController(ISeasonRepository seasonRepository,
            ISeasonStandingsRepository seasonStandingsRepository)
        {
            _seasonRepository = seasonRepository;
            _seasonStandingsRepository = seasonStandingsRepository;
        }

        /// <summary>
        /// Renders a view of the season stadings home page.
        /// </summary>
        /// <returns>The rendered view of the season standings home page.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);
            var seasonStandings = _seasonStandingsRepository.GetSeasonStandings(_groupByDivision);

            var viewModel = new SeasonStandingIndexViewModel
            {
                Seasons = new SelectList(seasons, "ID", "ID", _selectedSeasonId),
                SeasonStandings = seasonStandings,
                GroupByDivision = _groupByDivision
            };

            return View(viewModel);
        }

        /// <summary>
        /// Sets the selected season ID.
        /// </summary>
        /// <param name="seasonId">The ID of the selected season.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        public IActionResult SetSelectedSeasonId(int? seasonId)
        {
            if (!seasonId.HasValue)
            {
                return BadRequest();
            }

            _selectedSeasonId = seasonId.Value;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sets the groupByDivision flag
        /// </summary>
        /// <param name="groupByDivision">Indicates whether the groupByDivision flag should be set to true or false.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        public IActionResult SetGroupByDivision(bool? groupByDivision)
        {
            if (groupByDivision.HasValue)
            {
                _groupByDivision = groupByDivision.Value;
            }

            return RedirectToAction("Index");
        }
    }
}
