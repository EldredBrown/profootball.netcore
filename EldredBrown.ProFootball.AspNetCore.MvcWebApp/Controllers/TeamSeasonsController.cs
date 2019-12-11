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
        private readonly IWeeklyUpdateService _weeklyUpdateService;

        private static int _selectedSeasonId = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonsController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        /// <param name="teamSeasonScheduleProfileRepository">The repository by which team season schedule profile data will be accessed.</param>
        /// <param name="teamSeasonScheduleTotalsRepository">The repository by which team season schedule totals data will be accessed.</param>
        /// <param name="teamSeasonScheduleAveragesRepository">The repository by which team season schedule averages data will be accessed.</param>
        public TeamSeasonsController(
            ISeasonRepository seasonRepository,
            ITeamSeasonRepository teamSeasonRepository,
            ITeamSeasonScheduleProfileRepository teamSeasonScheduleProfileRepository,
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository,
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository,
            IWeeklyUpdateService weeklyUpdateService)
        {
            _seasonRepository = seasonRepository;
            _teamSeasonRepository = teamSeasonRepository;
            _teamSeasonScheduleProfileRepository = teamSeasonScheduleProfileRepository;
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
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
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);

            var viewModel = new TeamSeasonsIndexViewModel
            {
                Title = "Teams",
                Seasons = new SelectList(seasons, "ID", "ID", _selectedSeasonId),
                TeamSeasons = (await _teamSeasonRepository.GetTeamSeasons())
                    .Where(ts => ts.SeasonId == _selectedSeasonId)
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
            if (id == null)
            {
                return NotFound();
            }

            var teamSeason = await _teamSeasonRepository.GetTeamSeason(id.Value);
            if (teamSeason == null)
            {
                return NotFound();
            }

            var teamName = teamSeason.TeamName;
            var seasonId = teamSeason.SeasonId;
            var viewModel = new TeamSeasonsDetailsViewModel
            {
                Title = "Team Season",
                TeamSeason = teamSeason,
                TeamSeasonScheduleProfile = 
                    await _teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfile(teamName, seasonId),
                TeamSeasonScheduleTotals = 
                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonId),
                TeamSeasonScheduleAverages =
                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonId)
            };

            return View(viewModel);
        }
    }
}
