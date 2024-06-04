using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<Appointment>
    {
        public DeleteAppointmentCommand(Guid appointmentId)
        {
            Id = appointmentId;
        }

        public Guid Id { get; }
    }
}
