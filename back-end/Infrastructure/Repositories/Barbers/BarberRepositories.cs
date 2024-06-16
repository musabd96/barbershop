using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Domain.Models.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Barbers
{
    public class BarberRepositories : IBarberRepositories
    {
        private readonly AppDbContext _appDbContext;
        public BarberRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Barber>> GetAllBarbers(CancellationToken cancellationToken)
        {
            try
            {
                List<Barber> allBarbers = await _appDbContext.Barber.ToListAsync(cancellationToken);

                return allBarbers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all barbers  from the database", ex);
            }
        }

        public async Task<Barber> GetBarberById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Barber? wantedBarber = await _appDbContext.Barber
                    .FirstOrDefaultAsync(barber => barber.Id == id, cancellationToken);

                return wantedBarber!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the barber from the database", ex);
            }
        }

        public async Task<Barber> AddNewBarber(User userToCreate, Barber newBarber, string barbershopName, CancellationToken cancellationToken)
        {
            try
            {
                _appDbContext.Barber.Add(newBarber);
                _appDbContext.User.Add(userToCreate);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _appDbContext.UserBarbers.Add(
                    new UserRelationships.UserBarber
                    {
                        UserId = userToCreate.Id,
                        BarberId = newBarber.Id
                    });
                await _appDbContext.SaveChangesAsync(cancellationToken);

                var barberShopInfo = _appDbContext.BarberShop.FirstOrDefault(bS => bS.Name == barbershopName);

                _appDbContext.BarberShopBarbers.Add(
                    new BarberShopRelationships.BarberShopBarber
                    {
                        BarberShopId = barberShopInfo!.Id,
                        BarberId = newBarber.Id
                    });
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(newBarber);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the barber to the database", ex);
            }
        }

        public async Task<Barber> UpdateBarber(Guid barberId, string Name, string lastName, string email, string phone, CancellationToken cancellationToken)
        {
            try
            {
                Barber barberToUpdate = await _appDbContext.Barber.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToUpdate != null)
                {
                    barberToUpdate.FirstName = Name;
                    barberToUpdate.LastName = lastName;
                    barberToUpdate.Email = email;
                    barberToUpdate.Phone = phone;

                    _appDbContext.Barber.Update(barberToUpdate);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return barberToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the barber to the database", ex);
            }
        }

        public async Task<Barber> DeleteBarber(Guid barberId, CancellationToken cancellationToken)
        {
            try
            {
                Barber barberToDelete = await _appDbContext.Barber.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToDelete != null)
                {


                    _appDbContext.Barber.Remove(barberToDelete);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return barberToDelete;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the barber to the database", ex);
            }
        }
    }
}
