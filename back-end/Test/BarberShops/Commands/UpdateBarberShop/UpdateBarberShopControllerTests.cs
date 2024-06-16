using API.Controllers.BarberShopController;
using Application.Dtos;
using Application.Validators.BarberShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.UpdateBarberShop
{
    [TestFixture]
    public class UpdateBarberShopControllerTests
    {
        private BarberShopController _controller;
        private BarberShopValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BarberShopValidator();
            _controller = new BarberShopController(Mock.Of<IMediator>(), _validator);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller != null)
            {
                _controller.Dispose();
            }
        }

        [Test]
        public async Task UpdateBarberShop_ValidId_ReturnsOkResult()
        {
            // Arrange
            var barberShopId = Guid.NewGuid();
            var barberShopDto = new BarberShopDto
            {
                Id = barberShopId,
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };


            // Act
            var result = await _controller.UpdateBarberShop(barberShopDto, barberShopId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateBarberShop_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var barberShopId = Guid.Empty;
            var barberShopDto = new BarberShopDto
            {
                Id = barberShopId,
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };

            // Act
            var result = await _controller.UpdateBarberShop(barberShopDto, barberShopId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
