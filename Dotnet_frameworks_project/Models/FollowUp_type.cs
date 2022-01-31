using System.ComponentModel.DataAnnotations;

namespace Dotnet_frameworks_project.Models
{
    public class FollowUp_type
    {


        [Key]
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        [Required]
        public int Number_of_requiredsessions { get; set; }


    }

    public partial class FollowUpTypeIndexViewModel
    {
        public string ? NameFilter { get; set; }

        public List<FollowUp_type> ? followUp_Types { get; set; }
    }
}
