using Domain.Models.Barbers;
using Domain.Models.Customers;

namespace Domain.Models.Users
{
    public class UserRelationships
    {
        public class UserCustomer
        {
            public Guid UserId { get; set; }
            public User User { get; set; }

            public Guid CustomerId { get; set; }
            public Customer Customer { get; set; }
        }

        public class UserBarber
        {
            public Guid UserId { get; set; }
            public User User { get; set; }

            public Guid BarberId { get; set; }
            public Barber Barber { get; set; }
        }
    }
}
