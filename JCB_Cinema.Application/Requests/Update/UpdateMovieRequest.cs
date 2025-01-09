namespace JCB_Cinema.Application.Requests.Update
{
    public class UpdateMovieRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Genre { get; set; } = null!;
        public bool? SetPreviousPoster { get; set; } = true;
    }
}
