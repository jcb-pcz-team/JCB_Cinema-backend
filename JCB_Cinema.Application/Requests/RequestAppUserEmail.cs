namespace JCB_Cinema.Application.Requests
{
    public class RequestAppUserEmail
    {
        public string CurrentEmail { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
    }
}
