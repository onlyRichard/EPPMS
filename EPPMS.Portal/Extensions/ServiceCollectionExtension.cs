using EPPMS.Portal.Helpers;
using EPPMS.Portal.Services.ApiClients;
using EPPMS.Portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EPPMS.Portal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<ApiSettings>(
            configuration.GetSection(ApiSettings.SectionName));

        services.AddHttpClient<IApplicationApiClient, ApplicationApiClient>(
            (serviceProvider, client) =>
            {
                var apiSettings = serviceProvider
                    .GetRequiredService<IOptions<ApiSettings>>()
                    .Value;

                client.BaseAddress = new Uri(apiSettings.AdminBaseUrl);
            });

        return services;
    }
}