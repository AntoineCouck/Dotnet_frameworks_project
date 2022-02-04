using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_frameworks_project.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public bool AcceptCookies { get; set; }

    [ForeignKey("Language")]
    public string? LanguageId { get; set; }
    public Language? Language { get; set; }

}

public class ApplicationUserViewModel
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Language { get; set; }

    public bool? AcceptCookies { get; set; }
    public string? PhoneNumber { get; set; }
    public bool Lockout { get; set; }
    public bool Logopedist { get; set; }
    public bool Parents { get; set; }
    public bool Mutualiteit { get; set; }
    public bool Admin { get; set; }
}


