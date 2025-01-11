namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents the base class for all entities in the JCB Cinema domain.
    /// Provides common properties for tracking creation, modification, and deletion status.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the timestamp when the entity was created.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the username or identifier of the user who created the entity.
        /// </summary>
        public string Creator { get; set; } = null!;

        /// <summary>
        /// Gets or sets the timestamp when the entity was last modified.
        /// </summary>
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Gets or sets the username or identifier of the user who last modified the entity.
        /// This value is <c>null</c> if the entity has not been modified since creation.
        /// </summary>
        public string? Modifier { get; set; }

        /// <summary>
        /// Indicates whether the entity is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets the unique key of the entity.
        /// This property must be implemented by derived classes to provide entity-specific identification.
        /// </summary>
        public abstract object Key { get; }
    }
}
