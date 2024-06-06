using API.Controllers.BarberController;
using Application.Queries.Barbers.GetAllBarbers;
using Application.Validators.Barber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Queries.GetAll
{
    [TestFixture]
    public class GetAllBarbersControllerTests
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
        public async Task GetAllBarbers_ShouldReturnOk()
        {
            // Arrange
            var expectedBarbers = new List<Domain.Models.Barbers.Barber>
            {
                new() {
                    Id = new Guid(),
                    Name = "Foo",
                },
                new () {
                    Id = new Guid(),
                    Name = "Bar",
                }
            };

            var query = new GetAllBarbersQuery();

            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllBarbersQuery>(), default))
                .ReturnsAsync(expectedBarbers);

            // Act
            var result = await _controller.GetAllBarbers();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAllBarbers_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllBarbersQuery>(), default))
                .Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.GetAllBarbers();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
            });
        }
    }
}
