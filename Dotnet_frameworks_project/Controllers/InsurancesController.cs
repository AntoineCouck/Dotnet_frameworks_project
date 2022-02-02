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
    public class InsurancesController : ApplicationController
    {


        public InsurancesController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {

        }


        // GET: Insurances
        public async Task<IActionResult> Index(string nameFilter, string orderBy)
        {
            var filteredInsurances = from m in _context.Insurance
                                     select m;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                filteredInsurances = from s in filteredInsurances
                                     where s.Name.Contains(nameFilter)
                                     orderby s.Name
                                     select s;
            }

            ViewData["NameField"] = orderBy == "Name" ? "Name_Desc" : "Name";

            switch (orderBy)
            {

                case "Name_Desc":
                    filteredInsurances = filteredInsurances.OrderByDescending(m => m.Name);
                    break;
                case "Name":
                    filteredInsurances = filteredInsurances.OrderBy(m => m.Name);
                    break;


                default:
                    filteredInsurances = filteredInsurances.OrderBy(m => m.Name);
                    break;
            }

            InsuranceIndexViewModel viewModel = new InsuranceIndexViewModel
            {
                NameFilter = nameFilter,
                Insurances = await filteredInsurances.ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = _context.Patient.Where(p => p.InsuranceId == id).ToList();

            ViewData["Patients"] = patients;

            var insurance = await _context.Insurance
                .FirstOrDefaultAsync(m => m.Name == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Adres,PhoneNumber")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insurance);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Adres,PhoneNumber")] Insurance insurance)
        {
            if (id != insurance.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.Name))
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
            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .FirstOrDefaultAsync(m => m.Name == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var insurance = await _context.Insurance.FindAsync(id);
            _context.Insurance.Remove(insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(string id)
        {
            return _context.Insurance.Any(e => e.Name == id);
        }
    }
}
