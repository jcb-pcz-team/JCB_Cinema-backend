using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Infrastructure.Data
{
    public class CinemaDbContext : IdentityDbContext
    {
        public CinemaDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Seat> Seats { get; set; }
        public DbSet<BookingTicket> BookingTickets { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieProjection> MoviesProjection { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<AppUser>()
                .HasMany(a => a.BookingTickets)
                .WithOne(b => b.AppUser)
                .HasForeignKey(b => b.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Seat>()
                .HasMany(a => a.BookingTickets)
                .WithOne(a => a.Seat)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MovieProjection>()
                .OwnsOne(a => a.Price);                
        }
    }
}
