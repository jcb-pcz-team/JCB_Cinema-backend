using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the cinema application.
    /// Inherits from <see cref="IdentityDbContext"/> to manage identity data.
    /// </summary>
    public class CinemaDbContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CinemaDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public CinemaDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="Seat"/> entities.
        /// </summary>
        public DbSet<Seat> Seats { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="BookingTicket"/> entities.
        /// </summary>
        public DbSet<BookingTicket> BookingTickets { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="CinemaHall"/> entities.
        /// </summary>
        public DbSet<CinemaHall> CinemaHalls { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="Movie"/> entities.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="MovieProjection"/> entities.
        /// </summary>
        public DbSet<MovieProjection> MoviesProjection { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="Photo"/> entities.
        /// </summary>
        public DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="Schedule"/> entities.
        /// </summary>
        public DbSet<Schedule> Schedules { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of <see cref="AppUser"/> entities.
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }

        /// <summary>
        /// Configures the model for the CinemaDbContext.
        /// </summary>
        /// <param name="builder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure AppUser entity
            builder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();  

            builder.Entity<AppUser>()
                .HasMany(a => a.BookingTickets)
                .WithOne(b => b.AppUser)
                .HasForeignKey(b => b.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Seat entity
            builder.Entity<Seat>()
                .HasMany(a => a.BookingTickets)
                .WithOne(a => a.Seat)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure MovieProjection entity
            builder.Entity<MovieProjection>()
                .OwnsOne(a => a.Price);
        }
    }
}
