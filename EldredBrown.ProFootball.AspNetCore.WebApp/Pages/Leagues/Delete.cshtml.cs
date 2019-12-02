using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EldredBrown.ProFootball.AspNetCore.WebApp.Pages.Leagues
{
    public class DeleteModel : PageModel
    {
        private readonly ILeagueRepository _leagueRepository;

        public DeleteModel(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            League = await _leagueRepository.GetLeague(id.Value);

            if (League != null)
            {
                await _leagueRepository.Delete(League.ID);
                await _leagueRepository.Commit();
            }

            return RedirectToPage("./Index");
        }
    }
}
