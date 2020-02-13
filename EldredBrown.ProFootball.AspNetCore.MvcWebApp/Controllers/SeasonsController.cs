using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Seasons;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of season data.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class SeasonsController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ISharedRepository _sharedRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonsController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which shared data resources will be accessed.</param>
        public SeasonsController(ISeasonRepository seasonRepository, ISharedRepository sharedRepository)
        {
            _seasonRepository = seasonRepository;
            _sharedRepository = sharedRepository;
        }

        // GET: Seasons
        /// <summary>
        /// Renders a view of the Seasons list.
        /// </summary>
        /// <returns>The rendered view of the Seasons list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SeasonsIndexViewModel
            {
                Seasons = await _seasonRepository.GetSeasons()
            };

            return View(viewModel);
        }

        // GET: Seasons/Details/5
        /// <summary>
        /// Renders a view of the details of a selected season.
        /// </summary>
        /// <param name="id">The ID of the selected season.</param>
        /// <returns>The rendered view of the selected season.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _seasonRepository.GetSeason(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            var viewModel = new SeasonsDetailsViewModel
            {
                Title = "Season",
                Season = season
            };

            return View(viewModel);
        }

        // GET: Seasons/Create
        /// <summary>
        /// Renders a view of the season create form.
        /// </summary>
        /// <returns>The rendered view of the season create form.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the season create form.
        /// </summary>
        /// <param name="season">A <see cref="Season"/> object with the data provided for the new season.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,NumOfWeeksScheduled,NumOfWeeksCompleted")] Season season)
        {
            if (ModelState.IsValid)
            {
                await _seasonRepository.Add(season);
                await _sharedRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(season);
        }

        // GET: Seasons/Edit/5
        /// <summary>
        /// Renders a view of the season edit form.
        /// </summary>
        /// <returns>The rendered view of the season edit form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _seasonRepository.GetSeason(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the season edit form.
        /// </summary>
        /// <param name="season">A <see cref="Season"/> object with the data provided for the season game.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Year,NumOfWeeksScheduled,NumOfWeeksCompleted")] Season season)
        {
            if (id != season.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _seasonRepository.Update(season);
                    await _sharedRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _seasonRepository.SeasonExists(season.ID))
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

            return View(season);
        }

        // GET: Seasons/Delete/5
        /// <summary>
        /// Renders a view of the season delete form.
        /// </summary>
        /// <returns>The rendered view of the season delete form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _seasonRepository.GetSeason(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Delete/5
        /// <summary>
        /// Processes the confirmation of intent to delete a season.
        /// </summary>
        /// <param name="id">The ID of the season to delete.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var season = await _seasonRepository.Delete(id);
            await _sharedRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
