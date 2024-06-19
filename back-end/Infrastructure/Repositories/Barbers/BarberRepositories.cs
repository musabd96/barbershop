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
                List<Barber> allBarbers = await _appDbContext.Barbers.ToListAsync(cancellationToken);

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
                Barber? wantedBarber = await _appDbContext.Barbers
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
                var barberShopInfo = _appDbContext.BarberShops.FirstOrDefault(bS => bS.Name == barbershopName);
                newBarber.BarbershopId = barberShopInfo.Id;

                _appDbContext.Barbers.Add(newBarber);
                _appDbContext.User.Add(userToCreate);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _appDbContext.UserBarbers.Add(
                    new UserRelationships.UserBarber
                    {
                        UserId = userToCreate.Id,
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
                Barber barberToUpdate = await _appDbContext.Barbers.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToUpdate != null)
                {
                    barberToUpdate.FirstName = Name;
                    barberToUpdate.LastName = lastName;
                    barberToUpdate.Email = email;
                    barberToUpdate.Phone = phone;

                    _appDbContext.Barbers.Update(barberToUpdate);
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
                Barber barberToDelete = await _appDbContext.Barbers.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToDelete != null)
                {


                    _appDbContext.Barbers.Remove(barberToDelete);
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
