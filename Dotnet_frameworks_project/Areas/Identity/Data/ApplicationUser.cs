using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity;

namespace Dotnet_frameworks_project.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public string Firstname { get; set; }
    public string Lastname { get; set; }


}

