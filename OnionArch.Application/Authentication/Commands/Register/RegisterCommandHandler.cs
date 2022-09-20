using ErrorOr;
using MediatR;
using OnionArch.Application.Common.Interfaces.Authentication;
using OnionArch.Application.Common.Interfaces.Presistence;
using OnionArch.Application.Services.Authentication.Common;
using OnionArch.Domain.Common.Errors;
using OnionArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //Check if user exsits
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }
            //Create User(with unique GUID)
            var user = new User { Id = Guid.NewGuid(), FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

            _userRepository.AddUser(user);
            //Generate Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
