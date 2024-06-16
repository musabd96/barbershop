
using Domain.Models.BarberShops;

namespace Infrastructure.Repositories.BarberShops
{
    public interface IBarberShopRepositories
    {
        Task<List<BarberShop>> GetAllBarberShops(CancellationToken cancellationToken);
        Task<BarberShop> GetBarberShopById(Guid id, CancellationToken cancellationToken);
        Task<BarberShop> AddNewBarberShop(BarberShop newBarberShop, CancellationToken cancellationToken);
        Task<BarberShop> UpdateBarberShop(Guid barberShopId, string name,
                                          string email, string phone,
                                          string address, string zipCode,
                                          string city, CancellationToken cancellationToken);
        Task<BarberShop> DeleteBarberShop(Guid id, CancellationToken cancellationToken);
    }
}
