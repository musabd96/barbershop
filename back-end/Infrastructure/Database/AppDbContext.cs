using Domain.Models.Appointments;
using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using Domain.Models.Customers;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Barber> Barber { get; set; }
        public DbSet<BarberShop> BarberShop { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<User> User { get; set; }

        // Define bridge tables for relationships
        public DbSet<UserRelationships.UserCustomer> UserCustomers { get; set; }
        public DbSet<UserRelationships.UserBarber> UserBarbers { get; set; }
        public DbSet<AppointmentRelationships.AppointmentCustomer> AppointmentCustomers { get; set; }
        public DbSet<AppointmentRelationships.AppointmentBarber> AppointmentBarbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            // Seed data
            DbSeed.SeedAppointments(modelBuilder);
            DbSeed.SeedBarberShops(modelBuilder);
            //DbSeed.SeedCustomers(modelBuilder);

            // Define relationships between User and Customer
            modelBuilder.Entity<UserRelationships.UserCustomer>()
                .HasKey(uc => new { uc.UserId, uc.CustomerId });

            modelBuilder.Entity<UserRelationships.UserCustomer>()
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserRelationships.UserCustomer>()
                .HasOne(uc => uc.Customer)
                .WithMany()
                .HasForeignKey(uc => uc.CustomerId);

            // Define relationships between Appointment and Customer
            modelBuilder.Entity<AppointmentRelationships.AppointmentCustomer>()
                .HasKey(ac => new { ac.AppointmentId, ac.CustomerId });

            modelBuilder.Entity<AppointmentRelationships.AppointmentCustomer>()
                .HasOne(ac => ac.Appointment)
                .WithMany()
                .HasForeignKey(ac => ac.AppointmentId);

            modelBuilder.Entity<AppointmentRelationships.AppointmentCustomer>()
                .HasOne(ac => ac.Customer)
                .WithMany()
                .HasForeignKey(ac => ac.CustomerId);

            // Define relationships between Appointment and barber
            modelBuilder.Entity<AppointmentRelationships.AppointmentBarber>()
                .HasKey(ac => new { ac.AppointmentId, ac.BarberId });

            modelBuilder.Entity<AppointmentRelationships.AppointmentBarber>()
                .HasOne(ac => ac.Appointment)
                .WithMany()
                .HasForeignKey(ac => ac.AppointmentId);

            modelBuilder.Entity<AppointmentRelationships.AppointmentBarber>()
                .HasOne(ac => ac.Barber)
                .WithMany()
                .HasForeignKey(ac => ac.BarberId);

            // Define relationships between User and Barber
            modelBuilder.Entity<UserRelationships.UserBarber>()
                .HasKey(ub => new { ub.UserId, ub.BarberId });

            modelBuilder.Entity<UserRelationships.UserBarber>()
                .HasOne(ub => ub.User)
                .WithMany()
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserRelationships.UserBarber>()
                .HasOne(ub => ub.Barber)
                .WithMany()
                .HasForeignKey(ub => ub.BarberId);
        }
    }
}
