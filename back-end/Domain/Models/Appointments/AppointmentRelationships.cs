using Domain.Models.Barbers;
using Domain.Models.Customers;

namespace Domain.Models.Appointments
{
    public class AppointmentRelationships
    {
        public class AppointmentCustomer()
        {
            public Guid AppointmentId { get; set; }
            public Appointment Appointment { get; set; }

            public Guid CustomerId { get; set; }
            public Customer Customer { get; set; }
        }

        public class AppointmentBarber()
        {
            public Guid AppointmentId { get; set; }
            public Appointment Appointment { get; set; }

            public Guid BarberId { get; set; }
            public Barber Barber { get; set; }

        }
    }

}
