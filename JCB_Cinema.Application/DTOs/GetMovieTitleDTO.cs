using JCB_Cinema.Tools;
using Microsoft.Office.SharePoint.Tools;

namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the title of a movie with normalization functionality.
    /// </summary>
    public class GetMovieTitleDTO
    {
        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the title of the movie. 
        /// It is initialized as an empty string by default.
        /// </value>
        public string Title { get; set; } = string.Empty;

        private string _normalizedTitle = string.Empty;

        /// <summary>
        /// Gets or sets the normalized version of the movie title.
        /// The title is automatically normalized using the <see cref="NormalizeString"/> method.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the normalized title of the movie.
        /// This value is set via a custom setter that applies string normalization.
        /// </value>
        public string NormalizedTitle
        {
            get => _normalizedTitle;
            set => _normalizedTitle = value.NormalizeString();
        }
    }
}
