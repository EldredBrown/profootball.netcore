using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.TeamSeasons;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    public class TeamSeasonsController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly ITeamSeasonScheduleProfileRepository _teamSeasonScheduleProfileRepository;
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IWeeklyUpdateService _weeklyUpdateService;

        private static int _selectedSeasonYear = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonsController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        /// <param name="teamSeasonScheduleProfileRepository">The repository by which team season schedule profile data will be accessed.</param>
        /// <param name="teamSeasonScheduleTotalsRepository">The repository by which team season schedule totals data will be accessed.</param>
        /// <param name="teamSeasonScheduleAveragesRepository">The repository by which team season schedule averages data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which shared data resources will be accessed.</param>
        /// <param name="weeklyUpdateService">The service that will run weekly updates of the data store.</param>
        public TeamSeasonsController(
            ISeasonRepository seasonRepository,
            ITeamSeasonRepository teamSeasonRepository,
            ITeamSeasonScheduleProfileRepository teamSeasonScheduleProfileRepository,
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository,
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository,
            ISharedRepository sharedRepository,
            IWeeklyUpdateService weeklyUpdateService)
        {
            _seasonRepository = seasonRepository;
            _teamSeasonRepository = teamSeasonRepository;
            _teamSeasonScheduleProfileRepository = teamSeasonScheduleProfileRepository;
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
            _sharedRepository = sharedRepository;
            _weeklyUpdateService = weeklyUpdateService;
        }

        // GET: TeamSeasons
        /// <summary>
        /// Renders a view of the team seasons index.
        /// </summary>
        /// <returns>The rendered view of the team seasons index.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.Year);

            var viewModel = new TeamSeasonsIndexViewModel
            {
                Seasons = new SelectList(seasons, "Year", "Year", _selectedSeasonYear),
                SelectedSeasonYear = _selectedSeasonYear,
                TeamSeasons = (await _teamSeasonRepository.GetTeamSeasons())
                    .Where(ts => ts.SeasonYear == _selectedSeasonYear)
            };

            return View(viewModel);
        }

        // GET: TeamSeasons/Details/5
        /// <summary>
        /// Renders a view of a selected team season.
        /// </summary>
        /// <param name="id">The ID of the selected team season.</param>
        /// <returns>The rendered view of the selected team season.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var teamSeason = await _teamSeasonRepository.GetTeamSeason(id.Value);
            if (teamSeason is null)
            {
                return NotFound();
            }

            var teamName = teamSeason.TeamName;
            var seasonYear = teamSeason.SeasonYear;
            var viewModel = new TeamSeasonsDetailsViewModel
            {
                TeamSeason = teamSeason,
                TeamSeasonScheduleProfile = 
                    await _teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfile(teamName, seasonYear),
                TeamSeasonScheduleTotals = 
                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear),
                TeamSeasonScheduleAverages =
                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear)
            };

            return View(viewModel);
        }

        // TeamSeasons/RunWeeklyUpdate
        /// <summary>
        /// Runs a weekly update of the TeamSeasons list.
        /// </summary>
        /// <returns>The rendered view of the team seasons index.</returns>
        [HttpGet]
        public async Task<IActionResult> RunWeeklyUpdate()
        {
            await _weeklyUpdateService.RunWeeklyUpdate(_selectedSeasonYear);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Sets the selected season year.
        /// </summary>
        /// <param name="seasonYear">The season year to which the selected season year will be set.</param>
        /// <returns>The rendered view of the team seasons index.</returns>
        public IActionResult SetSelectedSeasonYear(int? seasonYear)
        {
            if (seasonYear is null)
            {
                return BadRequest();
            }

            _selectedSeasonYear = seasonYear.Value;

            return RedirectToAction(nameof(Index));
        }

    }
}
