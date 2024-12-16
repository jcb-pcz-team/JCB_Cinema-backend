using JCB_Cinema.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.Requests.Create
{
    public class AddMovieProjectionDTO
    {
        public DateTime ScreeningTime { get; set; }
        public ScreenType ScreenType { get; set; }
        [Required]
        public int CinemaHallId { get; set; }
        [Required]
        public string MovieNormalizedTitle { get; set; } = string.Empty;
    }
}
