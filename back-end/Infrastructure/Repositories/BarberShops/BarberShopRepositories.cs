using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories.BarberShops
{
    public class BarberShopRepositories : IBarberShopRepositories
    {
        private readonly AppDbContext _appDbContext;
        public BarberShopRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<BarberShop>> GetAllBarberShops(CancellationToken cancellationToken)
        {
            try
            {
                List<BarberShop> allBarberShops = await _appDbContext.BarberShops.ToListAsync(cancellationToken);

                return allBarberShops;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all barbershops  from the database", ex);
            }
        }

        public async Task<List<Barber>> GetAllBarberShopStaff(string barberShopName, CancellationToken cancellationToken)
        {
            BarberShop barberShop = await _appDbContext.BarberShops.FirstOrDefaultAsync(barberShop => barberShop.Name == barberShopName);

            List<Barber> allBarbers = await _appDbContext.Barbers.Where(b => b.BarbershopId == barberShop.Id)
                                                                 .ToListAsync(cancellationToken);

            return allBarbers;
        }

        public async Task<BarberShop> GetBarberShopById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                BarberShop? wantedBarberShop = await _appDbContext.BarberShops
                    .FirstOrDefaultAsync(barber => barber.Id == id, cancellationToken);

                return wantedBarberShop!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the barbershop from the database", ex);
            }
        }

        public async Task<BarberShop> AddNewBarberShop(BarberShop newBarberShop, CancellationToken cancellationToken)
        {
            try
            {
                _appDbContext.BarberShops.Add(newBarberShop);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(newBarberShop);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the barbershop to the database", ex);
            }
        }

        public async Task<BarberShop> UpdateBarberShop(Guid barberShopId, string name, string email, string phone,
                                                       string address, string zipCode, string city, CancellationToken cancellationToken)
        {
            try
            {
                BarberShop barberShopToUpdate = await _appDbContext.BarberShops.FirstOrDefaultAsync(barberShop => barberShop.Id == barberShopId);

                if (barberShopToUpdate != null)
                {
                    barberShopToUpdate.Name = name;
                    barberShopToUpdate.Email = email;
                    barberShopToUpdate.Phone = phone;
                    barberShopToUpdate.Address = address;
                    barberShopToUpdate.ZipCode = zipCode;
                    barberShopToUpdate.City = city;


                    _appDbContext.BarberShops.Update(barberShopToUpdate);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return barberShopToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the Barbershop to the database", ex);
            }
        }

        public async Task<BarberShop> DeleteBarberShop(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                BarberShop barberShopToDelete = await _appDbContext.BarberShops.FirstOrDefaultAsync(barberShop => barberShop.Id == id);

                if (barberShopToDelete != null)
                {


                    _appDbContext.BarberShops.Remove(barberShopToDelete);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return barberShopToDelete;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the barbershop to the database", ex);
            }
        }

    }
}
