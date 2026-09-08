using Microsoft.Data.SqlClient;

namespace ApiPersistenciaDotnet.Data.Persistence
{
    public class AdoNetConnectionFactory
    {
        private static IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        private static string connectionString = configurationRoot.GetConnectionString("OrdenesDotnetBD") ?? string.Empty;
        
        public static SqlConnection Create()
        {
            return new SqlConnection(connectionString);
        }
    }
}
