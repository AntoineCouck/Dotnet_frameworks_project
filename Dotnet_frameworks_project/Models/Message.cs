using Dotnet_frameworks_project.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_frameworks_project.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Sender")]
        public string? SenderId { get; set; }
        public ApplicationUser? Sender { get; set; }

        public ApplicationUser? Receiver { get; set; }
        [ForeignKey("Receiver")]
        public string? ReceiverId { get; set; }


    }
}
