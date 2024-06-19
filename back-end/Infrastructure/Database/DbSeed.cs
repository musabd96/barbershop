using Domain.Models.Appointments;
using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DbSeed
    {
        public static void SeedAdminUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
                }
            );
        }
    }
}
