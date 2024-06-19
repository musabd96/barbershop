using Domain.Models.Appointments;
using Domain.Models.BarberShops;
using System.Text.Json.Serialization;

namespace Domain.Models.Barbers
{
    public class Barber
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BarbershopId { get; set; }
        [JsonIgnore]
        public BarberShop Barbershop { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
