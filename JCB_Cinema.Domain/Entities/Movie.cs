using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Domain.Entities
{
    public class Movie : EntityBase
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Genre? Genre { get; set; }
        public string? GenreDescription => Genre?.GetDescription();
        public Photo? Poster { get; set; }
        public override int Key => MovieId;
    }
}
