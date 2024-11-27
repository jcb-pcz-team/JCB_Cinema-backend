namespace JCB_Cinema.Application.Requests.Queries
{
    public class QueryAppUserEmail
    {
        public string CurrentEmail { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
    }
}
