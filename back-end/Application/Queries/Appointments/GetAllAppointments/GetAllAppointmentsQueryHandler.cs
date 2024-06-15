using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAllAppointments
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, List<Appointment>>
    {
        private readonly IAppointmentRepositories _appointmentRepositories;

        public GetAllAppointmentsQueryHandler(IAppointmentRepositories appointmentRepositories)
        {
            _appointmentRepositories = appointmentRepositories;
        }

        public async Task<List<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> AllAppointments = await _appointmentRepositories.GetAllAppointments(request.UserName, cancellationToken);
            return AllAppointments ?? throw new InvalidOperationException("No appointments were found");
        }
    }
}
