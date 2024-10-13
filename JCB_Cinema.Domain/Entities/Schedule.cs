namespace JCB_Cinema.Domain.Entities
{
    public class Schedule : EntityBase
    {
        public int ScheduleId { get; set; }  // Klucz główny
        public DateOnly Date { get; set; }  // Dzień repertuaru
        public List<MovieProjection> Screenings { get; set; } = new List<MovieProjection>();  // Lista projekcji filmów

        public override int Key => ScheduleId;
    }

}
