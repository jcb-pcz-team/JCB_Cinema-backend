using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a movie entity in the JCB Cinema domain.
    /// Stores information about a movie including title, description, duration, and associated metadata.
    /// </summary>
    public class Movie : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the movie.
        /// </summary>
        public int MovieId { get; set; }

        private string _title = null!;

        /// <summary>
        /// Gets or sets the title of the movie.
        /// Setting this property also updates the normalized version of the title.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NormalizedTitle = _title.NormalizeString();
            }
        }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// This can include a brief summary or plot details.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        public DateOnly? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie.
        /// </summary>
        public Genre? Genre { get; set; }

        /// <summary>
        /// Gets the human-readable description of the movie's genre.
        /// </summary>
        public string? GenreDescription => Genre?.GetDescription();

        /// <summary>
        /// Gets or sets the photo identifier associated with the movie.
        /// </summary>
        public int? PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo entity associated with the movie.
        /// </summary>
        public Photo? Photo { get; set; }

        private string _normalizedTitle = null!;

        /// <summary>
        /// Gets the normalized version of the movie's title.
        /// This value is automatically updated when the title is set.
        /// </summary>
        public string NormalizedTitle
        {
            get => _normalizedTitle;
            private set => _normalizedTitle = value;
        }

        /// <summary>
        /// Gets the unique key of the movie entity.
        /// </summary>
        public override object Key => MovieId;
    }
}
