using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Infrastructure.Data.Seed
{
    public static class Seed
    {
        public static async Task Init(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<CinemaDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await ClearDatabaseAsync(dbContext);

            var users = GetUsers();
            var photos = GetPhotos();
            var movies = GetMovies(photos);

            var cinemaHalls = GetCinemaHalls();
            // Dodać Seats

            var seats = GetSeats(cinemaHalls);
            // Dodać bookingTickets

            var movieProjections = GetMovieProjections(movies, cinemaHalls);

            var schedule = GetSchedules(movieProjections);
            // Dodać listę movie projections => obecne roziwązanie jest tymczasowe

            var bookingTickets = GetBookingTickets(users, movieProjections, seats);

            await EnsureRolesAsync(roleManager);
            await AddUsersWithRolesAsync(userManager, roleManager, users);
            await AddEntitiesAsync(dbContext, photos);
            await AddEntitiesAsync(dbContext, movies);
            await AddEntitiesAsync(dbContext, cinemaHalls);
            await AddEntitiesAsync(dbContext, seats);

            //Update cinemaHalls - dodać seats
            await UpdateCinemaHalls(await dbContext.Seats.ToListAsync(), await dbContext.CinemaHalls.ToListAsync(), dbContext);

            await AddEntitiesAsync(dbContext, movieProjections);
            await AddEntitiesAsync(dbContext, schedule);
            await AddEntitiesAsync(dbContext, bookingTickets);

            //Update Seats - dodać booking Tickets
            await UpdateSeats(await dbContext.BookingTickets.ToListAsync(), await dbContext.Seats.ToListAsync(), dbContext);
        }

        // Booking Ticket
        private static List<BookingTicket> GetBookingTickets(List<AppUser> users, List<MovieProjection> moviesProjection, List<Seat> seats)
        {
            return new List<BookingTicket>
            {
                new BookingTicket
                {
                    AppUserId = users[0].Id,
                    AppUser = users[0],
                    MovieProjectionId = moviesProjection[0].MovieProjectionId,
                    MovieProjection = moviesProjection[0],
                    SeatId = seats[0].SeatId,
                    Seat = seats[0],
                    ReservationTime = GetDate(false),
                    ExpiresAt = GetDate(true).AddHours(2),
                    IsConfirmed = true,
                    Price = 150,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new BookingTicket
                {
                    AppUserId = users[1].Id,
                    AppUser = users[1],
                    MovieProjectionId = moviesProjection[1].MovieProjectionId,
                    MovieProjection = moviesProjection[1],
                    SeatId = seats[1].SeatId,
                    Seat = seats[1],
                    ReservationTime = GetDate(false),
                    ExpiresAt = GetDate(true).AddHours(3),
                    IsConfirmed = false,
                    Price = 1800,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new BookingTicket
                {
                    AppUserId = users[2].Id,
                    AppUser = users[2],
                    MovieProjectionId = moviesProjection[2].MovieProjectionId,
                    MovieProjection = moviesProjection[2],
                    SeatId = seats[2].SeatId,
                    Seat = seats[2],
                    ReservationTime = GetDate(false),
                    ExpiresAt = GetDate(true).AddHours(4),
                    IsConfirmed = true,
                    Price = 200,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new BookingTicket
                {
                    AppUserId = users[3].Id,
                    AppUser = users[3],
                    MovieProjectionId = moviesProjection[3].MovieProjectionId,
                    MovieProjection = moviesProjection[3],
                    SeatId = seats[3].SeatId,
                    Seat = seats[3],
                    ReservationTime = GetDate(false),
                    ExpiresAt = GetDate(true).AddHours(5),
                    IsConfirmed = false,
                    Price = 220,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                }
            };
        }

        // Movie Projections
        private static List<MovieProjection> GetMovieProjections(List<Movie> movies, List<CinemaHall> cinemaHalls)
        {
            return new List<MovieProjection>
            {
                new MovieProjection
                {
                    MovieId = movies[0].MovieId,
                    Movie = movies[0],
                    ScreeningTime = GetDate(true),
                    ScreenType = ScreenType.IMAX,
                    CinemaHall = cinemaHalls[0],
                    CinemaHallId = cinemaHalls[0].CinemaHallId,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new MovieProjection
                {
                    MovieId = movies[1].MovieId,
                    Movie = movies[1],
                    ScreeningTime = GetDate(true),
                    ScreenType = ScreenType.ThreeD,
                    CinemaHall = cinemaHalls[1],
                    CinemaHallId = cinemaHalls[1].CinemaHallId,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new MovieProjection
                {
                    MovieId = movies[2].MovieId,
                    Movie = movies[2],
                    ScreeningTime = GetDate(true),
                    ScreenType = ScreenType.ThreeD,
                    CinemaHall = cinemaHalls[2],
                    CinemaHallId = cinemaHalls[2].CinemaHallId,
                    Created = DateTime.Now,
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new MovieProjection
                {
                    MovieId = movies[3].MovieId,
                    Movie = movies[3],
                    ScreeningTime = GetDate(true),
                    ScreenType = ScreenType.TwoD,
                    CinemaHall = cinemaHalls[3],
                    CinemaHallId = cinemaHalls[3].CinemaHallId,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                }
            };
        }

        // Seat
        private static List<Seat> GetSeats(List<CinemaHall> cinemaHalls)
        {
            var seats = new List<Seat>
            {
                new Seat
                {
                    Number = 1,
                    CinemaHallId = cinemaHalls[0].CinemaHallId,
                    CinemaHall = cinemaHalls[0],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Seat
                {
                    Number = 2,
                    CinemaHallId = cinemaHalls[0].CinemaHallId,
                    CinemaHall = cinemaHalls[0],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Seat
                {
                    Number = 3,
                    CinemaHallId = cinemaHalls[1].CinemaHallId,
                    CinemaHall = cinemaHalls[1],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Seat
                {
                    Number = 1,
                    CinemaHallId = cinemaHalls[1].CinemaHallId,
                    CinemaHall = cinemaHalls[1],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Seat
                {
                    Number = 2,
                    CinemaHallId = cinemaHalls[2].CinemaHallId,
                    CinemaHall = cinemaHalls[2],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                }
            };

            return seats;
        }
        private static async Task UpdateSeats(List<BookingTicket> bookingTickets, List<Seat> seats, CinemaDbContext dbContext)
        {
            foreach (var seat in seats)
            {
                seat.BookingTickets = bookingTickets;
                dbContext.Update(seat);
            }
            await dbContext.SaveChangesAsync();
        }

        // Schedule
        private static List<Schedule> GetSchedules(List<MovieProjection> movieProjections)
        {
            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> {movieProjections[0], movieProjections[1] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> {movieProjections[1], movieProjections[2] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> {movieProjections[2], movieProjections[3] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date =  DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> {movieProjections[3], movieProjections[0] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                }
            };
            return schedules;
        }

        // Cinema Hall
        private static List<CinemaHall> GetCinemaHalls()
        {
            var cinemaHalls = new List<CinemaHall>
            {
                new CinemaHall
                {
                    Name = "Hall A",
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false,
                },
                new CinemaHall
                {
                    Name = "Hall B",
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false,
                },
                new CinemaHall
                {
                    Name = "Hall C",
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false,
                },
                new CinemaHall
                {
                    Name = "Hall D",
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false,
                }
            };

            return cinemaHalls;
        }
        private static async Task UpdateCinemaHalls(List<Seat> cinemaSeats, List<CinemaHall> cinemaHalls, CinemaDbContext dbContext)
        {

            foreach (var cinHal in cinemaHalls)
            {
                cinHal.Seats = cinemaSeats;
                dbContext.Update(cinHal);
            }
            await dbContext.SaveChangesAsync();
        }


        // DB Methods
        private static async Task ClearDatabaseAsync(CinemaDbContext dbContext)
        {
            dbContext.RemoveRange(dbContext.Set<AppUser>());
            dbContext.RemoveRange(dbContext.Set<Movie>());
            dbContext.RemoveRange(dbContext.Set<MovieProjection>());
            dbContext.RemoveRange(dbContext.Set<CinemaHall>());
            dbContext.RemoveRange(dbContext.Set<Seat>());
            dbContext.RemoveRange(dbContext.Set<BookingTicket>());
            dbContext.RemoveRange(dbContext.Set<Photo>());
            dbContext.RemoveRange(dbContext.Set<Schedule>());

            await dbContext.SaveChangesAsync();

            Console.WriteLine("Wszystkie dane zostały usunięte z bazy danych.");
        }

        private static async Task AddEntitiesAsync<T>(CinemaDbContext dbContext, List<T> entities) where T : EntityBase
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }


        // User Methods
        private static async Task AddUsersWithRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, List<AppUser> users)
        {
            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email == null ? "" : user.Email);
                if (existingUser == null)
                {
                    var result = await userManager.CreateAsync(user, "Haslo123!");
                    if (result.Succeeded)
                    {
                        string role = user.Email!.Contains("admin") ? "Admin" :
                                      user.Email.Contains("manager") ? "Manager" : "User";

                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User", "Manager" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static List<AppUser> GetUsers()
        {
            var users = new List<AppUser>
            {
                new AppUser { UserName = "admin", FirstName = "Admin", LastName = "User", Email = "admin@example.com", Created = GetDate(false) },
                new AppUser { UserName = "user1", FirstName = "John", LastName = "Doe", Email = "user1@example.com", Created = GetDate(false) },
                new AppUser { UserName = "user2", FirstName = "Jane", LastName = "Doe", Email = "user2@example.com", Created = GetDate(false) },
                new AppUser { UserName = "manag", FirstName = "Sam", LastName = "Smith", Email = "manager@example.com", Created = GetDate(false) },
                new AppUser { UserName = "user3", FirstName = "Bob", LastName = "Brown", Email = "user3@example.com", Created = GetDate(false) }
            };

            return users;
        }


        // Photo Methods
        private static List<Photo> GetPhotos()
        {
            var photos = new List<Photo>();
            var paths = GetImagesPaths();

            foreach (var filePath in paths)
            {

                var photo = new Photo
                {
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false,
                    Description = Path.GetFileNameWithoutExtension(filePath),
                    FileExtension = "jpg",
                    Size = (new FileInfo(filePath).Length) / 1024.0, // Rozmiar w KB
                    Bytes = File.ReadAllBytes(filePath)
                };

                photos.Add(photo);
            }

            return photos;
        }

        private static IEnumerable<string> GetImagesPaths()
        {
            //string folderPath = Directory.GetCurrentDirectory();
            //Console.WriteLine("Aktualny katalog: " + folderPath);

            string folderPath = "../JCB_Cinema.Infrastructure/Data/Seed/InitialPhotos";

            if (!Directory.Exists(folderPath))
            {
                throw new ArgumentException("Folder not found at path: " + folderPath);
            }
            Console.WriteLine("Aktualny katalog: " + folderPath);

            return Directory.GetFiles(folderPath, "*.jpg");
        }


        // Movie Methods
        private static List<Movie> GetMovies(List<Photo> photos)
        {
            return new List<Movie> {
                new Movie
                {
                    Title = photos.FirstOrDefault(x => x.Description == "1917")!.Description!,
                    Description = "Dwóch brytyjskich żołnierzy zostaje wysłanych z misją, by przekazać ważne ostrzeżenie, ryzykując życie na frontach I wojny światowej.",
                    Duration = 148,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.War,
                    Poster = photos.FirstOrDefault(x => x.Description == "1917")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = photos.FirstOrDefault(x => x.Description == "The Dark Knight")!.Description!,
                    Description = "Batman faces the Joker in Gotham City.",
                    Duration = 152,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Action,
                    Poster = photos.FirstOrDefault(x => x.Description == "The Dark Knight")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = photos.FirstOrDefault(x => x.Description == "Matrix")!.Description!,
                    Description = "A hacker discovers the reality he lives in is a simulation.",
                    Duration = 136,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.ScienceFiction,
                    Poster = photos.FirstOrDefault(x => x.Description == "Matrix")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = photos.FirstOrDefault(x => x.Description == "Forrest Gump")!.Description!,
                    Description = "A simple man with a big heart experiences life and love.",
                    Duration = 142,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Drama,
                    Poster = photos.FirstOrDefault(x => x.Description == "Forrest Gump")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = photos.FirstOrDefault(x => x.Description == "No Time To Die")!.Description!,
                    Description = "James Bond returns on a mission to face a new, dangerous adversary while solving a mystery from the past.",
                    Duration = 142,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Spy,
                    Poster = photos.FirstOrDefault(x => x.Description == "No Time To Die")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                }
            };
        }


        // Date Methods
        private static DateTime GetDate(bool isFuture)
        {
            Random random = new Random();
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            int hourOffset = random.Next(1, 24);
            return isFuture ? today.AddDays(+daysOffset).AddHours(+hourOffset) : today.AddDays(-daysOffset).AddHours(-hourOffset);
        }

        private static DateOnly GetDateOnly(bool isFuture)
        {
            Random random = new Random();
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            var result = DateOnly.FromDateTime(isFuture ? today.AddDays(+daysOffset) : today.AddDays(-daysOffset));
            return result;
        }
    }
}
