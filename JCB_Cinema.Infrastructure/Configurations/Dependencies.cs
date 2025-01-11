using JCB_Cinema.Infrastructure.Data;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Infrastructure.Configurations
{
    /// <summary>
    /// Provides methods for registering dependencies with the dependency injection container.
    /// </summary>
    public class Dependencies
    {
        /// <summary>
        /// Registers the necessary services and interfaces with the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the services with.</param>
        public static void Register(IServiceCollection services)
        {
            // Register the Unit of Work and its implementation
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
