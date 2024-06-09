using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> RegisterUser(User userToRegister);
    }
}
