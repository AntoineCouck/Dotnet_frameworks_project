﻿#nullable disable
using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Controllers
{
    public class TestsController : ApplicationController
    {
      
        private readonly IStringLocalizer<TestsController> _localizer;

        public TestsController(ApplicationContext context , IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<TestsController> localizer) : base(context, httpContextAccessor, logger)

        {
            _localizer = localizer;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Test.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.Name == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Usage,Min_age,Max_age")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Usage,Min_age,Max_age")] Test test)
        {
            if (id != test.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Name))
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
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.Name == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var test = await _context.Test.FindAsync(id);
            _context.Test.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(string id)
        {
            return _context.Test.Any(e => e.Name == id);
        }
    }
}