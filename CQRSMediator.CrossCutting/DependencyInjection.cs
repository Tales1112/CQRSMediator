using CQRSMediator.Domain.Interfaces;
using CQRSMediator.Infrastructure.Context;
using CQRSMediator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSMediator.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                              b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
