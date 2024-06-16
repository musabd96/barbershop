using Application.Commands.Barbers.AddNewBarber;
using Application.Dtos;
using Domain.Models.Users;
using Infrastructure.Repositories.Barbers;
using Moq;
using NUnit.Framework;


namespace Test.Barber.Commands.AddNewBarber
{
    [TestFixture]
    public class AddNewBarberTests
    {
        private AddNewBarberCommandHandler _handler;
        private Mock<IBarberRepositories> _barberRepositories;

        [SetUp]
        public void Setup()
        {
            _barberRepositories = new Mock<IBarberRepositories>();
            _handler = new AddNewBarberCommandHandler(_barberRepositories.Object);
        }

        protected void SetupMockDbContext(List<User> users, List<Domain.Models.Barbers.Barber> barbers)
        {
            _barberRepositories.Setup(repo => repo.AddNewBarber(It.IsAny<User>(), It.IsAny<Domain.Models.Barbers.Barber>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Callback((User user, Domain.Models.Barbers.Barber barber, string barbershopName, CancellationToken cancellationToken) =>
                {
                    users.Add(user);
                    barbers.Add(barber);
                })
                .Returns((User user, Domain.Models.Barbers.Barber barber, string barbershopName, CancellationToken cancellationToken) =>
                {
                    return Task.FromResult(barber);
                });
        }

        [Test]
        public async Task Handle_Validbarber_ReturnsNewbarber()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test1", PasswordHash = "Test111!!" },
                new User { Id = Guid.NewGuid(), Username = "Test2", PasswordHash = "Test222!!" }
            };

            var barbers = new List<Domain.Models.Barbers.Barber>
            {
                new Domain.Models.Barbers.Barber { Id = Guid.NewGuid(), FirstName = "test1", LastName = "test1", Email = "test1@test.com",  Phone = "0712345678" },
                new Domain.Models.Barbers.Barber { Id = Guid.NewGuid(), FirstName = "test2", LastName = "test2", Email = "test2@test.com",  Phone = "0712345678" },
            };

            SetupMockDbContext(users, barbers);

            var user = new UserDto
            {
                Username = "testuser",
                Password = "Password123!",
            };

            var barber = new BarberDto
            {
                Id = Guid.NewGuid(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Phone = "0712345678"
            };

            var barberShopName = "BarsherShop";

            var command = new AddNewBarberCommand(user, barber, barberShopName);

            // Act
            var result = await _handler!.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.FirstName, Is.EqualTo(barber.FirstName));
        }
    }
}
