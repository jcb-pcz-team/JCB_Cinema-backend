using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCB_Cinema.Infrastructure.Data.Seed
{
    /// <summary>
    /// Provides methods for initializing and seeding the database with test data.
    /// </summary>
    public static class Seed
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Initializes and seeds the database with test data.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve dependencies.</param>
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

            await AddEntitiesAsync(dbContext, cinemaHalls);
            cinemaHalls = await dbContext.CinemaHalls.ToListAsync();

            var seats = GetSeats(cinemaHalls);
            await AddEntitiesAsync(dbContext, seats);

            var movieProjections = GetMovieProjections(movies, cinemaHalls);

            var schedule = GetSchedules(movieProjections);

            var bookingTickets = GetBookingTickets(users, movieProjections, seats);

            await EnsureRolesAsync(roleManager);
            await AddUsersWithRolesAsync(userManager, roleManager, users);
            await AddEntitiesAsync(dbContext, photos);
            await AddEntitiesAsync(dbContext, movies);

            //await UpdateCinemaHalls(await dbContext.Seats.ToListAsync(), await dbContext.CinemaHalls.ToListAsync(), dbContext);

            await AddEntitiesAsync(dbContext, movieProjections);
            await AddEntitiesAsync(dbContext, schedule);
            await AddEntitiesAsync(dbContext, bookingTickets);

            await UpdateUsers(bookingTickets, users, dbContext);
            await UpdateSeats(await dbContext.BookingTickets.ToListAsync(), await dbContext.Seats.ToListAsync(), dbContext);
        }

        /// <summary>
        /// Generates a list of booking tickets.
        /// </summary>
        /// <param name="users">The list of users.</param>
        /// <param name="movieProjections">The list of movie projections.</param>
        /// <param name="seats">The list of seats.</param>
        /// <returns>A list of <see cref="BookingTicket"/> objects.</returns>
        private static List<BookingTicket> GetBookingTickets(List<AppUser> users, List<MovieProjection> movieProjections, List<Seat> seats)
        {
            var bookingTickets = new List<BookingTicket>();

            var limitedSeats = seats.Take(seats.Count / 2).ToList();

            foreach (var seat in limitedSeats)
            {
                foreach (var user in users.Take(users.Count / 2)) // Ograniczenie liczby użytkowników
                {
                    foreach (var projection in movieProjections.Take(movieProjections.Count / 2)) // Ograniczenie liczby projekcji
                    {
                        var isConfirmed = false;
                        var val = random.Next(0, 2);
                        if (val == 1)
                            isConfirmed = true;

                        bookingTickets.Add(new BookingTicket
                        {
                            AppUserId = user.Id,
                            AppUser = user,
                            MovieProjectionId = projection.MovieProjectionId,
                            MovieProjection = projection,
                            SeatId = seat.SeatId,
                            Seat = seat,
                            ReservationTime = GetDate(false),
                            ExpiresAt = GetDate(false).AddHours(2),
                            IsConfirmed = isConfirmed,
                            Price = random.Next(80, 190),
                            Created = GetDate(false),
                            Creator = "System",
                            Modified = DateTime.Now,
                            Modifier = "System",
                            IsDeleted = false
                        });
                    }
                }
            }

            return bookingTickets;
        }

        /// <summary>
        /// Generates a list of movie projections.
        /// </summary>
        /// <param name="movies">The list of movies.</param>
        /// <param name="cinemaHalls">The list of cinema halls.</param>
        /// <returns>A list of <see cref="MovieProjection"/> objects.</returns>
        private static List<MovieProjection> GetMovieProjections(List<Movie> movies, List<CinemaHall> cinemaHalls)
        {
            var projections = new List<MovieProjection>();

            for (int i = 0; i < movies.Count; i++)
            {
                var CinemaHallId = random.Next(0, cinemaHalls.Count);

                projections.Add(new MovieProjection
                {
                    MovieId = movies[i].MovieId,
                    Movie = movies[i],
                    ScreeningTime = GetDate(true),
                    ScreenType = (ScreenType)(i % Enum.GetValues(typeof(ScreenType)).Length),
                    CinemaHallId = cinemaHalls[CinemaHallId].CinemaHallId,
                    CinemaHall = cinemaHalls[CinemaHallId],
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System"
                });
            }

            return projections;
        }

        /// <summary>
        /// Generates a list of seats for the cinema halls.
        /// </summary>
        /// <param name="cinemaHalls">The list of cinema halls.</param>
        /// <returns>A list of <see cref="Seat"/> objects.</returns>
        private static List<Seat> GetSeats(List<CinemaHall> cinemaHalls)
        {
            var seats = new List<Seat>();

            foreach (var hall in cinemaHalls)
            {
                for (int seatNumber = 1; seatNumber <= 25; seatNumber++)
                {
                    seats.Add(new Seat
                    {
                        Number = seatNumber,
                        CinemaHallId = hall.CinemaHallId,
                        CinemaHall = hall,
                        Created = GetDate(false),
                        Creator = "System",
                        Modified = DateTime.Now,
                        Modifier = "System",
                        IsDeleted = false
                    });
                }
            }

            return seats;
        }

        /// <summary>
        /// Updates seats with associated booking tickets.
        /// </summary>
        /// <param name="bookingTickets">The list of booking tickets.</param>
        /// <param name="seats">The list of seats.</param>
        /// <param name="dbContext">The database context.</param>
        private static async Task UpdateSeats(List<BookingTicket> bookingTickets, List<Seat> seats, CinemaDbContext dbContext)
        {
            var groupedTickets = bookingTickets.GroupBy(bt => bt.SeatId).ToList();

            foreach (var group in groupedTickets)
            {
                var seat = await dbContext.Seats.FindAsync(group.Key);
                if (seat != null)
                {
                    seat.BookingTickets = group.ToList();
                    dbContext.Update(seat);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Generates a list of cinema halls.
        /// </summary>
        /// <returns>A list of <see cref="CinemaHall"/> objects.</returns>
        private static List<CinemaHall> GetCinemaHalls()
        {
            return new List<CinemaHall>
        {
            new CinemaHall { Name = "Hall A", Created = GetDate(false), Creator = "System", Modified = DateTime.Now, Modifier = "System", IsDeleted = false },
            new CinemaHall { Name = "Hall B", Created = GetDate(false), Creator = "System", Modified = DateTime.Now, Modifier = "System", IsDeleted = false },
            new CinemaHall { Name = "Hall C", Created = GetDate(false), Creator = "System", Modified = DateTime.Now, Modifier = "System", IsDeleted = false },
            new CinemaHall { Name = "Hall D", Created = GetDate(false), Creator = "System", Modified = DateTime.Now, Modifier = "System", IsDeleted = false }
        };
        }

        /// <summary>
        /// Generates a list of schedules for movie projections.
        /// </summary>
        /// <param name="movieProjections">The list of movie projections.</param>
        /// <returns>A list of <see cref="Schedule"/> objects.</returns>
        private static List<Schedule> GetSchedules(List<MovieProjection> movieProjections)
        {
            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> { movieProjections[0], movieProjections[1] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> { movieProjections[1], movieProjections[2] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> { movieProjections[2], movieProjections[3] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                },
                new Schedule
                {
                    Date = DateOnly.FromDateTime(GetDate(true)),
                    Screenings = new List<MovieProjection> { movieProjections[3], movieProjections[0] },
                    Created = GetDate(false),
                    Creator = "System",
                    Modified = DateTime.Now,
                    Modifier = "System",
                    IsDeleted = false
                }
            };

            return schedules;
        }

        /// <summary>
        /// Updates cinema halls with their associated seats.
        /// </summary>
        /// <param name="cinemaSeats">The list of seats in the cinema.</param>
        /// <param name="cinemaHalls">The list of cinema halls.</param>
        /// <param name="dbContext">The database context.</param>
        private static async Task UpdateCinemaHalls(List<Seat> cinemaSeats, List<CinemaHall> cinemaHalls, CinemaDbContext dbContext)
        {
            foreach (var cinHal in cinemaHalls)
            {
                cinHal.Seats = cinemaSeats;
                dbContext.Update(cinHal);
            }
            await dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Clears all data from the database.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
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

        /// <summary>
        /// Adds a list of entities to the database.
        /// </summary>
        /// <typeparam name="T">The type of the entities.</typeparam>
        /// <param name="dbContext">The database context.</param>
        /// <param name="entities">The list of entities to add.</param>
        private static async Task AddEntitiesAsync<T>(CinemaDbContext dbContext, List<T> entities) where T : EntityBase
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Adds users to the database and assigns roles based on their email address.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="roleManager">The role manager instance.</param>
        /// <param name="users">The list of users to add.</param>
        private static async Task AddUsersWithRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, List<AppUser> users)
        {
            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email ?? "");
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

        /// <summary>
        /// Ensures that predefined roles exist in the system.
        /// </summary>
        /// <param name="roleManager">The role manager instance.</param>
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

        /// <summary>
        /// Generates a random 9-digit phone number.
        /// </summary>
        /// <returns>A randomly generated phone number as a string.</returns>
        public static string GeneratePhoneNumber()
        {
            string phoneNumber = string.Empty;
            for (int i = 0; i < 9; i++)
                phoneNumber += random.Next(0, 10);

            return phoneNumber;
        }

        /// <summary>
        /// Retrieves a list of predefined users.
        /// </summary>
        /// <returns>A list of <see cref="AppUser"/> objects.</returns>
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

        /// <summary>
        /// Updates users with associated booking tickets.
        /// </summary>
        /// <param name="bookingTickets">The list of booking tickets.</param>
        /// <param name="users">The list of users to update.</param>
        /// <param name="dbContext">The database context.</param>
        private static async Task UpdateUsers(List<BookingTicket> bookingTickets, List<AppUser> users, CinemaDbContext dbContext)
        {
            // Uwaga liczba userów musi być większa niż liczba booking tickets
            for (int i = 0; i < users.Count; i++)
            {
                users[i].BookingTickets = dbContext.BookingTickets.Where(bt => bt.AppUserId == users[i].Id).ToList();
                dbContext.Update(users[i]);
            }
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of photos from predefined image paths.
        /// </summary>
        /// <returns>A list of <see cref="Photo"/> objects.</returns>
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
                    Size = (new FileInfo(filePath).Length) / 1024.0, // Size in KB
                    Bytes = File.ReadAllBytes(filePath)
                };

                photos.Add(photo);
            }

            return photos;
        }

        #pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        #pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Retrieves the file paths of images in a predefined folder.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{string}"/> containing the file paths of images in the folder.</returns>
        /// <exception cref="ArgumentException">Thrown if the folder does not exist.</exception>
        private static IEnumerable<string> GetImagesPaths()
        #pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
        #pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
        {
            // Set the folder path for image files.
            string folderPath = "../JCB_Cinema.Infrastructure/Data/Seed/InitialPhotos";

            // Check if the folder exists.
            if (!Directory.Exists(folderPath))
            {
                throw new ArgumentException("Folder not found at path: " + folderPath);
            }

            // Define allowed image extensions.
            HashSet<string> allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".bmp", ".gif"
            };

            // Return file paths of images with the allowed extensions.
            return Directory.GetFiles(folderPath, "*.*").Where(file => allowedExtensions.Contains(Path.GetExtension(file)));
        }


        /// <summary>
        /// Retrieves a list of predefined movies with associated photos.
        /// </summary>
        /// <param name="photos">A list of <see cref="Photo"/> objects to associate with movies.</param>
        /// <returns>A list of <see cref="Movie"/> objects.</returns>
        private static List<Movie> GetMovies(List<Photo> photos)
        {
            return new List<Movie>
            {
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
                    Photo = photos.FirstOrDefault(x => x.Description == "spiderman-no-way-home")!,
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

        /// <summary>
        /// Generates a random date and time offset by a random number of days and hours.
        /// </summary>
        /// <param name="isFuture">Determines whether to generate a future or past date.</param>
        /// <returns>A <see cref="DateTime"/> object with a random date and time.</returns>
        private static DateTime GetDate(bool isFuture)
        {
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            int hourOffset = random.Next(1, 24);
            return isFuture ? today.AddDays(+daysOffset).AddHours(+hourOffset) : today.AddDays(-daysOffset).AddHours(-hourOffset);
        }

        /// <summary>
        /// Generates a random date (without time) offset by a random number of days.
        /// </summary>
        /// <param name="isFuture">Determines whether to generate a future or past date.</param>
        /// <returns>A <see cref="DateOnly"/> object with a random date.</returns>
        private static DateOnly GetDateOnly(bool isFuture)
        {
            DateTime today = DateTime.Now;
            int daysOffset = random.Next(0, 4);
            var result = DateOnly.FromDateTime(isFuture ? today.AddDays(+daysOffset) : today.AddDays(-daysOffset));
            return result;
        }
    }
}
