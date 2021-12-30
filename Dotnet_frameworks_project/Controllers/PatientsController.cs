#nullable disable
using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Controllers
{
    public class PatientsController : ApplicationController
    {
        //private readonly ApplicationContext _context;


        private readonly IStringLocalizer<PatientsController> _localizer;
        private readonly UserManager<ApplicationUser> UserManager;
        public PatientsController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger)

        {
            _localizer = localizer;
            UserManager = userManager;
        }


        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Patient.Include(p => p.Insurance).Include(p => p.user);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.Insurance)
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.Id == id);

            PatientViewModel model = new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthday = patient.Birthday,
                ParentsPhone = patient.ParentsPhone,
                LeftSessions = patient.LeftSessions,
                UserId = patient.UserId,
                InsuranceId = patient.InsuranceId,
                AddSessions = 0,
                RemoveSessions = 0
            };

            //var ListOfFollowUps = _context.FollowUp_patients.Include(f => f.FollowUpType)
            //                                                .Where(p => p.PatientId == id).ToList();     
            var ListOfFollowUps = _context.FollowUp_type.Join(_context.FollowUp_patients, t => t.Name , p => p.FollowUpId , (t , p) => new {t , p})
                                                        .Where(p => p.p.PatientId == id)
                                                        .ToList();


            //var ListOfPassedTests = _context.PassedTests.Include(t => t.Test).Where(t => t.PatientId == id).ToList();    
            var ListOfPassedTests = _context.Test.Join(_context.PassedTests, t => t.Name, p => p.TestId, (t, p) => new { t, p })
                                                 .Where(p => p.p.PatientId == id)
                                                 .ToList();

          
            ViewData["ListOfFollowUps"] = ListOfFollowUps;
            ViewData["ListOfPassedTests"] = ListOfPassedTests;

            if (patient == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, [Bind("Id,FirstName,LastName,Birthday,ParentsPhone,LeftSessions,AddSessions,RemoveSessions,UserId,InsuranceId")] PatientViewModel models)
        {
            List<Patient> patients = _context.Patient.Where(p => p.Id == id).ToList();

            if (ModelState.IsValid)
            {
                foreach (Patient patient2 in patients)
                {
                    patient2.LeftSessions = models.LeftSessions + models.AddSessions - models.RemoveSessions;
                    _context.Update(patient2);


                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details));
            }


            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.Insurance)
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.Id == id);

            PatientViewModel model = new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthday = patient.Birthday,
                ParentsPhone = patient.ParentsPhone,
                LeftSessions = patient.LeftSessions,
                UserId = patient.UserId,
                InsuranceId = patient.InsuranceId,
                AddSessions = 0,
                RemoveSessions = 0
            };

            //var ListOfFollowUps = _context.FollowUp_patients.Include(f => f.FollowUpType)
            //                                                .Where(p => p.PatientId == id).ToList();     
            var ListOfFollowUps = _context.FollowUp_type.Join(_context.FollowUp_patients, t => t.Name, p => p.FollowUpId, (t, p) => new { t, p })
                                                        .Where(p => p.p.PatientId == id)
                                                        .ToList();


            //var ListOfPassedTests = _context.PassedTests.Include(t => t.Test).Where(t => t.PatientId == id).ToList();    
            var ListOfPassedTests = _context.Test.Join(_context.PassedTests, t => t.Name, p => p.TestId, (t, p) => new { t, p })
                                                 .Where(p => p.p.PatientId == id)
                                                 .ToList();


            ViewData["ListOfFollowUps"] = ListOfFollowUps;
            ViewData["ListOfPassedTests"] = ListOfPassedTests;

            if (patient == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            var user = _context.Users.Where(u => u.UserName == _user.UserName).ToList();


            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name");
            ViewData["UserId"] = user;
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,ParentsPhone,LeftSessions,UserId,InsuranceId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                var Account = Activator.CreateInstance<ApplicationUser>();

                Account.Firstname = patient.FirstName;
                Account.Lastname = patient.LastName;
                Account.UserName = patient.FirstName + "." + patient.LastName;
                Account.Email = patient.FirstName + "." + patient.LastName + "@hotmail.be";
                Account.EmailConfirmed = true;
                Account.LanguageId = "nl";
                await UserManager.CreateAsync(Account, patient.FirstName + "." + patient.LastName + "L2022");

                patient.AccountId = Account.Id;
                _context.Add(patient);
                await _context.SaveChangesAsync();

                await UserManager.AddToRoleAsync(Account, "Parents");
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name", patient.InsuranceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name", patient.InsuranceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthday,ParentsPhone,LeftSessions,UserId,InsuranceId")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name", patient.InsuranceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.Insurance)
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            var Account = _context.Users.Where(u => u.Id == patient.AccountId).ToList();

            _context.Patient.Remove(patient);

            foreach (ApplicationUser user in Account)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
