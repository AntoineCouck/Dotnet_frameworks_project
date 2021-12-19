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
}
