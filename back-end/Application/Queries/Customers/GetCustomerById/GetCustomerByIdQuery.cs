using Domain.Models.Customers;
using MediatR;

namespace Application.Queries.Customers.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public GetCustomerByIdQuery(Guid customertId)
        {
            Id = customertId;
        }

        public Guid Id { get; set; }
    }
}
