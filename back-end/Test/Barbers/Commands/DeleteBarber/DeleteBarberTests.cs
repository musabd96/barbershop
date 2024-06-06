using Application.Commands.Barbers.DeleteBarber;
using Infrastructure.Repositories.Barbers;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Commands.DeleteBarber
{
    [TestFixture]
    public class DeleteBarberTests
    {
        private DeleteBarberCommandHandler _handler;
        private Mock<IBarberRepositories> _barberRepositories;

        [SetUp]
        public void Setup()
        {
            _barberRepositories = new Mock<IBarberRepositories>();
            _handler = new DeleteBarberCommandHandler(_barberRepositories.Object);
        }

        protected void SetupMockDbContext(List<Domain.Models.Barbers.Barber> barbers)
        {
            _barberRepositories.Setup(repo => repo.DeleteBarber(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid barberId, CancellationToken cancellationToken) =>
                {
                    var barberToDelete = barbers.FirstOrDefault(barber => barber.Id == barberId);
                    if (barberToDelete != null)
                    {
                        barbers.Remove(barberToDelete);
                        return Task.FromResult(barberToDelete);
                    }
                    return Task.FromResult<Domain.Models.Barbers.Barber>(null);
                });
        }

        [Test]
        public async Task Handle_ValidId_DeleteBarber()
        {
            // Arrange
            var barberId = new Guid("12345678-1234-5678-1234-567812345678");
            var barber = new List<Domain.Models.Barbers.Barber>
            {
                new Domain.Models.Barbers.Barber
                {
                    Id = barberId
                }
            };
            SetupMockDbContext(barber);

            var command = new DeleteBarberCommand(barberId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(barberId));
        }


        [Test]
        public async Task Handle_inValidId_DeleteBarber()
        {
            // Arrange
            var barberId = Guid.NewGuid();

            var command = new DeleteBarberCommand(barberId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
