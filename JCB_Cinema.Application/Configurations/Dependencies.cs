﻿using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Mappers;
using JCB_Cinema.Application.Servicies;
using JCB_Cinema.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Application.Configurations
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieProjectionService, MovieProjectionService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ICinemaHallService, CinemaHallService>();
            services.AddAutoMapper(typeof(GenreServiceProfile), typeof(MovieServiceProfile), typeof(MovieProjectionServiceProfile), typeof(ScheduleServiceProfile), typeof(CinemaHallServiceProfile));

            //DI for Infrastructure
            Infrastructure.Configurations.Dependencies.Register(services);
        }
    }
}
