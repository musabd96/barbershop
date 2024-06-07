
using API.Controllers.CustomerController;
using Application.Queries.Customers.GetAllCustomers;
using Application.Validators.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Queries.GetAll
{
    [TestFixture]
    public class GetAllCustomersControllerTests
    {
        private IMediator _mediator;
        private CustomerValidator _validator;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = Mock.Of<IMediator>();
            _validator = Mock.Of<CustomerValidator>(); // Mock the validator
            _controller = new CustomerController(_mediator, _validator);
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
        public async Task GetAllCustomers_ShouldReturnOk()
        {
            // Arrange
            var expectedCustomers = new List<Domain.Models.Customers.Customer>
            {
                new() {
                    Id = new Guid(),
                    FirstName = "Foo",
                    LastName = "Bar",
                    Email = "foo.Bar@email.com",
                    Phone = "0712345678"
                },
                new () {
                    Id = new Guid(),
                    FirstName = "Raf",
                    LastName = "Far",
                    Email = "raf.far@email.com",
                    Phone = "0712345678"
                }
            };

            var query = new GetAllCustomersQuery();

            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllCustomersQuery>(), default))
                .ReturnsAsync(expectedCustomers);

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<GetAllCustomersQuery>(), default))
                .Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<ObjectResult>());
                NUnit.Framework.Assert.That((result as ObjectResult)?.StatusCode, Is.EqualTo(500));
            });
        }
    }
}
