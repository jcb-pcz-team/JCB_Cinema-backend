using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Domain.Entities
{
    public class Movie : EntityBase
    {
        public int MovieId { get; set; }  // Klucz główny
        public string Title { get; set; } = null!;  // Tytuł filmu
        public string? Description { get; set; }  // Opis filmu
        public int Duration { get; set; }  // Czas trwania w minutach
        public DateTime ReleaseDate { get; set; }  // Data premiery
        public Genre Genre { get; set; }  // Gatunek filmu
        public string GenreDescription => Genre.GetDescription();  // Opis gatunku
        public IList<Photo>? Posters { get; set; }
        public override int Key => MovieId;
    }
}
