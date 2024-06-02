

using Domain.Models.Appointments;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Appointments
{
    public class AppointmentRepositories : IAppointmentRepositories
    {
        private readonly AppDbContext _appDbContext;
        public AppointmentRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Appointment>> GetAllAppointments(CancellationToken cancellationToken)
        {
            return await _appDbContext.Appointment.ToListAsync(cancellationToken);
        }
    }
}
