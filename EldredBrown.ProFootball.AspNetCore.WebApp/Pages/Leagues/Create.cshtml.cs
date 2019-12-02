using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EldredBrown.ProFootball.AspNetCore.WebApp.Pages.Leagues
{
    public class CreateModel : PageModel
    {
        private readonly ILeagueRepository _leagueRepository;

        public CreateModel(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [BindProperty]
        public League League { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _leagueRepository.Add(League);
            await _leagueRepository.Commit();

            return RedirectToPage("./Index");
        }
    }
}
