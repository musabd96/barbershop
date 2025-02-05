﻿using API.Controllers.BarberController;
using Application.Dtos;
using Application.Validators.Barber;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test.Barber.Commands.UpdateBarber
{
    [TestFixture]
    public class UpdateBarberControllerTests
    {
        private BarberController _controller;
        private BarberValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BarberValidator();
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
        public async Task UpdateBarber_ValidId_ReturnsOkResult()
        {
            // Arrange
            var barberId = Guid.NewGuid();
            var barberDto = new BarberDto
            {
                Id = barberId,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };


            // Act
            var result = await _controller.UpdateBarber(barberDto, barberId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<OkObjectResult>());
            NUnit.Framework.Assert.That((result as OkObjectResult)?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateBarber_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var barberId = Guid.Empty;
            var barberDto = new BarberDto
            {
                Id = barberId,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@email.com",
                Phone = "0712345678"
            };

            // Act
            var result = await _controller.UpdateBarber(barberDto, barberId);

            // Assert
            NUnit.Framework.Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            NUnit.Framework.Assert.That((result as BadRequestObjectResult)?.StatusCode, Is.EqualTo(400));
        }
    }
}
