using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.SeasonTeams;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of <see cref="SeasonTeam"/> data.
    /// </summary>
    public class SeasonTeamsController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ISeasonTeamRepository _seasonTeamRepository;
        private readonly ISeasonTeamScheduleProfileRepository _seasonTeamScheduleProfileRepository;
        private readonly ISeasonTeamScheduleTotalsRepository _seasonTeamScheduleTotalsRepository;
        private readonly ISeasonTeamScheduleAveragesRepository _seasonTeamScheduleAveragesRepository;

        private static int _selectedSeasonId = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonTeamsController" class./>
        /// </summary>
        /// <param name="seasonRepository">The repository by which Season data will be accessed.</param>
        /// <param name="seasonTeamRepository">The repository by which SeasonTeam data will be accessed.</param>
        /// <param name="seasonTeamScheduleProfileRepository">The repository by which SeasonTeamScheduleProfile data will be accessed.</param>
        /// <param name="seasonTeamScheduleTotalsRepository">The repository by which SeasonTeamScheduleTotals data will be accessed.</param>
        /// <param name="seasonTeamScheduleAveragesRepository">The repository by which SeasonTeamScheduleAverages data will be accessed.</param>
        public SeasonTeamsController(
            ISeasonRepository seasonRepository,
            ISeasonTeamRepository seasonTeamRepository,
            ISeasonTeamScheduleProfileRepository seasonTeamScheduleProfileRepository,
            ISeasonTeamScheduleTotalsRepository seasonTeamScheduleTotalsRepository,
            ISeasonTeamScheduleAveragesRepository seasonTeamScheduleAveragesRepository)
        {
            _seasonRepository = seasonRepository;
            _seasonTeamRepository = seasonTeamRepository;
            _seasonTeamScheduleProfileRepository = seasonTeamScheduleProfileRepository;
            _seasonTeamScheduleTotalsRepository = seasonTeamScheduleTotalsRepository;
            _seasonTeamScheduleAveragesRepository = seasonTeamScheduleAveragesRepository;
        }

        // GET: SeasonTeams
        /// <summary>
        /// Renders a view of the SeasonTeams list.
        /// </summary>
        /// <returns>The rendered view of the SeasonTeams list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);
            
            var viewModel = new SeasonTeamListViewModel
            {
                Seasons = new SelectList(seasons, "ID", "ID", _selectedSeasonId),
                SeasonTeams = (await _seasonTeamRepository.GetSeasonTeams())
                    .Where(st => st.SeasonId == _selectedSeasonId)
            };

            return View(viewModel);
        }

        // GET: SeasonTeams/Details/5
        /// <summary>
        /// Renders a view of a selected seasonTeam.
        /// </summary>
        /// <param name="id">The ID of the selected seasonTeam.</param>
        /// <returns>The rendered view of the selected seasonTeam.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seasonTeam = await _seasonTeamRepository.GetSeasonTeam(id.Value);
            if (seasonTeam == null)
            {
                return NotFound();
            }

            var seasonId = seasonTeam.SeasonId;
            var teamName = seasonTeam.TeamName;
            var viewModel = new SeasonTeamDetailsViewModel
            {
                SeasonTeam = seasonTeam,
                SeasonTeamScheduleProfile = _seasonTeamScheduleProfileRepository.GetSeasonTeamScheduleProfile(
                    seasonId, teamName),
                SeasonTeamScheduleTotals = _seasonTeamScheduleTotalsRepository.GetSeasonTeamScheduleTotals(
                    seasonId, teamName),
                SeasonTeamScheduleAverages = _seasonTeamScheduleAveragesRepository.GetSeasonTeamScheduleAverages(
                    seasonId, teamName)
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
            if (seasonId == null)
            {
                return BadRequest();
            }

            _selectedSeasonId = seasonId.Value;

            return RedirectToAction("Index");
        }
    }
}
