using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of game data.
    /// </summary>
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IGameService _gameService;

        private static int _selectedSeasonYear = 1920;
        private static int? _selectedWeek;

        private static Game _oldGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="gameRepository">The repository by which game data will be accessed.</param>
        /// <param name="teamRepository">The repository by which team data will be accessed.</param>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which shared data resources will be accessed.</param>
        /// <param name="gameService">The service for processing Game data.</param>
        public GamesController(
            IGameRepository gameRepository,
            ITeamRepository teamRepository,
            ISeasonRepository seasonRepository,
            ISharedRepository sharedRepository,
            IGameService gameService)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _seasonRepository = seasonRepository;
            _sharedRepository = sharedRepository;
            _gameService = gameService;
        }

        // GET: Games
        /// <summary>
        /// Renders a view of the Games list.
        /// </summary>
        /// <returns>The rendered view of the Games list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seasons = (await _seasonRepository.GetSeasonsAsync()).OrderByDescending(s => s.Year);

            var weeks = new List<int?>();

            var selectedSeason = (await _seasonRepository.GetSeasonsAsync())
                .FirstOrDefault(s => s.Year == _selectedSeasonYear);
            if (!(selectedSeason is null))
            {
                for (int i = 0; i <= selectedSeason.NumOfWeeksScheduled; i++)
                {
                    if (i == 0)
                    {
                        weeks.Add(null);
                    }
                    else
                    {
                        weeks.Add(i);
                    }
                }
            }

            var games = (await _gameRepository.GetGamesAsync()).Where(g => g.SeasonYear == _selectedSeasonYear);
            if (_selectedWeek.HasValue)
            {
                games = games.Where(g => g.Week == _selectedWeek);
            }
            games = games.ToList();

            var viewModel = new GamesIndexViewModel
            {
                Seasons = new SelectList(seasons, "Year", "Year", _selectedSeasonYear),
                SelectedSeasonYear = _selectedSeasonYear,
                Weeks = new SelectList(weeks, _selectedWeek),
                SelectedWeek = _selectedWeek,
                Games = games
            };

            return View(viewModel);
        }

        // GET: Games/Details/5
        /// <summary>
        /// Renders a view of the details of a selected game.
        /// </summary>
        /// <param name="id">The ID of the selected game.</param>
        /// <returns>The rendered view of the selected game.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetGameAsync(id.Value);
            if (game is null)
            {
                return NotFound();
            }

            var viewModel = new GamesDetailsViewModel
            {
                Game = game
            };

            return View(viewModel);
        }

        // GET: Games/Create
        /// <summary>
        /// Renders a view of the game create form.
        /// </summary>
        /// <returns>The rendered view of the game create form.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var seasons = (await _seasonRepository.GetSeasonsAsync()).OrderByDescending(s => s.Year);
            ViewBag.Seasons = new SelectList(seasons, "Year", "Year", _selectedSeasonYear);

            var selectedSeason = (await _seasonRepository.GetSeasonsAsync())
                .FirstOrDefault(s => s.Year == _selectedSeasonYear);
            var weeks = new List<int>();
            for (int i = 1; i <= selectedSeason.NumOfWeeksScheduled; i++)
            {
                weeks.Add(i);
            }
            var selectedWeek = _selectedWeek ?? 1;
            ViewBag.Weeks = new SelectList(weeks, selectedWeek);

            // TODO: Uncomment this when the slate of teams is finalized.
            //var teams = await _teamRepository.GetTeams();
            //ViewBag.GuestName = new SelectList(teams, "Name", "Name");
            //ViewBag.HostName = new SelectList(teams, "Name", "Name");

            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the game create form.
        /// </summary>
        /// <param name="game">A <see cref="Game"/> object with the data provided for the new game.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeasonYear,Week,GuestName,GuestScore,HostName,HostScore,WinnerName,WinnerScore,LoserName,LoserScore,IsPlayoffGame,Notes")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gameService.AddGameAsync(game);
                await _sharedRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Create));
            }

            return View(game);
        }

        // GET: Games/Edit/5
        [HttpGet]
        /// <summary>
        /// Renders a view of the game edit form.
        /// </summary>
        /// <returns>The rendered view of the game edit form.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetGameAsync(id.Value);
            if (game is null)
            {
                return NotFound();
            }

            var seasons = await _seasonRepository.GetSeasonsAsync();
            ViewBag.Seasons = new SelectList(seasons, "Year", "Year", game.SeasonYear);

            var selectedSeason = (await _seasonRepository.GetSeasonsAsync())
                .FirstOrDefault(s => s.Year == _selectedSeasonYear);
            var weeks = new List<int>();
            for (int i = 1; i <= selectedSeason.NumOfWeeksScheduled; i++)
            {
                weeks.Add(i);
            }
            ViewBag.Weeks = new SelectList(weeks, game.Week);

            // TODO: Uncomment this when the slate of teams is finalized.
            //var teams = await _teamRepository.GetTeams();
            //ViewBag.GuestName = new SelectList(teams, "Name", "Name");
            //ViewBag.HostName = new SelectList(teams, "Name", "Name");

            _oldGame = game;

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Processes the data posted back from the game edit form.
        /// </summary>
        /// <param name="game">A <see cref="Game"/> object with the data provided for the updated game.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeasonYear,Week,GuestName,GuestScore,HostName,HostScore,WinnerName,WinnerScore,LoserName,LoserScore,IsPlayoffGame,Notes")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gameService.EditGameAsync(game, _oldGame);
                    await _sharedRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _gameRepository.GameExists(game.ID))
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

            return View(game);
        }

        // GET: Games/Delete/5
        /// <summary>
        /// Renders a view of the game delete form.
        /// </summary>
        /// <returns>The rendered view of the game delete form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetGameAsync(id.Value);
            if (game is null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        /// <summary>
        /// Processes the confirmation of intent to delete a game.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>The rendered <see cref="ActionResult"/> object.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gameService.DeleteGameAsync(id);
            await _sharedRepository.SaveChangesAsync();

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

        /// <summary>
        /// Sets the selected week.
        /// </summary>
        /// <param name="week">The selected week.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/>.</returns>
        public IActionResult SetSelectedWeek(int? week)
        {
            _selectedWeek = week;

            return RedirectToAction(nameof(Index));
        }
    }
}
