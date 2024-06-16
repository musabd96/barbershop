

using Domain.Models.Appointments;
using Domain.Models.Barbers;

namespace Domain.Models.BarberShops
{
    public class BarberShopRelationships
    {
        public class BarberShopBarber()
        {
            public Guid BarberShopId { get; set; }
            public BarberShop BarberShop { get; set; }

            public Guid BarberId { get; set; }
            public Barber Barber { get; set; }

        }
    }
}
