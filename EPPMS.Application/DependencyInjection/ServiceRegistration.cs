using EPPMS.Application.Interfaces.Repositories;
using EPPMS.Application.Interfaces.Services;

using EPPMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<ITechnicalModuleService, TechnicalModuleService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IBugService, BugService>();
            services.AddScoped<IOngoingTaskService, OngoingTaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IErrorPageService, ErrorPageService>();

            return services;
        }
    }
}
