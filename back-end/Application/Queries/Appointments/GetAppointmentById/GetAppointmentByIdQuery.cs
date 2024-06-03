
using Domain.Models.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<Appointment>
    {
        public GetAppointmentByIdQuery(Guid appointmentId)
        {
            Id = appointmentId;
        }

        public Guid Id { get; set; }
    }
}
