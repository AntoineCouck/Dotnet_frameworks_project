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

        public PatientsController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {

        }


        // GET: Patients
        public async Task<IActionResult> Index(string nameFilter, char genderFilter, string orderBy)
        {
            var applicationContext = await _context.Patient.Include(p => p.Insurance).Include(p => p.user).ToListAsync();
            
            ViewData["object"] = applicationContext;

            var filteredPatients = from m in _context.Patient
                                   select m;

            if (genderFilter != 0)
            {
                filteredPatients = from s in _context.Patient
                                   where s.GenderId == genderFilter
                                   select s;
            }


            if (!string.IsNullOrEmpty(nameFilter))
            {
                filteredPatients = from s in filteredPatients
                                   where s.LastName.Contains(nameFilter) || s.FirstName.Contains(nameFilter)
                                   orderby s.LastName, s.FirstName
                                   select s;
            }

            // encore a faire

            ViewData["NameField"] = orderBy == "Name" ? "Name_Desc" : "Name";
            ViewData["LastName"] = orderBy == "Lastname" ? "LastName_Desc" : "Lastname";
            ViewData["BirthDay"] = string.IsNullOrEmpty(orderBy) ? "Date_Desc" : "";
            ViewData["genderId"] = new SelectList(_context.Gender.ToList(), "ID", "Name");

            switch (orderBy)
            {
                case "LastName":
                    filteredPatients = filteredPatients.OrderBy(m => m.LastName);
                    break;
                case "LastName_Desc":
                    filteredPatients = filteredPatients.OrderByDescending(m => m.LastName);
                    break;
                case "Name_Desc":
                    filteredPatients = filteredPatients.OrderByDescending(m => m.FirstName);
                    break;
                case "Name":
                    filteredPatients = filteredPatients.OrderBy(m => m.FirstName);
                    break;
                case "Date":
                    filteredPatients = filteredPatients.OrderBy(m => m.Birthday);
                    break;
                case "Date_Desc":
                    filteredPatients = filteredPatients.OrderByDescending(m => m.Birthday);
                    break;


                default:
                    filteredPatients = filteredPatients.OrderBy(m => m.Birthday);
                    break;
            }

            // Lijst van groepen 
            IQueryable<Patient> groupsToSelect = from g in _context.Patient orderby g.FirstName select g;

            // Maak een object van de view-model-class en voeg daarin alle wat we nodig hebben

            // encore a faire

            PatientIndexViewModel studentviewmodel = new PatientIndexViewModel()
            {
                

                NameFilter = nameFilter,
                GenderFilter = genderFilter,
                FilteredStudents = await filteredPatients.Include(s => s.Gender).ToListAsync(),
                ListGenders = new SelectList(await groupsToSelect.ToListAsync(), "ID", "Name", genderFilter)


            };

            return View(studentviewmodel);


            //return View(await applicationContext.ToListAsync());

            //return View(Patientviewmodel);
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
                   .Include(s => s.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);

            PatientViewModel model = new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthday = patient.Birthday,
                GenderId = patient.GenderId,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, [Bind("Id,FirstName,LastName,Birthday,GenderId,ParentsPhone,LeftSessions,AddSessions,RemoveSessions,UserId,InsuranceId")] PatientViewModel models)
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
                GenderId = patient.GenderId,
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

            ViewData["GenderId"] = new SelectList(_context.Gender, "ID", "Name");
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name");
            ViewData["UserId"] = user;
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,GenderId,ParentsPhone,LeftSessions,UserId,InsuranceId")] Patient patient)
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
                await _userManager.CreateAsync(Account, patient.FirstName + "." + patient.LastName + "L2022");

                patient.AccountId = Account.Id;
                _context.Add(patient);
                await _context.SaveChangesAsync();

                await _userManager.AddToRoleAsync(Account, "Parents");
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Name", "Name", patient.InsuranceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "ID", "Name", patient.GenderId);

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
            ViewData["GenderId"] = new SelectList(_context.Gender, "ID", "Name", patient.GenderId);

            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birthday,GenderId,ParentsPhone,LeftSessions,UserId,InsuranceId")] Patient patient)
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "ID", "Name", patient.GenderId);

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
                 .Include(s => s.Gender)
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
