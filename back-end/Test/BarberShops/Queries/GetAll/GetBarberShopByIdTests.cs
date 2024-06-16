using API.Controllers.BarberShopController;
using Application.Queries.BarberShops.GetBarberShopById;
using Application.Validators.BarberShop;
using Domain.Models.BarberShops;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Queries.GetAll
{
    [TestFixture]
    public class GetBarberShopByIdTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<BarberShopValidator> _validatorMock;
        private BarberShopController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<BarberShopValidator>(); // Mock the validator
            _controller = new BarberShopController(_mediatorMock.Object, _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task GetBarberShopById_ShouldReturnOk_WithValidId()
        {
            // Arrange
            var expectedBarberShop = new BarberShop
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@email.com",
                Phone = "0712345678",
                Address = "Test",
                ZipCode = "12345",
                City = "City"
            };

            var query = new GetBarberShopByIdQuery(expectedBarberShop.Id);

            _mediatorMock.Setup(x => x.Send(It.Is<GetBarberShopByIdQuery>(q => q.Id == expectedBarberShop.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedBarberShop);

            // Act
            var result = await _controller.GetBarberShopById(expectedBarberShop.Id);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetBarberShopById_ShouldReturnInternalServerErrorOnException_WithInvalidId()
        {
            // Arrange
            var invalidBarberId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetBarberShopByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new Exception("Simulated database error"));

            // Act
            var result = await _controller.GetBarberShopById(invalidBarberId);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
                NUnit.Framework.Assert.That((result as ObjectResult)?.Value, Is.EqualTo("Simulated database error"));
            });
        }
    }
}
