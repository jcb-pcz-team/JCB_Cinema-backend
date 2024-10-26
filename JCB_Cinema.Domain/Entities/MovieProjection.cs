using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Domain.Entities
{
    public class MovieProjection : EntityBase
    {
        private readonly int _basePrice = 2250; // price in cents

        public int MovieProjectionId { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public DateTime ScreeningTime { get; set; }
        public ScreenType ScreenType { get; set; }

        public int CinemHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;
        public Photo? Poster { get; set; }

        public Price Price { get; private set; }// in cents!!!

        public MovieProjection()
        {
            Price = new Price(_basePrice + CalculateSubchargeForTicketPrice(ScreenType), "pln");
        }
        public int OccupiedSeats
        {
            get
            {
                return CinemaHall.Seats.Count(s => s.BookingTickets.Any(bt => bt.MovieProjectionId == MovieProjectionId));
            }
        }

        public int AvailableSeats
        {
            get
            {
                return CinemaHall.TotalSeats.HasValue ? CinemaHall.TotalSeats.Value - OccupiedSeats : 0;
            }
        }
        public override object Key => MovieProjectionId;

        public static int CalculateSubchargeForTicketPrice(ScreenType screenType = ScreenType.TwoD)
        {
            int surcharge = 0;

            switch (screenType)
            {
                case ScreenType.TwoD:
                    surcharge = 0;
                    break;
                case ScreenType.ThreeD:
                    surcharge = 690;
                    break;
                case ScreenType.IMAX:
                    surcharge = 1230;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null);
            }

            return surcharge; // returns subcharge for ticket
        }
    }
}
