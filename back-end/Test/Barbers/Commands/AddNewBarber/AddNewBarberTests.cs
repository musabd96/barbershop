using Application.Commands.Barbers.AddNewBarber;
using Application.Dtos;
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

        protected void SetupMockDbContext(List<Domain.Models.Barbers.Barber> barbers)
        {
            _barberRepositories.Setup(repo => repo.AddNewBarber(It.IsAny<Domain.Models.Barbers.Barber>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Models.Barbers.Barber barber,
                    CancellationToken cancellationToken) => barbers.Add(barber))
                .Returns((Domain.Models.Barbers.Barber barber,
                    CancellationToken cancellationToken) => Task.FromResult(barber));
        }

        [Test]
        public async Task Handle_Validbarber_ReturnsNewbarber()
        {
            // Arrange
            var barber = new List<Domain.Models.Barbers.Barber>();
            SetupMockDbContext(barber);

            var newbarber = new BarberDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };


            var addbarberCommand = new AddNewBarberCommand(newbarber);

            // Act
            var result = await _handler!.Handle(addbarberCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(newbarber.Id));
        }
    }
}
