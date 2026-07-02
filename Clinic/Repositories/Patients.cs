using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public int UserId { get; set; }

      
        public string FirstName { get; set; } = "";

      
        public string LastName { get; set; } = "";

        public string Gender { get; set; } = "";

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";
    }
}