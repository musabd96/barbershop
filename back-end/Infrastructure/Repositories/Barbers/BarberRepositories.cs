using Domain.Models.Barbers;
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

        public async Task<Barber> AddNewBarber(Barber newBarber, CancellationToken cancellationToken)
        {
            try
            {
                _appDbContext.Barber.Add(newBarber);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return newBarber;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the barber to the database", ex);
            }
        }

        public async Task<Barber> UpdateBarber(Guid barberId, string Name, CancellationToken cancellationToken)
        {
            try
            {
                Barber barberToUpdate = await _appDbContext.Barber.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToUpdate != null)
                {
                    barberToUpdate.Name = Name;

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
                Barber barberToUpdate = await _appDbContext.Barber.FirstOrDefaultAsync(barber => barber.Id == barberId);

                if (barberToUpdate != null)
                {


                    _appDbContext.Barber.Remove(barberToUpdate);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return barberToUpdate;
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
