namespace JCB_Cinema.Application.Requests.Queries
{
    public class QueryPhotos
    {
        public int? Id { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
