using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace Dotnet_frameworks_project.Controllers
{
    public class HomeController : ApplicationController
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<HomeController> localizer) : base(context, httpContextAccessor, logger)

        {
            _localizer = localizer;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}