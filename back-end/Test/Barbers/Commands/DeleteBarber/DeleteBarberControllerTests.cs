using API.Controllers.BarberController;
using Application.Commands.Barbers.DeleteBarber;
using Application.Dtos;
using Application.Validators.Barber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Commands.DeleteBarber
{
    [TestFixture]
    public class DeleteBarberControllerTests
    {
        private IMediator _mediator;
        private BarberValidator _validator;
        private BarberController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _validator = Mock.Of<BarberValidator>(); // Mock the validator
            _controller = new BarberController(_mediator, _validator);
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
        public async Task DeleteBarber_ValidId_ReturnsOkResult()
        {
            // Arrange
            var barberId = Guid.NewGuid();
            var barberDto = new BarberDto
            {
                Id = barberId,
                Name = "Test",
            };


            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteBarberCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Barbers.Barber());

            // Act
            var result = await _controller.DeleteBarber(barberId);


            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteBarber_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var barberId = Guid.NewGuid();
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteBarberCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Models.Barbers.Barber)null!);

            // Act
            var result = await _controller.DeleteBarber(barberId);

            // Assert
            var errors = (result as BadRequestObjectResult)?.Value as SerializableError;
            NUnit.Framework.Assert.That(errors!.ContainsKey("BarberNotFound"));
        }
    }
}
