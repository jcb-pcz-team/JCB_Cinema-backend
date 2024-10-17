using JCB_Cinema.Infrastructure.Data;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Infrastructure.Configurations
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
