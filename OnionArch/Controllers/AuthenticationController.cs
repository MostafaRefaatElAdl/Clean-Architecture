using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionArch.Application.Authentication.Commands.Register;
using OnionArch.Application.Authentication.Queries.Login;
using OnionArch.Application.Services.Authentication.Common;
using OnionArch.Contracts.Authentication;
using OnionArch.Filters;
using System.Xml.Linq;

namespace OnionArch.Controllers
{
    [Route("auth")]
    [ApiController]
    //[ErrorHandlingFilter]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

            //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var command = _mapper.Map<RegisterCommand>(request);
            AuthenticationResult authResult = await _mediator.Send(command);
            
            return Ok(_mapper.Map<AuthenticationResponse>(authResult));

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //var query = new LoginQuery(request.Email, request.Password);
            var query = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(query);
            return Ok(_mapper.Map<AuthenticationResponse>(authResult));
        }
    }
}
