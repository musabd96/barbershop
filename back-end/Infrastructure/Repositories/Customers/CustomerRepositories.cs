using Domain.Models.Customers;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Customers
{
    public class CustomerRepositories : ICustomerRepositories
    {
        private readonly AppDbContext _appDbContext;
        public CustomerRepositories(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken)
        {
            try
            {
                List<Customer> allCustomers = await _appDbContext.Customers.ToListAsync(cancellationToken);

                return allCustomers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all customers  from the database", ex);
            }
        }
        public async Task<Customer> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                Customer? wantedCustomer = await _appDbContext.Customers
                    .FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);

                return wantedCustomer!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the customer from the database", ex);
            }
        }
        public async Task<Customer> AddNewCustomer(Customer newCustomer, CancellationToken cancellationToken)
        {
            try
            {
                _appDbContext.Customers.Add(newCustomer);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return newCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the customer to the database", ex);

            }
        }

        public async Task<Customer> DeleteCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            try
            {
                Customer customerToUpdate = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);

                if (customerToUpdate != null)
                {


                    _appDbContext.Customers.Remove(customerToUpdate);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return customerToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the customer to the database", ex);
            }
        }

        public async Task<Customer> UpdateCustomer(Guid customerId, string FirstName, string LastName, string Email, string Phone, CancellationToken cancellationToken)
        {
            try
            {
                Customer customerToUpdate = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);

                if (customerToUpdate != null)
                {
                    customerToUpdate.FirstName = FirstName;
                    customerToUpdate.LastName = LastName;
                    customerToUpdate.Email = Email;
                    customerToUpdate.Phone = Phone;

                    _appDbContext.Customers.Update(customerToUpdate);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    return customerToUpdate;
                }

                return null!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the customer to the database", ex);
            }
        }

    }
}
