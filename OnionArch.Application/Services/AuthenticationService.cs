using OnionArch.Application.Common.Interfaces.Authentication;
using OnionArch.Application.Common.Interfaces.Presistence;
using OnionArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnionArch.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //Check if user exsits
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exsits.");
            }
            //Create User(with unique GUID)
            var user = new User { Id = Guid.NewGuid(), FirstName = firstName, LastName = lastName, Email = email, Password = password };

            _userRepository.AddUser(user);
            //Generate Token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Login(string email, string password)
        {
            //Check if user exsits
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exsits.");
            }
            //validate the password is correct
            if (user.Password != password)
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
