

using Domain.Models.Appointments;
using Domain.Models.Barbers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DbSeed
    {
        public static void SeedAppointments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    BarberId = Guid.NewGuid(),
                    AppointmentDate = new DateTime(2024, 06, 10),
                    Service = "Cutting",
                    Price = 20.00m,
                    IsCancelled = false,
                }
            );
        }

        public static void SeedBarbers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barber>().HasData(
                new Barber
                {
                    Id = Guid.NewGuid(),
                    Name = "Mustafa"
                }
            );
        }
    }
}
