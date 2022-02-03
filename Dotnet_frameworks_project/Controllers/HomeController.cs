using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Net;

namespace Dotnet_frameworks_project.Controllers
{
    public class HomeController : ApplicationController
    {


        public HomeController(ApplicationContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer) : base(context, httpContextAccessor, logger, localizer, userManager)

        {

        }

        public IActionResult Index()
        {
            var CookiesAccepted = _context.Users.Where(u => u.Id == _user.Id).Select(u => u.AcceptCookies);

            foreach(var Accept in CookiesAccepted)
            {
                if (Accept == true)
                {
                   

                    Cookie cookie = new Cookie("Username", _user.UserName );
                    ViewData["username"] = cookie.Value;


                } else
                {


                }
            }
          
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}