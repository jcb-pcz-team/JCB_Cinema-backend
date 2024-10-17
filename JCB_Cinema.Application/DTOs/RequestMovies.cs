namespace JCB_Cinema.Application.DTOs
{
    public class RequestMovies
    {
        public int? GenereId { get; set; }
        public string? GenereName { get; set; }
        public DateOnly? Release { get; set; }
    }
}
