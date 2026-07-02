
namespace Clinic.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int VisitId { get; set; }
        public string MedicationName { get; set; } = "";
        public string Dosage { get; set; } = "";
        public string Instructions { get; set; } = "";
    }
}