using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of game data.
    /// </summary>
    public class GamesController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;

        private static int _selectedSeasonId = 1920;
        private static int? _selectedWeek;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which season data will be accessed.</param>
        /// <param name="weekRepository">The repository by which week data will be accessed.</param>
        /// <param name="gameRepository">The repository by which game data will be accessed.</param>
        /// <param name="teamRepository">The repository by which team data will be accessed.</param>
        public GamesController(
            ISeasonRepository seasonRepository,
            IGameRepository gameRepository,
            ITeamRepository teamRepository)
        {
            _seasonRepository = seasonRepository;
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
        }

        /// <summary>
        /// Renders a view of the games list.
        /// </summary>
        /// <param name="seasonId">The season for which games will be listed.</param>
        /// <param name="weekId">The week for which games will be listed.</param>
        /// <returns>The rendered view of the games list.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);

            var selectedSeason = await _seasonRepository.GetSeason(_selectedSeasonId);
            var weeks = new List<int>();
            for (int i = 1; i <= selectedSeason.NumOfWeeks; i++)
            {
                weeks.Add(i);
            }

            var games = (await _gameRepository.GetGames()).Where(g => g.SeasonId == _selectedSeasonId);
            if (_selectedWeek.HasValue)
            {
                games = games.Where(g => g.Week == _selectedWeek);
            }                

            var viewModel = new GameListViewModel
            {
                Seasons = new SelectList(seasons, "ID", "ID", _selectedSeasonId),
                Weeks = new SelectList(weeks, _selectedWeek),
                Games = games
            };

            return View(viewModel);
        }

        /// <summary>
        /// Renders a view of the game create form.
        /// </summary>
        /// <param name="seasonId">The season to which the game will be added.</param>
        /// <param name="weekId">The week to which the game will be added.</param>
        /// <returns>The rendered view of the game create form.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var seasons = (await _seasonRepository.GetSeasons()).OrderByDescending(s => s.ID);
            ViewBag.SeasonId = new SelectList(seasons, "ID", "ID", _selectedSeasonId);

            var selectedSeason = await _seasonRepository.GetSeason(_selectedSeasonId);
            var weeks = new List<int>();
            for (int i = 1; i <= selectedSeason.NumOfWeeks; i++)
            {
                weeks.Add(i);
            }
            ViewBag.Week = new SelectList(weeks, _selectedWeek);

            var teams = await _teamRepository.GetTeams();
            ViewBag.GuestName = new SelectList(teams, "Name", "Name");
            ViewBag.HostName = new SelectList(teams, "Name", "Name");

            return View();
        }

        /// <summary>
        /// Adds a new game to the data store.
        /// </summary>
        /// <param name="newGame">The game to be added.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Game newGame)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _gameRepository.Add(newGame);
            await _gameRepository.SaveChanges();

            return RedirectToAction("List");
        }

        /// <summary>
        /// Renders a view of the game details.
        /// </summary>
        /// <param name="id">The ID of the game to show.</param>
        /// <returns>The rendered view of the game's details.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var game = await _gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        /// <summary>
        /// Renders a view of the game edit form.
        /// </summary>
        /// <param name="id">The ID of the game to edit.</param>
        /// <returns>The rendered view of the game edit form.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var game = await _gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            var seasons = await _seasonRepository.GetSeasons();
            ViewBag.SeasonId = new SelectList(seasons, "ID", "ID", game.SeasonId);

            var selectedSeason = await _seasonRepository.GetSeason(_selectedSeasonId);
            var weeks = new List<int>();
            for (int i = 1; i <= selectedSeason.NumOfWeeks; i++)
            {
                weeks.Add(i);
            }
            ViewBag.Week = new SelectList(weeks, game.Week);

            var teams = await _teamRepository.GetTeams();
            ViewBag.GuestName = new SelectList(teams, "Name", "Name", game.GuestName);
            ViewBag.HostName = new SelectList(teams, "Name", "Name", game.HostName);

            return View(game);
        }

        /// <summary>
        /// Updates a game in the data store.
        /// </summary>
        /// <param name="updatedGame">The game to update.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Game updatedGame)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _gameRepository.Edit(updatedGame);
            await _gameRepository.SaveChanges();

            return RedirectToAction("List");
        }

        /// <summary>
        /// Renders the game delete form.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>The rendered view of the game delete form.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var game = await _gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        /// <summary>
        /// Deletes a game from the data store.
        /// </summary>
        /// <param name="oldGame">The game to delete.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id.HasValue)
            {
                await _gameRepository.Delete(id.Value);
                await _gameRepository.SaveChanges();
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// Sets the selected season ID.
        /// </summary>
        /// <param name="seasonId">The ID of the selected season.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        public IActionResult SetSelectedSeasonId(int? seasonId)
        {
            if (!seasonId.HasValue)
            {
                return BadRequest();
            }

            _selectedSeasonId = seasonId.Value;

            return RedirectToAction("List");
        }

        /// <summary>
        /// Sets the selected week.
        /// </summary>
        /// <param name="week">The ID of the selected week.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        public IActionResult SetSelectedWeek(int? week)
        {
            _selectedWeek = week;

            return RedirectToAction("List");
        }
    }
}
