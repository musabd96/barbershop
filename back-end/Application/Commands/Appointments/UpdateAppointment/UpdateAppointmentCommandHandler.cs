using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Appointment>
    {
        private readonly IAppointmentRepositories _appointmentRepositories;

        public UpdateAppointmentCommandHandler(IAppointmentRepositories appointmentRepositories)
        {
            _appointmentRepositories = appointmentRepositories;
        }
        public async Task<Appointment> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointmentToUpdate = await _appointmentRepositories.UpdateAppointment(request.AppointmentId,
                                                                                       request.Username,
                                                                                       request.AppointmentDto.BarberId,
                                                                                       request.AppointmentDto.AppointmentDate,
                                                                                       request.AppointmentDto.Service,
                                                                                       request.AppointmentDto.Price,
                                                                                       request.AppointmentDto.IsCancelled,
                                                                                       cancellationToken);

            return appointmentToUpdate;
        }
    }
}
