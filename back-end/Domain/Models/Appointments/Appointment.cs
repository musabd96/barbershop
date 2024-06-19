using Domain.Models.Barbers;
using Domain.Models.Customers;
using System.Text.Json.Serialization;

namespace Domain.Models.Appointments
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public Guid BarberId { get; set; }
        [JsonIgnore]
        public Barber Barber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Service { get; set; }
        public decimal Price { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
