using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Domain.Entities
{
    public class Movie : EntityBase
    {
        public int MovieId { get; set; }

        private string _title = null!;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NormalizedTitle = _title.NormalizeString();
            }
        }

        public string? Description { get; set; }
        public int? Duration { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public Genre? Genre { get; set; }
        public string? GenreDescription => Genre?.GetDescription();
        public int? PhotoId { get; set; }
        public Photo? Poster { get; set; }

        private string _normalizedTitle = null!;
        public string NormalizedTitle
        {
            get => _normalizedTitle;
            private set => _normalizedTitle = value;
        }

        public override object Key => MovieId;
    }
}
