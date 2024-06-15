using Domain.Models.Appointments;
using Domain.Models.Barbers;

namespace Infrastructure.Repositories.Barbers
{
    public interface IBarberRepositories
    {
        Task<List<Barber>> GetAllBarbers(CancellationToken cancellationToken);
        Task<Barber> GetBarberById(Guid id, CancellationToken cancellationToken);
        Task<Barber> AddNewBarber(Barber newABarber, CancellationToken cancellationToken);
        Task<Barber> UpdateBarber(Guid barberId, string firstName, string lastName, string email, string phone, CancellationToken cancellationToken);
        Task<Barber> DeleteBarber(Guid barberId, CancellationToken cancellationToken);
    }
}
