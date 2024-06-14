

using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Appointment>
    {
        private readonly IAppointmentRepositories _appointmentRepositories;

        public GetAppointmentByIdQueryHandler(IAppointmentRepositories appointmentRepositories)
        {
            _appointmentRepositories = appointmentRepositories;
        }

        public async Task<Appointment> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepositories.GetAppointmentById(request.Id, request.UserName, cancellationToken);

            return appointment;
        }
    }
}
