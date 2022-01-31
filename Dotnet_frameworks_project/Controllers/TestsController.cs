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
    [Authorize(Roles = "Logopedist,Admin")]
    public class TestsController : ApplicationController
    {


        public TestsController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {
        }

        // GET: Tests
        public async Task<IActionResult> Index(string nameFilter , string orderBy)
        {

            var filteredTests = from m in _context.Test
                                   select m;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                filteredTests =    from s in filteredTests
                                   where s.Name.Contains(nameFilter)
                                   orderby s.Name
                                   select s;
            }

            ViewData["NameField"] = orderBy == "Name" ? "Name_Desc" : "Name";

            switch (orderBy)
            {
               
                case "Name_Desc":
                    filteredTests = filteredTests.OrderByDescending(m => m.Name);
                    break;
                case "Name":
                    filteredTests = filteredTests.OrderBy(m => m.Name);
                    break;


                default:
                    filteredTests = filteredTests.OrderBy(m => m.Name);
                    break;
            }

            TestIndexViewModel testsviewmodel = new TestIndexViewModel()
            {


                NameFilter = nameFilter,

                tests = await filteredTests.ToListAsync()
               


            };

            return View(testsviewmodel);
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

            var HasPassedTheTest = _context.PassedTests.Join(_context.Patient, t => t.PatientId, p => p.Id, (t, p) => new { t, p })
                                           .Where(p => p.t.TestId == id)
                                           .ToList();


            ViewData["HasPassedTheTest"] = HasPassedTheTest;



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
