using Application.Commands.BarberShops.AddNewBarberShop;
using Application.Dtos;
using Domain.Models.BarberShops;
using Infrastructure.Repositories.BarberShops;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.AddNewBarberShop
{
    [TestFixture]
    public class AddNewBarberShopTests
    {
        private AddNewBarberShopCommandHandler _handler;
        private Mock<IBarberShopRepositories> _barberShopRepositories;

        [SetUp]
        public void Setup()
        {
            _barberShopRepositories = new Mock<IBarberShopRepositories>();
            _handler = new AddNewBarberShopCommandHandler(_barberShopRepositories.Object);
        }

        protected void SetupMockDbContext(List<BarberShop> barberShops)
        {
            _barberShopRepositories.Setup(repo => repo.GetAllBarberShops(It.IsAny<CancellationToken>()))
                .ReturnsAsync(barberShops);

            _barberShopRepositories.Setup(repo => repo.AddNewBarberShop(It.IsAny<BarberShop>(), It.IsAny<CancellationToken>()))
                .Callback((BarberShop barberShop, CancellationToken cancellationToken) => barberShops.Add(barberShop))
                .Returns((BarberShop barberShop, CancellationToken cancellationToken) => Task.FromResult(barberShop));
        }

        [Test]
        public async Task Handle_Validbarber_ReturnsNewbarberShop()
        {
            // Arrange
            var barberShops = new List<BarberShop>
            {
                new BarberShop
                {
                    Id = Guid.NewGuid(),
                    Name = "test1",
                    Email = "test1@test.com",
                    Phone = "0712345678",
                    Address = "street 1",
                    ZipCode = "12345",
                    City = "city1"
                },
                new BarberShop
                {
                    Id = Guid.NewGuid(),
                    Name = "test2",
                    Email = "test2@test.com",
                    Phone = "0712345678",
                    Address = "street 2",
                    ZipCode = "12345",
                    City = "city2"
                }
            };

            SetupMockDbContext(barberShops);

            var barberShop = new BarberShopDto
            {
                Id = Guid.NewGuid(),
                Name = "test3",
                Email = "test3@test.com",
                Phone = "0712345678",
                Address = "street 3",
                ZipCode = "12345",
                City = "city3"
            };


            var addbarberCommand = new AddNewBarberShopCommand(barberShop);

            // Act
            var result = await _handler!.Handle(addbarberCommand, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result.Name, Is.EqualTo(barberShop.Name));
        }
    }
}
