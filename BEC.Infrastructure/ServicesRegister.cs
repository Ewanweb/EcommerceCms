using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BEC.Infrastructure
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {

            //Register DbContext
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            //Register Repositories

            //Register Services


            return services;
        }
    }
}
