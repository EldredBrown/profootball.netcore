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

        private static int _guestSeasonId = 1920;
        private static int _hostSeasonId = 1920;

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
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);

            ViewBag.GuestSeasonId = new SelectList(seasons, "ID", "ID", _guestSeasonId);

            var guests = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonId == _guestSeasonId);
            ViewBag.GuestName = new SelectList(guests, "TeamName", "TeamName");

            ViewBag.HostSeasonId = new SelectList(seasons, "ID", "ID", _hostSeasonId);

            var hosts = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonId == _hostSeasonId);
            ViewBag.HostName = new SelectList(hosts, "TeamName", "TeamName");

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
        public async Task<IActionResult> PredictGame([Bind("GuestSeasonId,GuestName,GuestScore,HostSeasonId,HostName,HostScore")] GamePrediction prediction)
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);

            _guestSeasonId = prediction.GuestSeasonId;

            ViewBag.GuestSeasonId = new SelectList(seasons, "ID", "ID", _guestSeasonId);

            var guests = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonId == _guestSeasonId);
            var guest = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(
                prediction.GuestName, _guestSeasonId);
            ViewBag.GuestName = new SelectList(guests, "TeamName", "TeamName", guest.TeamName);

            _hostSeasonId = prediction.HostSeasonId;

            ViewBag.HostSeasonId = new SelectList(seasons, "ID", "ID", _hostSeasonId);

            var hosts = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonId == _hostSeasonId);
            var host = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(
                prediction.HostName, _hostSeasonId);
            ViewBag.HostName = new SelectList(hosts, "TeamName", "TeamName", host.TeamName);

            return View(prediction);
        }

        /// <summary>
        /// Applies a filter to listed guest or host data.
        /// </summary>
        /// <param name="guestSeasonId">The season for which possible guests will be shown.</param>
        /// <param name="hostSeasonId">The season for which possible hosts will be shown.</param>
        /// <returns>The rendered view of the Game Predictor form.</returns>
        public IActionResult ApplyFilter(int? guestSeasonId, int? hostSeasonId)
        {
            if (guestSeasonId != null)
            {
                _guestSeasonId = guestSeasonId.Value;
            }

            if (hostSeasonId != null)
            {
                _hostSeasonId = hostSeasonId.Value;
            }

            return RedirectToAction(nameof(PredictGame));
        }
    }
}
