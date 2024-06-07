using Domain.Models.Customers;
using MediatR;

namespace Application.Queries.Customers.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<Customer>>
    {
    }
}
