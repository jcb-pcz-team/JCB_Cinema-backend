namespace JCB_Cinema.Domain.Entities
{
    public class CinemaHall : EntityBase
    {
        /*private readonly PriceCalculationService _priceCalculationService;

        public CinemaHall(PriceCalculationService priceCalculationService)
        {
            _priceCalculationService = priceCalculationService;
        }*/
        public int CinemaHallId { get; set; }  // Klucz główny
        public string Name { get; set; } = null!;  // Nazwa sali
        public int? TotalSeats { get; set; }  // Całkowita liczba miejsc w sali
        public List<Seat> Seats { get; set; } = new List<Seat>();  // Lista miejsc w sali
        public override int Key => CinemaHallId;
        /*public int Price
        {
            get
            {
                return _priceCalculationService.CalculatePrice(ScreenType, TotalSeats);
            }
        }*/
    }
}
