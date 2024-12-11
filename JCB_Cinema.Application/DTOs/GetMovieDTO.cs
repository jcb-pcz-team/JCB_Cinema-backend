namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieDTO
    {
        public int? MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public GetGenreDTO? Genre { get; set; }
        public string? NormalizedTitle { get; set; }
        public DateOnly? Release { get; set; }
    }
}
