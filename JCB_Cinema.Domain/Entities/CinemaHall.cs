using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Domain.Entities
{
    public class CinemaHall : EntityBase
    {
        /*private readonly PriceCalculationService _priceCalculationService;

        public CinemaHall(PriceCalculationService priceCalculationService)
        {
            _priceCalculationService = priceCalculationService;
        }*/
        public int HallId { get; set; }  // Klucz główny
        public string Name { get; set; } = null!;  // Nazwa sali
        public int? TotalSeats { get; set; }  // Całkowita liczba miejsc w sali
        public ScreenType ScreenType { get; set; } // Typ ekranu
        public List<Seat> Seats { get; set; } = new List<Seat>();  // Lista miejsc w sali
        public string ScreenTypeDescription => ScreenType.GetDescription();  // Opis gatunku
        public override int Key => HallId;
        /*public int Price
        {
            get
            {
                return _priceCalculationService.CalculatePrice(ScreenType, TotalSeats);
            }
        }*/
    }
}
