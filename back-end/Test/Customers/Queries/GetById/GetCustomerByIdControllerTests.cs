
using API.Controllers.CustomerController;
using Application.Queries.Customers.GetCustomerById;
using Application.Validators.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Queries.GetById
{
    [TestFixture]
    public class GetCustomerByIdControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<CustomerValidator> _validatorMock;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<CustomerValidator>(); // Mock the validator
            _controller = new CustomerController(_mediatorMock.Object, _validatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task GetCustomerById_ShouldReturnOk_WithValidId()
        {
            // Arrange
            var expectedCustomer = new Domain.Models.Customers.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.Bar@email.com",
                Phone = "0712345678"
            };

            var query = new GetCustomerByIdQuery(expectedCustomer.Id);

            _mediatorMock.Setup(x => x.Send(It.Is<GetCustomerByIdQuery>(q => q.Id == expectedCustomer.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _controller.GetCustomerById(expectedCustomer.Id);

            // Assert
            NUnit.Framework.Assert.Multiple(() =>
            {
                NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
                NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public async Task GetCustomerById_ShouldReturnInternalServerErrorOnException_WithInvalidId()
        {
            // Arrange
            var invalidCustomerId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new Exception("Simulated database error"));

            // Act
            var result = await _controller.GetCustomerById(invalidCustomerId);

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
