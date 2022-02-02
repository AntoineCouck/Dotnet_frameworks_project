using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dotnet_frameworks_project.Views.Privacy
{
    public class PrivacyController : ApplicationController
    {
        //private readonly ApplicationContext _context;

        public PrivacyController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
