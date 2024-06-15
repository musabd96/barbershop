


using Domain.Models.Appointments;

namespace Infrastructure.Services
{
    public interface IEmailService
    {
        Task<string> SendBookingConfirmed(string userName, Appointment newAppointment, CancellationToken cancellationToken);
    }
}
