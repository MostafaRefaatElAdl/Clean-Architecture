using Mapster;
using OnionArch.Application.Authentication.Commands.Register;
using OnionArch.Application.Authentication.Queries.Login;
using OnionArch.Application.Services.Authentication.Common;
using OnionArch.Contracts.Authentication;

namespace OnionArch.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>().Map(dest => dest, src => src.User);
        }
    }
}
