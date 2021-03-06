using Dotnet_frameworks_project.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_frameworks_project.Models;

public class Patient
{

    [ForeignKey("applicationUser")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Voornaam")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Achternaam")]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Geboortedatum")]
    public DateTime Birthday { get; set; }

    [Required]
    [Phone]
    public string ParentsPhone { get; set; }


    public int? LeftSessions { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? user { get; set; }


    public Insurance? Insurance { get; set; }

    [ForeignKey("Insurance")]
    public string? InsuranceId { get; set; }


    [ForeignKey("Account")]
    public string? AccountId { get; set; }
    public ApplicationUser? Account { get; set; }


    [ForeignKey("Gender")]
    public char? GenderId { get; set; }

    public Gender? Gender { get; set; }



}

public partial class PatientViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Voornaam")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Achternaam")]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Geboortedatum")]
    public DateTime Birthday { get; set; }

    [Required]
    [Phone]
    public string ParentsPhone { get; set; }


    public int? LeftSessions { get; set; }

    public int? AddSessions { get; set; }

    public int? RemoveSessions { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? user { get; set; }


    public Insurance? Insurance { get; set; }

    [ForeignKey("Insurance")]
    public string? InsuranceId { get; set; }


    [ForeignKey("Gender")]
    public char? GenderId { get; set; }

    public Gender? Gender { get; set; }

}

public partial class PatientIndexViewModel
{

    public string? NameFilter { get; set; }
    public char GenderFilter { get; set; }
    public List<Patient>? FilteredStudents { get; set; }
    public SelectList? ListGenders { get; set; }

}
