using Application.Commands.Barbers.DeleteBarber;
using Application.Commands.BarberShops.DeleteBarberShop;
using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.DeleteBarberShop
{
    [TestFixture]
    public class DeleteBarberShopTests
    {
        private DeleteBarberShopCommandHandler _handler;
        private Mock<IBarberShopRepositories> _barberShopRepositories;

        [SetUp]
        public void Setup()
        {
            _barberShopRepositories = new Mock<IBarberShopRepositories>();
            _handler = new DeleteBarberShopCommandHandler(_barberShopRepositories.Object);
        }

        protected void SetupMockDbContext(List<BarberShop> barberShops)
        {
            _barberShopRepositories.Setup(repo => repo.DeleteBarberShop(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid barberId, CancellationToken cancellationToken) =>
                {
                    var barberToDelete = barberShops.FirstOrDefault(barber => barber.Id == barberId);
                    if (barberToDelete != null)
                    {
                        barberShops.Remove(barberToDelete);
                        return Task.FromResult(barberToDelete);
                    }
                    return Task.FromResult<BarberShop>(null);
                });
        }

        [Test]
        public async Task Handle_ValidId_DeleteBarberShop()
        {
            // Arrange
            var barberShopId = new Guid("12345678-1234-5678-1234-567812345678");
            var barberShop = new List<BarberShop>
            {
                new BarberShop
                {
                    Id = barberShopId
                }
            };
            SetupMockDbContext(barberShop);

            var command = new DeleteBarberShopCommand(barberShopId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(barberShopId));
        }


        [Test]
        public async Task Handle_inValidId_DeleteBarberShop()
        {
            // Arrange
            var barberShopId = Guid.NewGuid();

            var command = new DeleteBarberShopCommand(barberShopId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);
        }
    }
}
