using MediatR;
using OnionArch.Application.Authentication.Commands.Register;
using OnionArch.Application.Common.Interfaces.Authentication;
using OnionArch.Application.Common.Interfaces.Presistence;
using OnionArch.Application.Services.Authentication.Common;
using OnionArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //Check if user exsits
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                throw new Exception("User with given email does not exsits.");
            }
            //validate the password is correct
            if (user.Password != query.Password)
            {
                throw new Exception("Invalid password");
            }
            //create the JWT token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
