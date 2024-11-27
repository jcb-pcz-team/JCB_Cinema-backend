namespace JCB_Cinema.Application.DTOs
{
    public class GetAppUserDTO
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public int? PhoneNumber { get; set; }
        public string? DialCode { get; set; }
    }
}
