#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;

namespace Dotnet_frameworks_project.Controllers
{
    public class PassedTestsController : Controller
    {
        private readonly ApplicationContext _context;

        public PassedTestsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: PassedTests
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.PassedTests.Include(p => p.Patient).Include(p => p.Test);
            return View(await applicationContext.ToListAsync());
        }

        // GET: PassedTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passedTests = await _context.PassedTests
                .Include(p => p.Patient)
                .Include(p => p.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passedTests == null)
            {
                return NotFound();
            }

            return View(passedTests);
        }

        // GET: PassedTests/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName");
            ViewData["TestId"] = new SelectList(_context.Test, "Name", "Name");
            return View();
        }

        // POST: PassedTests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestId,PatientId")] PassedTests passedTests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passedTests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", passedTests.PatientId);
            ViewData["TestId"] = new SelectList(_context.Test, "Name", "Name", passedTests.TestId);
            return View(passedTests);
        }

        // GET: PassedTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passedTests = await _context.PassedTests.FindAsync(id);
            if (passedTests == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", passedTests.PatientId);
            ViewData["TestId"] = new SelectList(_context.Test, "Name", "Name", passedTests.TestId);
            return View(passedTests);
        }

        // POST: PassedTests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestId,PatientId")] PassedTests passedTests)
        {
            if (id != passedTests.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passedTests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassedTestsExists(passedTests.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "FirstName", passedTests.PatientId);
            ViewData["TestId"] = new SelectList(_context.Test, "Name", "Name", passedTests.TestId);
            return View(passedTests);
        }

        // GET: PassedTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passedTests = await _context.PassedTests
                .Include(p => p.Patient)
                .Include(p => p.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passedTests == null)
            {
                return NotFound();
            }

            return View(passedTests);
        }

        // POST: PassedTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passedTests = await _context.PassedTests.FindAsync(id);
            _context.PassedTests.Remove(passedTests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassedTestsExists(int id)
        {
            return _context.PassedTests.Any(e => e.Id == id);
        }
    }
}
