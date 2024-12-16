using JCB_Cinema.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCB_Cinema.Domain.Entities
{
    public class MovieProjection : EntityBase
    {
        private readonly int _basePrice = 2250; // price in cents

        public int MovieProjectionId { get; set; }
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime ScreeningTime { get; set; }

        private ScreenType _screenType;
        public ScreenType ScreenType
        {
            get => _screenType;
            set
            {
                _screenType = value;
                UpdatePrice(); // Update Price when ScreenType changes
            }
        }

        public int CinemaHallId { get; set; }
        public CinemaHall? CinemaHall { get; set; }

        [NotMapped]
        public string MovieNormalizedTitle
        {
            get { return Movie == null ? "" : Movie.NormalizedTitle; }
        }

        public Price Price { get; private set; } // in cents!!!

        public MovieProjection()
        {
            _screenType = ScreenType.TwoD; // Default ScreenType
            Price = new Price(_basePrice + CalculateSubchargeForTicketPrice(_screenType), "pln");
        }

        [NotMapped]
        public int OccupiedSeats
        {
            get { return CinemaHall == null ? 0 : CinemaHall.Seats.Count(s => s.BookingTickets.Any(bt => bt.MovieProjectionId == MovieProjectionId)); }
        }

        [NotMapped]
        public int AvailableSeats
        {
            get { return CinemaHall == null ? 0 : CinemaHall.TotalSeats.HasValue ? CinemaHall.TotalSeats.Value - OccupiedSeats : 0; }
        }

        public override object Key => MovieProjectionId;

        private void UpdatePrice()
        {
            Price = new Price(_basePrice + CalculateSubchargeForTicketPrice(_screenType), "pln");
        }

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
