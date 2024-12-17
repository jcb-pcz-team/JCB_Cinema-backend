using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.DTOs
{
    public class GetMovieTitleDTO
    {
        public string Title { get; set; } = string.Empty;
        private string _normalizedTitle = string.Empty;
        public string NormalizedTitle
        {
            get => _normalizedTitle;
            set => _normalizedTitle = value.NormalizeString();
        }
    }
}
