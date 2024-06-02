using Domain.Models.Appointments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed fake data in Appointment table
            // Configure the precision and scale for the Price property
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            DbSeed.SeedAppointments(modelBuilder);
        }
    }
}
