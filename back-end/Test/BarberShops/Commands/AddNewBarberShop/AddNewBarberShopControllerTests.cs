

using API.Controllers.BarberShopController;
using Application.Commands.Barbers.AddNewBarber;
using Application.Commands.BarberShops.AddNewBarberShop;
using Application.Dtos;
using Application.Validators.BarberShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.AddNewBarberShop
{
    [TestFixture]
    public class AddNewBarberShopShopControllerTests
    {
        private BarberShopController _controller;
        private BarberShopValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BarberShopValidator(); // Use real validator instance
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
        public async Task AddNewBarberShop_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var barberShopDto = new BarberShopDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };


            // Act
            var result = await _controller.AddNewBarberShop(barberShopDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AddNewBarberShop_ValidInput_ReturnsBadRequest()
        {
            // Arrange
            var barberShopDto = new BarberShopDto
            {

            };

            // Act
            var result = await _controller.AddNewBarberShop(barberShopDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
