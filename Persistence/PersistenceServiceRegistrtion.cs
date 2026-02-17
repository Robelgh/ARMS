using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static partial class PersistenceServiceRegistrtion
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PASDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("staggingConnectionString")));


           // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
     
            return services;
        }

    }
}