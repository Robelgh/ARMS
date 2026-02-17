using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;


namespace Persistence
{
    public static partial class PersistenceServiceRegistrtion
    {
        public class ECXHRDbContextFactory
            : IDesignTimeDbContextFactory<PASDbContext>
        {
            public PASDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<PASDbContext>();
                var connectionString = configuration.GetConnectionString("staggingConnectionString");
                builder.UseSqlServer(connectionString);
                return new PASDbContext(builder.Options);
            }

        }

    }

}