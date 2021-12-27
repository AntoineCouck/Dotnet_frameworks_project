using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Controllers
{
    public class FollowUp_patientsController : ApplicationController
    {
        private readonly IStringLocalizer<FollowUp_patientsController> _localizer;

        public FollowUp_patientsController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<FollowUp_patientsController> localizer) : base(context, httpContextAccessor, logger)

        {
            _localizer = localizer;
        }


        // GET: FollowUp_patients
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.FollowUp_patients.Include(f => f.Patient);
            return View(await applicationContext.ToListAsync());
        }

        // GET: FollowUp_patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_patients = await _context.FollowUp_patients
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp_patients == null)
            {
                return NotFound();
            }

            return View(followUp_patients);
        }

        // GET: FollowUp_patients/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName");
            ViewData["TypeId"] = new SelectList(_context.FollowUp_type, "Name", "Name");
            return View();
        }

        // POST: FollowUp_patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FollowUpId,PatientId")] FollowUp_patients followUp_patients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followUp_patients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", followUp_patients.PatientId);
            ViewData["TypeId"] = new SelectList(_context.FollowUp_type, "Name", "Name");

            return View(followUp_patients);
        }

        // GET: FollowUp_patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_patients = await _context.FollowUp_patients.FindAsync(id);
            if (followUp_patients == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", followUp_patients.PatientId);
            return View(followUp_patients);
        }

        // POST: FollowUp_patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FollowUpId,PatientId")] FollowUp_patients followUp_patients)
        {
            if (id != followUp_patients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followUp_patients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowUp_patientsExists(followUp_patients.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", followUp_patients.PatientId);
            return View(followUp_patients);
        }

        // GET: FollowUp_patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp_patients = await _context.FollowUp_patients
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp_patients == null)
            {
                return NotFound();
            }

            return View(followUp_patients);
        }

        // POST: FollowUp_patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var followUp_patients = await _context.FollowUp_patients.FindAsync(id);
            _context.FollowUp_patients.Remove(followUp_patients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowUp_patientsExists(int id)
        {
            return _context.FollowUp_patients.Any(e => e.Id == id);
        }
    }
}
