
using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }

        
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string Specialty { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
    }
}