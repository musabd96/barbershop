using Domain.Models.Customers;
using Infrastructure.Repositories.Customers;
using MediatR;

namespace Application.Queries.Customers.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepositories _customerRepositories;

        public GetAllCustomersQueryHandler(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }
        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            List<Customer> AllCustomers = await _customerRepositories.GetAllCustomers(cancellationToken);
            return AllCustomers ?? throw new InvalidOperationException("No appointments were found");
        }
    }
}
