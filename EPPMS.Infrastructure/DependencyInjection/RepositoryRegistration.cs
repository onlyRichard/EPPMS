using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Infrastructure.Data;
using EPPMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Infrastructure.DependencyInjection
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositories( this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            /*services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<ITechnicalModuleRepository, TechnicalModuleRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IOngoingTaskRepository, OngoingTaskRepository>();*/

            return services;
        }
    }
}
