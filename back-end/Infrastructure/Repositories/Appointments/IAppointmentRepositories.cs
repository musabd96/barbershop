using Domain.Models.Appointments;

namespace Infrastructure.Repositories.Appointments
{
    public interface IAppointmentRepositories
    {
        Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken);
        Task<Appointment> GetAppointmentById(Guid id, CancellationToken cancellationToken);
    }
}
