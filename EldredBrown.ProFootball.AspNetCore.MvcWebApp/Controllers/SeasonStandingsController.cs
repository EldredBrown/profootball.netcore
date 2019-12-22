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

        private static int _selectedSeasonYear = 1920;
        private static bool _groupByDivision = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonStandingsController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="seasonStandingsRepository">The repository by which season standings data will be accessed.</param>
        public SeasonStandingsController(
            ISeasonRepository seasonRepository,
            ISeasonStandingsRepository seasonStandingsRepository)
        {
            _seasonRepository = seasonRepository;
            _seasonStandingsRepository = seasonStandingsRepository;
        }

        // GET: SeasonStandings
        /// <summary>
        /// Renders a view of the SeasonStandings list.
        /// </summary>
        /// <returns>The rendered view of the SeasonStandings list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.Year);

            var viewModel = new SeasonStandingsIndexViewModel
            {
                Title = "Standings",
                Seasons = new SelectList(seasons, "ID", "ID", _selectedSeasonYear),
                SeasonStandings = await _seasonStandingsRepository.GetSeasonStandings(_selectedSeasonYear)
            };

            return View(viewModel);
        }

        /// <summary>
        /// Sets the selected season ID.
        /// </summary>
        /// <param name="seasonYear">The ID of the selected season.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/>.</returns>
        public IActionResult SetSelectedSeasonYear(int? seasonYear)
        {
            if (seasonYear == null)
            {
                return BadRequest();
            }

            _selectedSeasonYear = seasonYear.Value;

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Sets the groupByDivision flag.
        /// </summary>
        /// <param name="groupByDivision">Indicates whether the groupByDivision flag should be set to true or false.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/>.</returns>
        public IActionResult SetGroupByDivision(bool? groupByDivision)
        {
            if (groupByDivision != null)
            {
                _groupByDivision = groupByDivision.Value;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
