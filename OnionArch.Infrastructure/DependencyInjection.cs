using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArch.Application.Common.Interfaces.Authentication;
using OnionArch.Application.Common.Interfaces.Presistence;
using OnionArch.Application.Common.Interfaces.Serivces;
using OnionArch.Infrastructure.Authentication;
using OnionArch.Infrastructure.Persistence;
using OnionArch.Infrastructure.Services;

namespace OnionArch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
             
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
