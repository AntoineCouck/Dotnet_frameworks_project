namespace Dotnet_frameworks_project.Models
{
    public class PassedTests
    {
        public int Id { get; set; }


        public Patient ? Patient { get; set; }


        public Test ? Test { get; set; }


        public string ? TestId { get; set; }


        public int ? PatientId { get; set; } 

    }
}
