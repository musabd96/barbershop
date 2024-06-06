using Application.Queries.Barbers.GetAllBarbers;
using Infrastructure.Repositories.Barbers;
using Moq;
using NUnit.Framework;

namespace Test.Barbers.Queries.GetAll
{
    [TestFixture]
    public class GetAllBarbersTests
    {
        private GetAllBarbersQueryHandler _handler;
        private GetAllBarbersQuery _request;
        private Mock<IBarberRepositories> _barberRepositories;

        [SetUp]
        public void Setup()
        {
            _barberRepositories = new Mock<IBarberRepositories>();
            _handler = new GetAllBarbersQueryHandler(_barberRepositories.Object);
            _request = new GetAllBarbersQuery();
        }

        protected void SetupMockDbContext(List<Domain.Models.Barbers.Barber> barbers)
        {
            _barberRepositories.Setup(repo => repo.GetAllBarbers(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(barbers);
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsListOfBarbers()
        {
            // Arrange
            var barbersList = new List<Domain.Models.Barbers.Barber>
            {
                new Domain.Models.Barbers.Barber
                {
                    Id = Guid.NewGuid(),
                    Name = "Foo",
                },
                new Domain.Models.Barbers.Barber
                {
                    Id = Guid.NewGuid(),
                    Name = "Bar",
                },
            };

            SetupMockDbContext(barbersList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(barbersList.Count));
        }

        [Test]
        public async Task Handle_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyBarbersList = new List<Domain.Models.Barbers.Barber>();
            SetupMockDbContext(emptyBarbersList);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Empty);
        }
    }
}
