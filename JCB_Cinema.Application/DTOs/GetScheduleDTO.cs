namespace JCB_Cinema.Application.DTOs
{
    public class GetScheduleDTO
    {
        public DateOnly Date { get; set; }
        public IList<GetMovieProjectionDTO> Screenings { get; set; } = new List<GetMovieProjectionDTO>();
    }
}
