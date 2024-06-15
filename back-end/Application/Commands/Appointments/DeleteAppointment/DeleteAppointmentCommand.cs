using Domain.Models.Appointments;
using MediatR;

namespace Application.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<Appointment>
    {
        public DeleteAppointmentCommand(Guid appointmentId, string userName)
        {
            Id = appointmentId;
            UserName = userName;
        }

        public Guid Id { get; }
        public string UserName { get; }
    }
}
