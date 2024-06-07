using Domain.Models.Customers;
using Infrastructure.Repositories.Customers;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepositories _customerRepositories;

        public GetCustomerByIdQueryHandler(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            Customer customer = await _customerRepositories.GetCustomerById(request.Id, cancellationToken);

            return customer;
        }
    }
}
