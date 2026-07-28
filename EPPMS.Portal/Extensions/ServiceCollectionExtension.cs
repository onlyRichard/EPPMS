using EPPMS.Application.DependencyInjection;
using EPPMS.Infrastructure.DependencyInjection;

namespace EPPMS.Portal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPortalServices(this IServiceCollection services,  IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddApplication()
            .AddInfrastructure(configuration);

        return services;
    }
}