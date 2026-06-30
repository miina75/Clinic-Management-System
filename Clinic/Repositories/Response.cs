namespace Clinic.Models
{
    public class Response
    {
        public Boolean Status { get; set; }
        public String Message { get; set; }
        public object? Data { get; set; }
    }
}