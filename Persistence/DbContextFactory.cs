using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;


namespace Persistence
{
    public static partial class PersistenceServiceRegistrtion
    {
        public class ECXHRDbContextFactory
            : IDesignTimeDbContextFactory<ARMSDbContext>
        {
            public ARMSDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<ARMSDbContext>();
                var connectionString = configuration.GetConnectionString("staggingConnectionString");
                builder.UseSqlServer(connectionString);
                return new ARMSDbContext(builder.Options);
            }

        }

    }

}