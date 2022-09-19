using MediatR;
using OnionArch.Application.Services.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;
}
