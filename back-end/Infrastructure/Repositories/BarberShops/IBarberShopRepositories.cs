
using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using static Domain.Models.BarberShops.BarberShopRelationships;

namespace Infrastructure.Repositories.BarberShops
{
    public interface IBarberShopRepositories
    {
        Task<List<BarberShop>> GetAllBarberShops(CancellationToken cancellationToken);
        Task<List<Barber>> GetAllBarberShopStaff(string barberShopName, CancellationToken cancellationToken);
        Task<BarberShop> GetBarberShopById(Guid id, CancellationToken cancellationToken);
        Task<BarberShop> AddNewBarberShop(BarberShop newBarberShop, CancellationToken cancellationToken);
        Task<BarberShop> UpdateBarberShop(Guid barberShopId, string name,
                                          string email, string phone,
                                          string address, string zipCode,
                                          string city, CancellationToken cancellationToken);
        Task<BarberShop> DeleteBarberShop(Guid id, CancellationToken cancellationToken);
    }
}
