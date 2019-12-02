using System.Threading.Tasks;
using EldredBrown.ProFootball.AspNetCore.MvcWebApp.ViewModels.Leagues;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldredBrown.ProFootball.AspNetCore.MvcWebApp.Controllers
{
    /// <summary>
    /// Provides control of the flow of execution for views of League data.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class LeaguesController : Controller
    {
        private readonly ILeagueRepository _leagueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaguesController"/> class.
        /// </summary>
        /// <param name="leagueRepository">The repository by which League data will be accessed.</param>
        public LeaguesController(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        /// <summary>
        /// Renders a view of the Leagues list.
        /// </summary>
        /// <returns>The rendered view of the Leagues list.</returns>
        public async Task<IActionResult> Index()
        {
            var viewModel = new LeagueListViewModel
            {
                Title = "Leagues",
                Leagues = await _leagueRepository.GetLeagues()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Renders a view of a selected league.
        /// </summary>
        /// <param name="id">The ID of the selected league.</param>
        /// <returns>The rendered view of the selected league.</returns>
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

            var viewModel = new LeagueDetailsViewModel
            {
                Title = "League",
                League = league
            };

            return View(viewModel);
        }
    }
}
