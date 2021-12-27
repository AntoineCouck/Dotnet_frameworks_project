using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Controllers
{
    public class FollowUp_typeController : ApplicationController
    {
        private readonly IStringLocalizer<FollowUp_typeController> _localizer;

        public FollowUp_typeController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<FollowUp_typeController> localizer) : base(context, httpContextAccessor, logger)

        {
            _localizer = localizer;
        }



        // GET: FollowUp_type
        public async Task<IActionResult> Index()
        {
            return View(await _context.FollowUp_type.ToListAsync());
        }

        // GET: FollowUp_type/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_type = await _context.FollowUp_type
                .FirstOrDefaultAsync(m => m.Name == id);
            if (followUp_type == null)
            {
                return NotFound();
            }

            return View(followUp_type);
        }

        // GET: FollowUp_type/Create
        public IActionResult Create()
        {


            return View();
        }

        // POST: FollowUp_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Number_of_requiredsessions")] FollowUp_type followUp_type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followUp_type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(followUp_type);
        }

        // GET: FollowUp_type/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_type = await _context.FollowUp_type.FindAsync(id);
            if (followUp_type == null)
            {
                return NotFound();
            }
            return View(followUp_type);
        }

        // POST: FollowUp_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Number_of_requiredsessions")] FollowUp_type followUp_type)
        {
            if (id != followUp_type.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followUp_type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowUp_typeExists(followUp_type.Name))
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
            return View(followUp_type);
        }

        // GET: FollowUp_type/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_type = await _context.FollowUp_type
                .FirstOrDefaultAsync(m => m.Name == id);
            if (followUp_type == null)
            {
                return NotFound();
            }

            return View(followUp_type);
        }

        // POST: FollowUp_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var followUp_type = await _context.FollowUp_type.FindAsync(id);
            _context.FollowUp_type.Remove(followUp_type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowUp_typeExists(string id)
        {
            return _context.FollowUp_type.Any(e => e.Name == id);
        }
    }
}
