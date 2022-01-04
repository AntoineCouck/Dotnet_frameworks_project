﻿using Dotnet_frameworks_project.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using StudentenBeheer.Services;

namespace Dotnet_frameworks_project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ApplicationController : Controller
    {
        protected readonly ApplicationUser _user;
        protected readonly ApplicationContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger<ApplicationController> _logger;
        protected readonly IStringLocalizer<PatientsController> _Localizer;

        protected ApplicationController(ApplicationContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        ILogger<ApplicationController> logger, IStringLocalizer<PatientsController> localizer, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _Localizer = localizer;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            //string? userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            //if (userName == null)
            //    userName = "-";
            //_user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            _user = SessionUser.GetUser(httpContextAccessor.HttpContext);
        }
    }
}
