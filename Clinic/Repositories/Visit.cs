
namespace Clinic.Models
{
    public class Visit
    {
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = "";
    }
}