namespace JCB_Cinema.Application.Requests
{
    public class RequestMovies
    {
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public DateOnly? Release { get; set; }
    }
}
