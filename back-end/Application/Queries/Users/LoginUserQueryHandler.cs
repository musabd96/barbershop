﻿using Infrastructure.Repositories.Authorization;
using MediatR;

namespace Application.Queries.Users.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IAuthRepository _authRepository;

        public LoginUserQueryHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _authRepository.AuthenticateUser(request.LoginUser.Username.ToLowerInvariant(), request.LoginUser.Password, cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            string token = _authRepository.GenerateJwtToken(user, cancellationToken);

            return Task.FromResult(token);
        }
    }
}
