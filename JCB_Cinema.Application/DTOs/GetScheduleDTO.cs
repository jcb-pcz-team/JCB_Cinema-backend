namespace JCB_Cinema.Application.DTOs
{
    public class GetScheduleDTO
    {
        public DateOnly Date { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }
        public string ScreenType { get; set; } = null!;
        public IList<GetMovieProjectionDTO> Screenings { get; set; } = new List<GetMovieProjectionDTO>();
    }
}
