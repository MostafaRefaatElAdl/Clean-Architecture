using ErrorOr;
using MediatR;
using OnionArch.Application.Authentication.Commands.Register;
using OnionArch.Application.Common.Interfaces.Authentication;
using OnionArch.Application.Common.Interfaces.Presistence;
using OnionArch.Domain.Common.Errors;
using OnionArch.Application.Services.Authentication.Common;
using OnionArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            //Check if user exsits
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //validate the password is correct
            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //create the JWT token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
