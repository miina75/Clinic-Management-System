// Models/Bill.cs
namespace Clinic.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int VisitId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = "";
        public DateTime BillDate { get; set; }
    }
}