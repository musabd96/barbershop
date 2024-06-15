using Domain.Models.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<List<Appointment>>
    {
        public GetAllAppointmentsQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}
