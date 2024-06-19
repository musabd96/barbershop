using Domain.Models.Customers;
using Domain.Models.Users;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> allUsersFromDatabase = _appDbContext.User.ToList();
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
                _appDbContext.Customers.Add(customerToRegister);
                _appDbContext.User.Add(userToRegister);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                _appDbContext.UserCustomers.Add(
                    new UserRelationships.UserCustomer
                    {
                        UserId = userToRegister.Id,
                        CustomerId = customerToRegister.Id,
                    });
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return await Task.FromResult(userToRegister);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
