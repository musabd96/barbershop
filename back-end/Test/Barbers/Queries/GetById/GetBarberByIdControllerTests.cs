using API.Controllers.BarberController;
using Application.Queries.Barbers.GetBarberById;
using Application.Validators.Barber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Barbers.Queries.GetById
{
    [TestFixture]
    public class GetBarberByIdControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<BarberValidator> _validatorMock;
        private BarberController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<BarberValidator>(); // Mock the validator
            _controller = new BarberController(_mediatorMock.Object, _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task GetBarberById_ShouldReturnOk_WithValidId()
        {
            // Arrange
            var expectedBarber = new Domain.Models.Barbers.Barber
            {
                Id = Guid.NewGuid(),
                FirstName = "Foo",
            };

            var query = new GetBarberByIdQuery(expectedBarber.Id);

            _mediatorMock.Setup(x => x.Send(It.Is<GetBarberByIdQuery>(q => q.Id == expectedBarber.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedBarber);

            // Act
            var result = await _controller.GetBarberById(expectedBarber.Id);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetBarberById_ShouldReturnInternalServerErrorOnException_WithInvalidId()
        {
            // Arrange
            var invalidBarberId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetBarberByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new Exception("Simulated database error"));

            // Act
            var result = await _controller.GetBarberById(invalidBarberId);

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
