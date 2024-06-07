using API.Controllers.CustomerController;
using Application.Dtos;
using Application.Validators.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.AddNewCustomer
{
    [TestFixture]
    public class AddNewCustomerControllerTests
    {
        private CustomerController _controller;
        private CustomerValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CustomerValidator(); // Use real validator instance
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
        public async Task AddNewCustomer_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"

            };

            // Act
            var result = await _controller.AddNewCustomer(customerDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AddNewCustomer_ValidInput_ReturnsBadRequest()
        {
            // Arrange
            var customerDto = new CustomerDto
            {

            };

            // Act
            var result = await _controller.AddNewCustomer(customerDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
