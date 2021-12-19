namespace Dotnet_frameworks_project.Models
{
    public class FollowUp_patients
    {
        public int Id { get; set; }

        public string? FollowUpId { get; set; }

        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }

        public FollowUp_type? FollowUpType { get; set; }

    }
}
