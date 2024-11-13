namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieDTO
    {
        public int? MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public GetGenreDTO? Genere { get; set; }
        public string? PosterURL { get; set; }
        public DateOnly? Release { get; set; }
    }
}
