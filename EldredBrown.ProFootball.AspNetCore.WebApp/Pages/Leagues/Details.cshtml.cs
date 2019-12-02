using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EldredBrown.ProFootball.AspNetCore.WebApp.Pages.Leagues
{
    public class DetailsModel : PageModel
    {
        private readonly ILeagueRepository _leagueRepository;

        public DetailsModel(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public League League { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            League = await _leagueRepository.GetLeague(id.Value);

            if (League == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
