namespace JCB_Cinema.Application.DTOs.AdminPanel
{
    public class AdmScheduleDTO
    {
        public DateOnly Date { get; set; }
        public int MovieCount { get; set; }
        public int MovieProjectionsCount { get; set; }
        public int ActiveCinemaHalls { get; set; }
        public int TotalBookingTickets { get; set; }
    }
}
