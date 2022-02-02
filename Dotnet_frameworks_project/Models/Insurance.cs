using System.ComponentModel.DataAnnotations;

namespace Dotnet_frameworks_project.Models
{
    public class Insurance
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]

        public int PhoneNumber { get; set; }


    }

    public partial class InsuranceIndexViewModel
    {
        public string? NameFilter { get; set; }

        public List<Insurance>? Insurances { get; set; }
    }
}
