
using ApiPersistenciaDotnet.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiPersistenciaDotnet.Data
{
    public static class DataSourceConfiguration
    {
        private static IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        private static string connectionString = configurationRoot.GetConnectionString("OrdenesDotnetBD") ?? string.Empty;

        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ApplicationDbContext>();

            return services;
        }
    }
}
