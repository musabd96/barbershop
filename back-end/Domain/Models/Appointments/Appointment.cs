
namespace Domain.Models.Appointments
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BarberId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Service { get; set; }
        public decimal Price { get; set; }
        public bool IsCancelled { get; set; }
    }
}
