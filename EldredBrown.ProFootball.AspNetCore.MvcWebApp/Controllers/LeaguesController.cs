using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Leagues;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of league data.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class LeaguesController : Controller
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly ISharedRepository _sharedRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaguesController"/> class.
        /// </summary>
        /// <param name="leagueRepository">The repository by which league data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which shared data resources will be accessed.</param>
        public LeaguesController(ILeagueRepository leagueRepository, ISharedRepository sharedRepository)
        {
            _leagueRepository = leagueRepository;
            _sharedRepository = sharedRepository;
        }

        // GET: Leagues
        /// <summary>
        /// Renders a view of the Leagues list.
        /// </summary>
        /// <returns>The rendered view of the Leagues list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new LeaguesIndexViewModel
            {
                Title = "Leagues",
                Leagues = await _leagueRepository.GetLeagues()
            };

            return View(viewModel);
        }

        // GET: Leagues/Details/5
        /// <summary>
        /// Renders a view of the details of a selected league.
        /// </summary>
        /// <param name="id">The ID of the selected league.</param>
        /// <returns>The rendered view of the selected league.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _leagueRepository.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }

            var viewModel = new LeaguesDetailsViewModel
            {
                Title = "League",
                League = league
            };

            return View(viewModel);
        }

        // GET: Leagues/Create
        /// <summary>
        /// Renders a view of the league create form.
        /// </summary>
        /// <returns>The rendered view of the league create form.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the league create form.
        /// </summary>
        /// <param name="league">A <see cref="League"/> object with the data provided for the new league.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LongName,ShortName,FirstSeasonYear,LastSeasonYear")] League league)
        {
            if (ModelState.IsValid)
            {
                await _leagueRepository.Add(league);
                await _sharedRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(league);
        }

        // GET: Leagues/Edit/5
        /// <summary>
        /// Renders a view of the league edit form.
        /// </summary>
        /// <returns>The rendered view of the league edit form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _leagueRepository.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the league edit form.
        /// </summary>
        /// <param name="league">A <see cref="League"/> object with the data provided for the league game.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LongName,ShortName,FirstSeasonYear,LastSeasonYear")] League league)
        {
            if (id != league.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _leagueRepository.Edit(league);
                    await _sharedRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _leagueRepository.LeagueExists(league.ID))
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

            return View(league);
        }

        // GET: Leagues/Delete/5
        /// <summary>
        /// Renders a view of the league delete form.
        /// </summary>
        /// <returns>The rendered view of the league delete form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = await _leagueRepository.GetLeague(id.Value);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        /// <summary>
        /// Processes the confirmation of intent to delete a league.
        /// </summary>
        /// <param name="id">The ID of the league to delete.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leagueRepository.Delete(id);
            await _sharedRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
