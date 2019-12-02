﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Games;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
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

        // GET: Games
        /// <summary>
        /// Renders a view of the Games list.
        /// </summary>
        /// <returns>The rendered view of the Games list.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
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

        // GET: Games/Details/5
        /// <summary>
        /// Renders a view of a selected game.
        /// </summary>
        /// <param name="id">The ID of the selected game.</param>
        /// <returns>The rendered view of the selected game.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        /// <summary>
        /// Renders a view of the game create form.
        /// </summary>
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
        public async Task<IActionResult> Create([Bind("ID,SeasonId,Week,GuestName,GuestScore,HostName,HostScore,WinnerName,WinnerScore,LoserName,LoserScore,IsPlayoffGame,Notes")] Game game)
        {
            if (ModelState.IsValid)
            {
                await _gameRepository.Add(game);
                await _gameRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
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
            if (id == null)
            {
                return NotFound();
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeasonId,Week,GuestName,GuestScore,HostName,HostScore,WinnerName,WinnerScore,LoserName,LoserScore,IsPlayoffGame,Notes")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _gameRepository.Edit(game);
                    await _gameRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await GameExists(game.ID)))
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
            if (id == null)
            {
                return NotFound();
            }

            var game = await _gameRepository.GetGame(id.Value);
            if (game == null)
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
            await _gameRepository.Delete(id);
            await _gameRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
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

        /// <summary>
        /// Sets the selected week.
        /// </summary>
        /// <param name="week">The selected week.</param>
        /// <returns>The rendered view of the <see cref="RedirectToActionResult"/></returns>
        public IActionResult SetSelectedWeek(int? week)
        {
            _selectedWeek = week;

            return RedirectToAction("Index");
        }

        private async Task<bool> GameExists(int id)
        {
            return (await _gameRepository.GetGames()).Any(e => e.ID == id);
        }
    }
}
