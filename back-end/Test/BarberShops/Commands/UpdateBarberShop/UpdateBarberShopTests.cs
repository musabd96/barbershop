

using Application.Commands.Barbers.UpdateBarber;
using Application.Commands.BarberShops.UpdateBarberShop;
using Application.Dtos;
using Domain.Models.BarberShops;
using Infrastructure.Repositories.Barbers;
using Infrastructure.Repositories.BarberShops;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.UpdateBarberShop
{
    [TestFixture]
    public class UpdateBarberShopTests
    {
        private Mock<IBarberShopRepositories> _barberShopRepository;
        private UpdateBarberShopCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _barberShopRepository = new Mock<IBarberShopRepositories>();
            _handler = new UpdateBarberShopCommandHandler(_barberShopRepository.Object);
        }

        private void SetupMockDbContext(List<BarberShop> barberShops)
        {
            _barberShopRepository.Setup(repo => repo.UpdateBarberShop(It.IsAny<Guid>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<string>(),
                                                              It.IsAny<CancellationToken>()))!
                                  .ReturnsAsync((Guid barberShopId, string name,
                                                 string email, string phone,
                                                 string address, string zipCode,
                                                 string city, CancellationToken ct) =>
                                  {
                                      var barberShop = barberShops.FirstOrDefault(a => a.Id == barberShopId);
                                      if (barberShop != null)
                                      {
                                          barberShop.Id = barberShopId;
                                          barberShop.Name = name;
                                          barberShop.Email = email;
                                          barberShop.Phone = phone;
                                          barberShop.Address = address;
                                          barberShop.ZipCode = zipCode;
                                          barberShop.City = city;

                                          return barberShop;
                                      }
                                      return null;
                                  });
        }

        [Test]
        public async Task Handle_UpdateValidBarberShop_ReturnsUpdatedBarber()
        {
            // Arrange
            var barberShopId = Guid.NewGuid();
            var barberShops = new List<BarberShop>
            {
                new BarberShop
                {
                    Id = barberShopId,
                     Name = "Test",
                    Email = "Test@email.com",
                    Phone = "0712345678",
                    Address = "Test",
                    ZipCode = "12345",
                    City = "City"

                }
            };
            SetupMockDbContext(barberShops);

            var updateDto = new BarberShopDto
            {
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };

            var command = new UpdateBarberShopCommand(updateDto, barberShopId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Name, Is.EqualTo(updateDto.Name));

        }

        [Test]
        public async Task Handle_UpdateInValidBarberShop_ReturnsNull()
        {
            // Arrange
            var barberShopId = Guid.NewGuid();

            var updateDto = new BarberShopDto
            {
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };

            var command = new UpdateBarberShopCommand(updateDto, barberShopId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);

        }
    }
}
