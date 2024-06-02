using Domain.Models.Appointments;
using Infrastructure.Repositories.Appointments;
using MediatR;

namespace Application.Queries.Appointments.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<List<Appointment>>
    {

    }
}
