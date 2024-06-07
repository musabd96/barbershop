using Domain.Models.Customers;
using Infrastructure.Repositories.Customers;
using MediatR;

namespace Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepositories _customerRepositories;

        public UpdateCustomerCommandHandler(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToUpdate = await _customerRepositories.UpdateCustomer(request.CustomerId,
                                                                              request.CustomerDto.FirstName,
                                                                              request.CustomerDto.LastName,
                                                                              request.CustomerDto.Email,
                                                                              request.CustomerDto.Phone,
                                                                              cancellationToken);

            return customerToUpdate;
        }
    }
}
