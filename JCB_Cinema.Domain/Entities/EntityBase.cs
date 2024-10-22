namespace JCB_Cinema.Domain.Entities
{
    public abstract class EntityBase
    {
        public DateTime? Created { get; set; }
        public int CreatorId { get; set; }
        public DateTime? Modified { get; set; }
        public int? ModifierId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public abstract int Key { get; }
    }
}
