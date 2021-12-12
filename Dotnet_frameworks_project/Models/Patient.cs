using Dotnet_frameworks_project.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_frameworks_project.Models;

public class Patient
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
   
    
    public int ? LeftSessions { get; set; } 


   public string ? FollowUp_Name { get; set; } 
   public FollowUp_type FollowUpType { get; set; }

   
  
    public string ? UserId { get; set; }
    public ApplicationUser ? user { get; set; }

}
