using Domain.Models.Customers;
using Domain.Models.Users;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> allUsersFromDatabase = _dbContext.User.ToList();
                return await Task.FromResult(allUsersFromDatabase);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task<User> RegisterUser(User userToRegister, Customer customerToRegister, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Customer.Add(customerToRegister);
                _dbContext.User.Add(userToRegister);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _dbContext.UserCustomers.Add(
                    new UserRelationships.UserCustomer
                    {
                        UserId = userToRegister.Id,
                        CustomerId = customerToRegister.Id,
                    });
                await _dbContext.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(userToRegister);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
