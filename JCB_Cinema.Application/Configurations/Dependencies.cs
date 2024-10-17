using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Application.Configurations
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddScoped<ITServicies<Person>, PersonServicies>();

            //DI for Infrastructure
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
