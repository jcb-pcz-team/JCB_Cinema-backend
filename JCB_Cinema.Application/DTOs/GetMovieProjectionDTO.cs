using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieProjectionDTO
    {
        public int MovieProjectionId { get; init;  }
        public Movie Movie { get; init; } = null!;
        public DateTime ScreeningTime { get; init;  }
        public ScreenType ScreenType { get; init; }
        public CinemaHall CinemaHall { get; init;  } = null!;
        public Photo? Poster { get; init;  }

        public Price Price { get; init; } = null!;
        public int OccupiedSeats { get; init; }
        public int AvailableSeats { get; init; }
    }
}
