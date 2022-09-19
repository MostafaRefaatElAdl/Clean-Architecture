using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnionArch.Errors;
using OnionArch.Mapping;

namespace OnionArch
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            //builder.Services.AddControllers(Options => Options.Filters.Add<ErrorHandlingFilter>());
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, OnionArchProblemDetailsFactory>();
            
            services.AddMapping();
            return services;
        }
    }
}
