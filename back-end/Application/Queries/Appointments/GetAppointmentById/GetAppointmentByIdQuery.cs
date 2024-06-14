
using Domain.Models.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<Appointment>
    {
        public GetAppointmentByIdQuery(Guid appointmentId, string userName)
        {
            Id = appointmentId;
            UserName = userName;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
