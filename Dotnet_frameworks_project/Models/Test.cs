using System.ComponentModel.DataAnnotations;

namespace Dotnet_frameworks_project.Models
{
    public class Test
    {

        [Key]
        public string Name { get; set; }


        public string? Description { get; set; }

        [Required]
        public string Usage { get; set; }

        [Required]
        public int Min_age { get; set; }

        [Required]
        public int Max_age { get; set; }



    }
}
