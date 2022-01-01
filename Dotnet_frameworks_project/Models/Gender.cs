using System.ComponentModel.DataAnnotations;

namespace Dotnet_frameworks_project.Models
{
    public class Gender
    {

        [Required]
        public char ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

    }
}
