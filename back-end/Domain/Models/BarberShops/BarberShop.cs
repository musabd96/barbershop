
using Domain.Models.Barbers;
using System.Text.Json.Serialization;

namespace Domain.Models.BarberShops
{
    public class BarberShop
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Barber> Barbers { get; set; } = new List<Barber>();
    }
}
