using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Commands.Appointments.AddNewAppoinment
{
    public class AddNewAppointmentCommandHandler : IRequestHandler<AddNewAppointmentCommand, Appointment>
    {
        private readonly IAppointmentRepositories _appointmentRepositories;

        public AddNewAppointmentCommandHandler(IAppointmentRepositories appointmentRepositories)
        {
            _appointmentRepositories = appointmentRepositories;
        }

        public async Task<Appointment> Handle(AddNewAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment newAppointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                BarberId = request.NewAppointment.CustomerId,
                AppointmentDate = request.NewAppointment.AppointmentDate,
                Service = request.NewAppointment.Service,
                Price = request.NewAppointment.Price,
                IsCancelled = request.NewAppointment.IsCancelled,
            };

            await _appointmentRepositories.AddNewAppoinment(newAppointment, request.UserName, cancellationToken);

            return newAppointment;
        }
    }
}
