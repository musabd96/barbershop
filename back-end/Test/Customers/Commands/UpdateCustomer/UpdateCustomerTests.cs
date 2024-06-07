

using Application.Commands.Customers.UpdateCustomer;
using Application.Dtos;
using Infrastructure.Repositories.Customers;
using Moq;
using NUnit.Framework;

namespace Test.Customers.Commands.UpdateCustomer
{
    [TestFixture]
    public class UpdateCustomerTests
    {
        private Mock<ICustomerRepositories> _customerRepository;
        private UpdateCustomerCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepositories>();
            _handler = new UpdateCustomerCommandHandler(_customerRepository.Object);
        }

        private void SetupMockDbContext(List<Domain.Models.Customers.Customer> customers)
        {
            _customerRepository.Setup(repo => repo.UpdateCustomer(It.IsAny<Guid>(), It.IsAny<string>(),
                                                                  It.IsAny<string>(), It.IsAny<string>(),
                                                                  It.IsAny<string>(), It.IsAny<CancellationToken>()))!
                                  .ReturnsAsync((Guid customerId, string firstName,
                                                 string lastName, string email,
                                                 string phone, CancellationToken ct) =>
                                  {
                                      var customer = customers.FirstOrDefault(a => a.Id == customerId);
                                      if (customer != null)
                                      {
                                          customer.Id = customerId;
                                          customer.FirstName = firstName;
                                          customer.LastName = lastName;
                                          customer.Email = email;
                                          customer.Phone = phone;
                                          return customer;
                                      }
                                      return null;
                                  });
        }

        [Test]
        public async Task Handle_UpdateValidCustomer_ReturnsUpdatedCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customers = new List<Domain.Models.Customers.Customer>
            {
                new Domain.Models.Customers.Customer
                {
                    Id = customerId,
                    FirstName = "Foo",
                    LastName = "Bar",
                    Email = "foo.Bar@email.com",
                    Phone = "0712345678"

                }
            };
            SetupMockDbContext(customers);

            var updateDto = new CustomerDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.Bar@email.com",
                Phone = "0712345678"
            };

            var command = new UpdateCustomerCommand(updateDto, customerId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.FirstName, Is.EqualTo(updateDto.FirstName));

        }

        [Test]
        public async Task Handle_UpdateInValidCustomer_ReturnsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var updateDto = new CustomerDto
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.Bar@email.com",
                Phone = "0712345678"
            };

            var command = new UpdateCustomerCommand(updateDto, customerId);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            NUnit.Framework.Assert.That(result, Is.Null);

        }
    }
}
