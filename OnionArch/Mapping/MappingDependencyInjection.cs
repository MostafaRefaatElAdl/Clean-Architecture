using Mapster;
using MapsterMapper;
using System.Reflection;

namespace OnionArch.Mapping;
    
    public static class MappingDependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        

        return services;
        }
    }

