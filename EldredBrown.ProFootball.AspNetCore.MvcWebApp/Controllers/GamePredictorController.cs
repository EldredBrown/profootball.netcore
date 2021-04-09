using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for the game predictor.
    /// </summary>
    public class GamePredictorController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        private static int _guestSeasonYear = 1920;
        private static int _hostSeasonYear = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePredictorController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        public GamePredictorController(ISeasonRepository seasonRepository,
            ITeamSeasonRepository teamSeasonRepository)
        {
            _seasonRepository = seasonRepository;
            _teamSeasonRepository = teamSeasonRepository;
        }

        // GET: GamePredictor/PredictGame
        /// <summary>
        /// Renders a view of the Game Predictor form.
        /// </summary>
        /// <returns>The rendered view of the Game Predictor form.</returns>
        [HttpGet]
        public async Task<IActionResult> PredictGame()
        {
            var seasons = (await _seasonRepository.GetSeasonsAsync()).OrderByDescending(s => s.Year);

            ViewBag.GuestSeasons = new SelectList(seasons, "Year", "Year", _guestSeasonYear);

            var guests = await _teamSeasonRepository.GetTeamSeasonsBySeasonAsync(_guestSeasonYear);
            ViewBag.Guests = new SelectList(guests, "TeamName", "TeamName");

            ViewBag.HostSeasons = new SelectList(seasons, "Year", "Year", _hostSeasonYear);

            var hosts = await _teamSeasonRepository.GetTeamSeasonsBySeasonAsync(_hostSeasonYear);
            ViewBag.Hosts = new SelectList(hosts, "TeamName", "TeamName");

            return View();
        }

        // POST: GamePredictor/PredictGame
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the Game Predictor form.
        /// </summary>
        /// <param name="prediction">A <see cref="GamePrediction"/> object representing the game matchup.</param>
        /// <returns>The rendered view of the Game Predictor form.</returns>
        [HttpPost]
        public async Task<IActionResult> PredictGame([Bind("GuestSeasonYear,GuestName,GuestScore,HostSeasonYear,HostName,HostScore")] GamePrediction prediction)
        {
            var seasons = (await _seasonRepository.GetSeasonsAsync()).OrderByDescending(s => s.Year);

            _guestSeasonYear = prediction.GuestSeasonYear;

            ViewBag.GuestSeasons = new SelectList(seasons, "Year", "Year", _guestSeasonYear);

            var guests = await _teamSeasonRepository.GetTeamSeasonsBySeasonAsync(_guestSeasonYear);
            var guest = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(
                prediction.GuestName, _guestSeasonYear);
            ViewBag.Guests = new SelectList(guests, "TeamName", "TeamName", guest.TeamName);

            _hostSeasonYear = prediction.HostSeasonYear;

            ViewBag.HostSeasons = new SelectList(seasons, "Year", "Year", _hostSeasonYear);

            var hosts = await _teamSeasonRepository.GetTeamSeasonsBySeasonAsync(_hostSeasonYear);
            var host = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(
                prediction.HostName, _hostSeasonYear);
            ViewBag.Hosts = new SelectList(hosts, "TeamName", "TeamName", host.TeamName);

            return View(prediction);
        }

        /// <summary>
        /// Applies a filter to listed guest or host data.
        /// </summary>
        /// <param name="guestSeasonYear">The season for which possible guests will be shown.</param>
        /// <param name="hostSeasonYear">The season for which possible hosts will be shown.</param>
        /// <returns>The rendered view of the Game Predictor form.</returns>
        public IActionResult ApplyFilter(int? guestSeasonYear, int? hostSeasonYear)
        {
            if (guestSeasonYear.HasValue)
            {
                _guestSeasonYear = guestSeasonYear.Value;
            }

            if (hostSeasonYear.HasValue)
            {
                _hostSeasonYear = hostSeasonYear.Value;
            }

            return RedirectToAction(nameof(PredictGame));
        }
    }
}
