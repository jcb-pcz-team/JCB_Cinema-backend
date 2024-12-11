namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieProjectionDTO
    {
        public GetMovieDTO? Movie { get; init; }
        public DateTime? ScreeningTime { get; init; }
        public string? ScreenType { get; init; }
        public GetCinemaHallDTO? CinemaHall { get; set; }
        public string? NormalizedMovieTitle { get; set; }
        public GetPriceDTO? Price { get; init; }
        public int? OccupiedSeats { get; init; }
        public int? AvailableSeats { get; init; }
    }
}
