using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.LeagueSeasons;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of league season data.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class LeagueSeasonsController : Controller
    {
        private readonly ILeagueSeasonRepository _leagueSeasonRepository;
        private readonly ISharedRepository _sharedRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueSeasonsController"/> class.
        /// </summary>
        /// <param name="leagueSeasonRepository">The repository by which leagueSeason data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which shared data resources will be accessed.</param>
        public LeagueSeasonsController(
            ILeagueSeasonRepository leagueSeasonRepository,
            ISharedRepository sharedRepository)
        {
            _leagueSeasonRepository = leagueSeasonRepository;
            _sharedRepository = sharedRepository;
        }

        // GET: LeagueSeasons
        /// <summary>
        /// Renders a view of the LeagueSeasons list.
        /// </summary>
        /// <returns>The rendered view of the LeagueSeasons list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new LeagueSeasonsIndexViewModel
            {
                Title = "League Seasons",
                LeagueSeasons = await _leagueSeasonRepository.GetLeagueSeasons()
            };

            return View(viewModel);
        }

        // GET: LeagueSeasons/Details/5
        /// <summary>
        /// Renders a view of the details of a selected league season.
        /// </summary>
        /// <param name="id">The ID of the selected league season.</param>
        /// <returns>The rendered view of the selected league season.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueSeason = await _leagueSeasonRepository.GetLeagueSeason(id.Value);
            if (leagueSeason == null)
            {
                return NotFound();
            }

            var viewModel = new LeagueSeasonsDetailsViewModel
            {
                Title = "League Season",
                LeagueSeason = leagueSeason
            };

            return View(viewModel);
        }

        // GET: LeagueSeasons/Create
        /// <summary>
        /// Renders a view of the league season create form.
        /// </summary>
        /// <returns>The rendered view of the league season create form.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeagueSeasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the league season create form.
        /// </summary>
        /// <param name="leagueSeason">A <see cref="LeagueSeason"/> object with the data provided for the new league season.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueName,SeasonYear,TotalGames,TotalPoints,AveragePoints")] LeagueSeason leagueSeason)
        {
            if (ModelState.IsValid)
            {
                await _leagueSeasonRepository.Add(leagueSeason);
                await _sharedRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(leagueSeason);
        }

        // GET: LeagueSeasons/Edit/5
        /// <summary>
        /// Renders a view of the league season edit form.
        /// </summary>
        /// <returns>The rendered view of the league season edit form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueSeason = await _leagueSeasonRepository.GetLeagueSeason(id.Value);
            if (leagueSeason == null)
            {
                return NotFound();
            }

            return View(leagueSeason);
        }

        // POST: LeagueSeasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the league season edit form.
        /// </summary>
        /// <param name="leagueSeason">A <see cref="LeagueSeason"/> object with the data provided for the league season game.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LeagueName,SeasonYear,TotalGames,TotalPoints,AveragePoints")] LeagueSeason leagueSeason)
        {
            if (id != leagueSeason.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _leagueSeasonRepository.Edit(leagueSeason);
                    await _sharedRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _leagueSeasonRepository.LeagueSeasonExists(leagueSeason.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(leagueSeason);
        }

        // GET: LeagueSeasons/Delete/5
        /// <summary>
        /// Renders a view of the league season delete form.
        /// </summary>
        /// <returns>The rendered view of the league season delete form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueSeason = await _leagueSeasonRepository.GetLeagueSeason(id.Value);
            if (leagueSeason == null)
            {
                return NotFound();
            }

            return View(leagueSeason);
        }

        // POST: LeagueSeasons/Delete/5
        /// <summary>
        /// Processes the confirmation of intent to delete a league season.
        /// </summary>
        /// <param name="id">The ID of the league season to delete.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leagueSeasonRepository.Delete(id);
            await _sharedRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
