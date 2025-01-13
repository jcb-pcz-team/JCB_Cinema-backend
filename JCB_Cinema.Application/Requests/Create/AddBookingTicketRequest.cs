namespace JCB_Cinema.Application.Requests.Create
{
    public class AddBookingTicketRequest
    {
        public int MovieProjectionId { get; set; }
        public int SeatId { get; set; }
    }
}
