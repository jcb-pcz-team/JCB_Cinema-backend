namespace JCB_Cinema.Application.DTOs
{
    public class GetAppUserEmailDTO
    {
        public string CurrentEmail { get; set; } = null!;
        public string NewEmail { get; } = string.Empty;
    }
}
