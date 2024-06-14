using Domain.Models.Customers;
using Domain.Models.Users;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterUserCommandValidator _validator;

        public RegisterUserCommandHandler(IUserRepository userRepository, RegisterUserCommandValidator validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }
        public Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registerCommandValidation = _validator.Validate(request);

            if (!registerCommandValidation.IsValid)
            {
                var allErrors = registerCommandValidation.Errors.ConvertAll(errors => errors.ErrorMessage);

                throw new ArgumentException("Registration error: " + string.Join("; ", allErrors));
            }

            // Password hashing using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password);


            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.Username.ToLowerInvariant(),
                PasswordHash = hashedPassword,
            };

            var customerToRegister = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = request.NewCustomer.FirstName,
                LastName = request.NewCustomer.LastName,
                Email = request.NewCustomer.Email,
                Phone = request.NewCustomer.Phone,
            };

            var createdUser = _userRepository.RegisterUser(userToCreate, customerToRegister, cancellationToken);

            return createdUser;
        }
    }
}
