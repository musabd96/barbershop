using API.Controllers.BarberShopController;
using Application.Commands.BarberShops.DeleteBarberShop;
using Application.Dtos;
using Application.Validators.BarberShop;
using Domain.Models.BarberShops;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Commands.DeleteBarberShop
{
    [TestFixture]
    public class DeleteBarberShopControllerTests
    {
        private IMediator _mediator;
        private BarberShopValidator _validator;
        private BarberShopController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _validator = Mock.Of<BarberShopValidator>();
            _controller = new BarberShopController(_mediator, _validator);
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
        public async Task DeleteBarberShop_ValidId_ReturnsOkResult()
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


            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteBarberShopCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BarberShop());

            // Act
            var result = await _controller.DeleteBarberShop(barberShopId);


            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteBarber_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var barberShopId = Guid.NewGuid();
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteBarberShopCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((BarberShop)null!);

            // Act
            var result = await _controller.DeleteBarberShop(barberShopId);

            // Assert
            var errors = (result as BadRequestObjectResult)?.Value as SerializableError;
            NUnit.Framework.Assert.That(errors!.ContainsKey("BarberShopNotFound"));
        }
    }
}
