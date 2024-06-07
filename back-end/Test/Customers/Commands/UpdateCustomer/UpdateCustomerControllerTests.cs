using API.Controllers.CustomerController;
using Application.Dtos;
using Application.Validators.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.UpdateCustomer
{
    [TestFixture]
    public class UpdateCustomerControllerTests
    {
        private CustomerController _controller;
        private CustomerValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CustomerValidator();
            _controller = new CustomerController(Mock.Of<IMediator>(), _validator);
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
        public async Task UpdateCustomer_ValidId_ReturnsOkResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto
            {
                Id = customerId,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@gmail.com",
                Phone = "0712345678"
            };


            // Act
            var result = await _controller.UpdateCustomer(customerDto, customerId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateCustomer_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var customerId = Guid.Empty;
            var customerDto = new CustomerDto
            {
                Id = customerId,
                FirstName = "Test123",
                LastName = "Test",
                Email = "Test@gmail.com",
                Phone = "0712345678"
            };

            // Act
            var result = await _controller.UpdateCustomer(customerDto, customerId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
