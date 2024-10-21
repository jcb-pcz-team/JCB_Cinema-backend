namespace JCB_Cinema.Application.DTOs
{
    public class RequestMovies
    {
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public DateOnly? Release { get; set; }
    }
}
