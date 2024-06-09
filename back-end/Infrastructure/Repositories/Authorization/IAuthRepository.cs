using Domain.Models.Users;

namespace Infrastructure.Repositories.Authorization
{
    public interface IAuthRepository
    {
        User AuthenticateUser(string username, string password, CancellationToken cancellationToken);
        string GenerateJwtToken(User user, CancellationToken cancellationToken);
    }
}
