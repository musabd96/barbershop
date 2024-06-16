using Domain.Models.Barbers;
using Domain.Models.Users;
using Infrastructure.Repositories.Barbers;
using MediatR;

namespace Application.Commands.Barbers.AddNewBarber
{
    public class AddNewBarberCommandHandler : IRequestHandler<AddNewBarberCommand, Barber>
    {
        private readonly IBarberRepositories _barberRepositories;

        public AddNewBarberCommandHandler(IBarberRepositories barberRepositories)
        {
            _barberRepositories = barberRepositories;
        }

        public async Task<Barber> Handle(AddNewBarberCommand request, CancellationToken cancellationToken)
        {
            // Password hashing using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password);

            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.Username.ToLowerInvariant(),
                PasswordHash = hashedPassword,
            };

            Barber newBarber = new Barber()
            {
                Id = Guid.NewGuid(),
                FirstName = request.NewBarber.FirstName,
                LastName = request.NewBarber.LastName,
                Email = request.NewBarber.Email,
                Phone = request.NewBarber.Phone,
            };

            await _barberRepositories.AddNewBarber(userToCreate, newBarber, cancellationToken);

            return newBarber;
        }
    }
}
