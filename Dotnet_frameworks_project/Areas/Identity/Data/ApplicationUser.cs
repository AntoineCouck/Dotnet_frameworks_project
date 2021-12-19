using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dotnet_frameworks_project.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public string Firstname { get; set; }

    
    public string Lastname { get; set; }


}

