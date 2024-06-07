using API.Controllers.CustomerController;
using Application.Commands.Customers.DeleteCustomer;
using Application.Dtos;
using Application.Validators.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.DeleteCustomer
{
    [TestFixture]
    public class DeleteCustomerControllerTests
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
        public async Task DeleteCustomer_ValidId_ReturnsOkResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };


            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Customers.Customer());

            // Act
            var result = await _controller.DeleteCustomer(customerId);


            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteCustomer_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            Mock.Get(_mediator)
                .Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Models.Customers.Customer)null!);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            var errors = (result as BadRequestObjectResult)?.Value as SerializableError;
            NUnit.Framework.Assert.That(errors!.ContainsKey("CustomerNotFound"));
        }
    }
}
