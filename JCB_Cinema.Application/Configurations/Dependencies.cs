using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Mappers;
using JCB_Cinema.Application.Services;
using JCB_Cinema.Application.Servicies;
using JCB_Cinema.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Application.Configurations
{
    /// <summary>
    /// Class responsible for registering application dependencies.
    /// </summary>
    public class Dependencies
    {
        /// <summary>
        /// Registers services in the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> used to register dependencies.</param>
        public static void Register(IServiceCollection services)
        {
            // Register application services
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieProjectionService, MovieProjectionService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ICinemaHallService, CinemaHallService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBookingTicketService, BookingTicketService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IScreenTypeService, ScreenTypeService>();

            // Register AutoMapper profiles
            services.AddAutoMapper(
                typeof(GenreServiceProfile),
                typeof(MovieServiceProfile),
                typeof(MovieProjectionServiceProfile),
                typeof(ScheduleServiceProfile),
                typeof(CinemaHallServiceProfile),
                typeof(AppUserServiceProfile),
                typeof(BookingTicketServiceProfile),
                typeof(PhotoServiceProfile),
                typeof(ScreenTypeService)
            );

            // Register dependencies from the infrastructure layer
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
