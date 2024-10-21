namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieDTO
    {
        public int MovieId { get; set; }
        public string? Description { get; set; }
        public GetGenreDTO? Genere { get; set; }
        public int PosterId { get; set; }
        public DateOnly Release { get; set; }
    }
}
