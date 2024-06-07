﻿

using API.Controllers.BarberController;
using Application.Dtos;
using Application.Validators.Barber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Commands.AddNewBarber
{
    [TestFixture]
    public class AddNewBarberControllerTests
    {
        private BarberController _controller;
        private BarberValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BarberValidator(); // Use real validator instance
            _controller = new BarberController(Mock.Of<IMediator>(), _validator);
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
        public async Task AddNewBarber_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var barberDto = new BarberDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",

            };

            // Act
            var result = await _controller.AddNewBarber(barberDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AddNewBarber_ValidInput_ReturnsBadRequest()
        {
            // Arrange
            var barberDto = new BarberDto
            {

            };

            // Act
            var result = await _controller.AddNewBarber(barberDto);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}