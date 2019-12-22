using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EldredBrown.ProFootball.AspNetCore.WebApp.Pages.Leagues
{
    public class EditModel : PageModel
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly ISharedRepository _sharedRepository;

        public EditModel(ILeagueRepository leagueRepository, ISharedRepository sharedRepository)
        {
            _leagueRepository = leagueRepository;
            _sharedRepository = sharedRepository;
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _leagueRepository.Edit(League);

            try
            {
                await _sharedRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _leagueRepository.LeagueExists(League.ID)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
