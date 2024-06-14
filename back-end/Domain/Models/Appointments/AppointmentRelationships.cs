
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
    }

}
