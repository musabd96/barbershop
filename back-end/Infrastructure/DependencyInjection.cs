using Infrastructure.Repositories.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Database;
using Infrastructure.Repositories.Barbers;
using Infrastructure.Repositories.Customers;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.Authorization;
using Infrastructure.Services;
using Infrastructure.Repositories.BarberShops;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppointmentRepositories, AppointmentRepositories>();
            services.AddScoped<IBarberRepositories, BarberRepositories>();
            services.AddScoped<IBarberShopRepositories, BarberShopRepositories>();
            services.AddScoped<ICustomerRepositories, CustomerRepositories>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 36)));
            });
            return services;
        }
    }
}
