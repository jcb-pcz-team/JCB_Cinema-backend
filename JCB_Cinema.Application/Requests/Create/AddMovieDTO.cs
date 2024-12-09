using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Requests.Create
{
    public class AddMovieDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public UploadPhoto? Poster { get; set; }
    }
}
