using Ledbim.ApiExample.Domain.Base;
using Ledbim.ApiExample.Infrastructure.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ledbim.ApiExample.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                var sqlConStr = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(sqlConStr);
            });

            //Add Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
