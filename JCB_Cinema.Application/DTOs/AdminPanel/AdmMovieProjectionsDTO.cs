namespace JCB_Cinema.Application.DTOs.AdminPanel
{
    public class AdmMovieProjectionDTO
    {
        public string MovieTitle { get; set; } = null!;
        public int? MovieDuration { get; set; }
        public string NormalizedMovieTitle { get; set; } = null!;

        public DateTime? ScreeningTime { get; init; }
        public string? ScreenType { get; init; }
        public string CinemaHallName { get; set; } = null!;

        public int? OccupiedSeats { get; init; }
        public int? AvailableSeats { get; init; }

        public GetPriceDTO? Price { get; init; }
    }
}
