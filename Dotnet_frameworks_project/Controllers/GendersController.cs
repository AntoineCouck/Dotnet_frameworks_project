#nullable disable
using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GendersController : ApplicationController
    {


        public GendersController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {


        }


        // GET: Genders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gender.ToListAsync());
        }

        // GET: Genders/Details/5
        public async Task<IActionResult> Details(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // GET: Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(char id, [Bind("ID,Name")] Gender gender)
        {
            if (id != gender.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.ID))
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
            return View(gender);
        }

        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(char id)
        {
            var gender = await _context.Gender.FindAsync(id);
            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(char id)
        {
            return _context.Gender.Any(e => e.ID == id);
        }
    }
}
