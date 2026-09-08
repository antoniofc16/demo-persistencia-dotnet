using ApiPersistenciaDotnet.Data.Services;
using System.Reflection;

namespace ApiPersistenciaDotnet.Data
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServicesApplication(this IServiceCollection services)
        {
            services.AddAutoMapper((s) => s.AddMaps(Assembly.GetExecutingAssembly()));

            services.AddScoped<IAdoNetService, AdoNetService>();
            services.AddScoped<IEFCoreService, EFCoreService>();
            services.AddScoped<IDapperService, DapperService>();

            return services;
        }
    }
}
