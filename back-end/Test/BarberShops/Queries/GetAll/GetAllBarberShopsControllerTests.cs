using API.Controllers.BarberShopController;
using Application.Queries.Barbers.GetAllBarbers;
using Application.Queries.BarberShops.GetAllBarberShops;
using Application.Validators.BarberShop;
using Domain.Models.BarberShops;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.BarberShops.Queries.GetAll
{
    [TestFixture]
    public class GetAllBarberShopsControllerTests
    {
        private IMediator _mediator;
        private BarberShopValidator _validator;
        private BarberShopController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _validator = Mock.Of<BarberShopValidator>(); // Mock the validator
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
        public async Task GetAllBarberShops_ShouldReturnOk()
        {
            // Arrange
            var expectedBarberShops = new List<BarberShop>
            {
                new() {
                    Id = new Guid(),
                     Name = "Test",
                    Email = "Test@email.com",
                    Phone = "0712345678",
                    Address = "Test",
                    ZipCode = "12345",
                    City = "City"
                },
                new () {
                    Id = new Guid(),
                     Name = "Test",
                    Email = "Test@email.com",
                    Phone = "0712345678",
                    Address = "Test",
                    ZipCode = "12345",
                    City = "City"
                }
            };

            var query = new GetAllBarberShopsQuery();

            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllBarberShopsQuery>(), default))
                .ReturnsAsync(expectedBarberShops);

            // Act
            var result = await _controller.GetAllBarberShops();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAllBarberShops_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllBarberShopsQuery>(), default))
                .Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.GetAllBarberShops();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
            });
        }
    }
}
