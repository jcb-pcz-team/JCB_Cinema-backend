using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Infrastructure.Data.Seed
{
    public static class Seed
    {
        private static readonly Random random = new Random();

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

            // Update User.BookingTickets
            await UpdateUsers(bookingTickets, users, dbContext);

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

        public static string GeneratePhoneNumber()
        {
            string phoneNumber = string.Empty;
            for (int i = 0; i < 9; i++)
                phoneNumber += random.Next(0, 10);

            return phoneNumber;
        }
        private static List<AppUser> GetUsers()
        {
            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "User",
                    Street = "Main Street",
                    HouseNumber = "1A",
                    Town = "AdminTown",
                    Email = "admin@example.com",
                    PhoneNumber = GeneratePhoneNumber(),
                    Created = GetDate(false),
                    IsDeleted = false
                },
                new AppUser
                {
                    UserName = "user1",
                    FirstName = "John",
                    LastName = "Doe",
                    Street = "Broadway",
                    HouseNumber = "23",
                    Town = "Springfield",
                    Email = "user1@example.com",
                    PhoneNumber = GeneratePhoneNumber(),
                    Created = GetDate(false),
                    IsDeleted = false
                },
                new AppUser
                {
                    UserName = "user2",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Street = "Elm Street",
                    HouseNumber = "42B",
                    Town = "Springfield",
                    Email = "user2@example.com",
                    PhoneNumber = GeneratePhoneNumber(),
                    Created = GetDate(false),
                    IsDeleted = false
                },
                new AppUser
                {
                    UserName = "manager",
                    FirstName = "Sam",
                    LastName = "Smith",
                    Street = "Market Street",
                    HouseNumber = "7",
                    Town = "ManagerVille",
                    Email = "manager@example.com",
                    PhoneNumber = GeneratePhoneNumber(),
                    Created = GetDate(false),
                    IsDeleted = false
                },
                new AppUser
                {
                    UserName = "user3",
                    FirstName = "Bob",
                    LastName = "Brown",
                    Street = "Oak Avenue",
                    HouseNumber = "10",
                    Town = "OldTown",
                    Email = "user3@example.com",
                    PhoneNumber = GeneratePhoneNumber(),
                    Created = GetDate(false),
                    IsDeleted = false
                }
            };

            return users;
        }

        private static async Task UpdateUsers(List<BookingTicket> bookingTickets, List<AppUser> users, CinemaDbContext dbContext)
        {
            // Uwaga liczba userów musi być większa niż liczba booking tickets
            for (int i = 0; i < bookingTickets.Count; i++)
            {
                users[i].BookingTickets = new List<BookingTicket>() { bookingTickets[i] };
                dbContext.Update(users[i]);
            }
            await dbContext.SaveChangesAsync();
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
                    Description = Path.GetFileNameWithoutExtension(filePath).NormalizeString(),
                    FileExtension = Path.GetExtension(filePath),
                    Size = (new FileInfo(filePath).Length) / 1024.0, // Rozmiar w KB
                    Bytes = File.ReadAllBytes(filePath)
                };
                //Console.WriteLine(filePath);

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
            //Console.WriteLine("Aktualny katalog: " + folderPath);

            HashSet<string> allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".bmp", ".gif"
            };

            return Directory.GetFiles(folderPath, "*.*").Where(file => allowedExtensions.Contains(Path.GetExtension(file)));
        }


        // Movie Methods
        private static List<Movie> GetMovies(List<Photo> photos)
        {
            return new List<Movie> {
                new Movie
                {
                    Title = "1917",
                    Description = "Dwóch brytyjskich żołnierzy zostaje wysłanych z misją, by przekazać ważne ostrzeżenie, ryzykując życie na frontach I wojny światowej.",
                    Duration = 148,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.War,
                    Photo = photos.FirstOrDefault(x => x.Description == "1917")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Description = "Batman faces the Joker in Gotham City.",
                    Duration = 152,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Action,
                    Photo = photos.FirstOrDefault(x => x.Description == "the-dark-knight")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Matrix",
                    Description = "A hacker discovers the reality he lives in is a simulation.",
                    Duration = 136,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.ScienceFiction,
                    Photo = photos.FirstOrDefault(x => x.Description == "matrix")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Description = "A simple man with a big heart experiences life and love.",
                    Duration = 142,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Drama,
                    Photo = photos.FirstOrDefault(x => x.Description == "forrest-gump")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "No Time to Die",
                    Description = "James Bond returns on a mission to face a new, dangerous adversary while solving a mystery from the past.",
                    Duration = 142,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Spy,
                    Photo = photos.FirstOrDefault(x => x.Description == "no-time-to-die")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Die Hard",
                    Description = "Bruce Willis stars as New York police officer John McClane, who faces off against a group of terrorists taking over a Los Angeles skyscraper during a Christmas party.",
                    Duration = 120,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Action,
                    Photo = photos.FirstOrDefault(x => x.Description == "die-hard")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Interstellar",
                    Description = "Cooper, a former NASA pilot and devoted father, embarks on a mission through a wormhole to save humanity and secure a future for his children",
                    Duration = 129,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Drama,
                    Photo = photos.FirstOrDefault(x => x.Description == "interstellar")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "The Mandalorian",
                    Description = "Pedro Pascal stars as the Mandalorian, a lone bounty hunter navigating the outer reaches of the galaxy while protecting a mysterious child with extraordinary powers.",
                    Duration = 154,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.Adventure,
                    Photo = photos.FirstOrDefault(x => x.Description == "mandalorian")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Spider-Man: No Way Home",
                    Description = "Tom Holland stars as Spider-Man, who faces multiverse chaos and battles iconic villains from alternate realities while seeking to restore his secret identity and protect those he loves.",
                    Duration = 142,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.ScienceFiction,
                    Photo = photos.FirstOrDefault(x => x.Description == "spider-man-no-way-home")!,
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                },
                new Movie
                {
                    Title = "Venom: Last Dance",
                    Description = "Tom Hardy stars as Eddie Brock, who struggles to control the symbiote Venom while facing a new threat in the form of a powerful villain, forcing him to make difficult choices to protect those he cares about.",
                    Duration = 132,
                    ReleaseDate = GetDateOnly(true),
                    Genre = Genre.ScienceFiction,
                    Photo = photos.FirstOrDefault(x => x.Description == "venom-last-dance")!,
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
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            int hourOffset = random.Next(1, 24);
            return isFuture ? today.AddDays(+daysOffset).AddHours(+hourOffset) : today.AddDays(-daysOffset).AddHours(-hourOffset);
        }

        private static DateOnly GetDateOnly(bool isFuture)
        {
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            var result = DateOnly.FromDateTime(isFuture ? today.AddDays(+daysOffset) : today.AddDays(-daysOffset));
            return result;
        }
    }
}
