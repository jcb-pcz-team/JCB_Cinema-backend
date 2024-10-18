namespace JCB_Cinema.Domain.Entities
{
    public abstract class EntityBase
    {
        // Filled in at the time of object creation
        public DateTime? Created { get; set; } // Defaulted to the current date
        public string? Creator { get; set; } // User who created the record

        // Filled in at the time of object modification
        public DateTime? Modified { get; set; } // Last modification date
        public string? Modifier { get; set; } // User who modified the record

        // Soft delete - not physically removed from the database, but marked as deleted
        public bool IsDeleted { get; set; } = false;

        // Abstract Key property, enforcing implementation in derived classes
        public abstract int Key { get; }
    }
}
