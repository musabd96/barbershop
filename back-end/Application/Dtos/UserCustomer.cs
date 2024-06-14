
using Domain.Models.Customers;
using Domain.Models.Users;

namespace Application.Dtos
{
    public class UserCustomer
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
