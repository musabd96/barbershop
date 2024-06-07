

using Domain.Models.Customers;

namespace Infrastructure.Repositories.Customers
{
    public interface ICustomerRepositories
    {
        Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken);
        Task<Customer> GetCustomerById(Guid id, CancellationToken cancellationToken);
        Task<Customer> AddNewCustomer(Customer newACustomer, CancellationToken cancellationToken);
        Task<Customer> UpdateCustomer(Guid customerId, string FirstName, string LastName, string Email, string Phone, CancellationToken cancellationToken);
        Task<Customer> DeleteCustomer(Guid customerId, CancellationToken cancellationToken);
    }
}
