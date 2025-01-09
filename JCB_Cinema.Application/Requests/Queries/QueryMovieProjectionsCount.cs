namespace JCB_Cinema.Application.Requests.Queries
{
    public class QueryMovieProjectionsCount
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? ScreenTypeName { get; set; }
        public int? CinemaHallId { get; set; }
        public string? MovieName { get; set; }
        public bool? DistinctActiveHalls { get; set; }
        public bool? DistinctMovies { get; set; }
    }
}
