using EPPMS.Application.Interfaces.Data;
using EPPMS.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Infrastructure.DependencyInjection
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            return services;
        }
    }
}
