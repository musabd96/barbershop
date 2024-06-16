

using Domain.Models.Appointments;

namespace Domain.Models.Barbers
{
    public class BarberRelationships
    {
        public class AppointmentBarber()
        {
            public Guid AppointmentId { get; set; }
            public Appointment Appointment { get; set; }

            public Guid BarberId { get; set; }
            public Barber Barber { get; set; }

        }
    }
}
