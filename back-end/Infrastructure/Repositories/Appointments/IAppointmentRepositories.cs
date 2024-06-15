using Domain.Models.Appointments;

namespace Infrastructure.Repositories.Appointments
{
    public interface IAppointmentRepositories
    {
        Task<List<Appointment>> GetAllAppointments(string userName, CancellationToken cancellationToken);
        Task<Appointment> GetAppointmentById(Guid id, string userName, CancellationToken cancellationToken);
        Task<Appointment> AddNewAppoinment(Appointment newAppointment, string userName, CancellationToken cancellationToken);
        Task<Appointment> UpdateAppointment(Guid appointmentId,
                                             string userName,
                                             Guid BarberId,
                                             DateTime AppointmentDate,
                                             string Service,
                                             decimal Price,
                                             bool IsCancelled
                                            , CancellationToken cancellationToken);
        Task<Appointment> DeleteAppointment(Guid appointmentId, string userName, CancellationToken cancellationToken);
    }
}