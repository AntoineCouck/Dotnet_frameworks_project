using System.ComponentModel.DataAnnotations;

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


}
