namespace JCB_Cinema.Domain.Entities
{
    public class Schedule : EntityBase
    {
        public int ScheduleId { get; set; }
        public DateOnly Date { get; set; }
        public List<MovieProjection> Screenings { get; set; } = new List<MovieProjection>();

        public override object Key => ScheduleId;
    }
}
