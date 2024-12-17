using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Requests.Create
{
    public class AddMovieProjectionRequest
    {
        public string NormalizedTitle { get; set; } = null!;
        public DateTime ScreeningTime { get; set; }
        public ScreenType ScreenType { get; set; }
        public int CinemaHallId { get; set; }
    }
}
