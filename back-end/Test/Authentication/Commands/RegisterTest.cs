using Application.Commands.Users.Register;
using Application.Dtos;
using Domain.Models.Customers;
using Domain.Models.Users;
using Infrastructure.Repositories.Users;
using Moq;
using NUnit.Framework;
using static Domain.Models.Users.UserRelationships;

namespace Test.Authentication.Commands
{
    [TestFixture]
    public class RegisterTest
    {
        private RegisterUserCommandHandler _handler;
        private Mock<IUserRepository> _userRepository;
        private RegisterUserCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _validator = new RegisterUserCommandValidator(_userRepository.Object);
            _handler = new RegisterUserCommandHandler(_userRepository.Object, _validator);
        }

        protected void SetupMockDbContext(List<User> users, List<Customer> customers)
        {
            _userRepository.Setup(repo => repo.GetAllUsers())
                .ReturnsAsync(users);

            _userRepository.Setup(repo => repo.RegisterUser(It.IsAny<User>(), It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
                .Callback((User user, Customer customer, CancellationToken cancellationToken) =>
                {
                    users.Add(user);
                    customers.Add(customer);
                })
                .Returns((User user, Customer customer, CancellationToken cancellationToken) =>
                {
                    return Task.FromResult(user);
                });
        }


        [Test]
        public async Task RegisterUser_ValidCommand_Successful()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test1", PasswordHash = "Test111!!" },
                new User { Id = Guid.NewGuid(), Username = "Test2", PasswordHash = "Test222!!" }
            };

            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "test1", LastName = "test1", Email = "test1@test.com",  Phone = "0712345678" },
                new Customer { Id = Guid.NewGuid(), FirstName = "test2", LastName = "test2", Email = "test2@test.com",  Phone = "0712345678" },
            };

            SetupMockDbContext(users, customers);

            var user = new UserDto
            {
                Username = "testuser",
                Password = "Password123!",
            };

            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Phone = "0712345678"
            };

            var registerCommand = new RegisterUserCommand(user, customer);

            // Act
            var result = await _handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Username, Is.EqualTo(user.Username));
        }
        [Test]
        public async Task RegisterUser_InvalidCommand_ThrowsArgumentException()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test1", PasswordHash = "Test111!!" },
                new User { Id = Guid.NewGuid(), Username = "Test2", PasswordHash = "Test222!!" }
            };

            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "test1", LastName = "test1", Email = "test1@test.com",  Phone = "0712345678" },
                new Customer { Id = Guid.NewGuid(), FirstName = "test2", LastName = "test2", Email = "test2@test.com",  Phone = "0712345678" },
            };

            SetupMockDbContext(users, customers);

            var user = new UserDto
            {
                Username = "Test1", // taken user
                Password = "Password123!",
            };

            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Phone = "0712345678"
            };

            var registerCommand = new RegisterUserCommand(user, customer);

            // Act and Assert
            var ex = NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _handler.Handle(registerCommand, CancellationToken.None);
            });

            // Assert
            NUnit.Framework.Assert.That(ex!.Message, Is.EqualTo("Registration error: Username is already taken."));
        }


    }
}
