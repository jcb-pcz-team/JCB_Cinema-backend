namespace JCB_Cinema.Application.Requests.Queries
{
    public class QueryMovies
    {
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public DateOnly? Release { get; set; }
    }
}
