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

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<User> User { get; set; }

        // Define bridge tables for relationships
        public DbSet<UserRelationships.UserCustomer> UserCustomers { get; set; }
        public DbSet<UserRelationships.UserBarber> UserBarbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            DbSeed.SeedAdminUser(modelBuilder);

            // Configure Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Appointment>()
               .HasOne(a => a.Customer)
               .WithMany(c => c.Appointments)
               .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Barber)
                .WithMany(b => b.Appointments)
                .HasForeignKey(a => a.BarberId);

            // Configure Barber entity
            modelBuilder.Entity<Barber>()
                .HasOne(b => b.Barbershop)
                .WithMany(bs => bs.Barbers)
                .HasForeignKey(b => b.BarbershopId);

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
