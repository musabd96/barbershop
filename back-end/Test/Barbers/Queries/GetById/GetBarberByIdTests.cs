

using Application.Queries.Barbers.GetBarberById;
using Infrastructure.Repositories.Barbers;
using Moq;
using NUnit.Framework;

namespace Test.Barbers.Queries.GetById
{
    [TestFixture]
    public class GetBarberByIdTests
    {
        private GetBarberByIdQueryHandler _handler;
        private Mock<IBarberRepositories> _barberRepositories;

        [SetUp]
        public void Setup()
        {
            _barberRepositories = new Mock<IBarberRepositories>();
            _handler = new GetBarberByIdQueryHandler(_barberRepositories.Object);
        }

        protected void SetupMockDbContext(Domain.Models.Barbers.Barber barber)
        {
            _barberRepositories.Setup(repo => repo.GetBarberById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(barber);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectBarber()
        {
            // Arrange
            var barberId = Guid.NewGuid();
            var expectedBarber = new Domain.Models.Barbers.Barber
            {
                Id = barberId,
                FirstName = "test"
            };

            SetupMockDbContext(expectedBarber);

            var query = new GetBarberByIdQuery(barberId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(expectedBarber.Id));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var barberId = Guid.NewGuid();

            var query = new GetBarberByIdQuery(barberId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
