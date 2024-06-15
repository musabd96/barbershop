

using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Appointment>
    {
        private readonly IAppointmentRepositories _appointmentRepositories;

        public DeleteAppointmentCommandHandler(IAppointmentRepositories appointmentRepositories)
        {
            _appointmentRepositories = appointmentRepositories;
        }

        public async Task<Appointment> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepositories.DeleteAppointment(request.Id, request.UserName, cancellationToken);

            return appointment;
        }
    }
}
