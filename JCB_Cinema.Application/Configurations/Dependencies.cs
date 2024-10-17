using JCB_Cinema.Application.Interfaces.Servicies;
using JCB_Cinema.Application.Mappers;
using JCB_Cinema.Application.Servicies;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Application.Configurations
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();

            services.AddAutoMapper(typeof(GenreServiceProfile), typeof(MovieServiceProfile));

            //DI for Infrastructure
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
