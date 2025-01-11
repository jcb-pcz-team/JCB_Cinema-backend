namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents different filters for querying the status of an entity.
    /// </summary>
    public enum EntityStatusFilter
    {
        /// <summary>
        /// Filter to include entities that exist (are not deleted).
        /// </summary>
        Exists,

        /// <summary>
        /// Filter to include all entities, regardless of their deletion status.
        /// </summary>
        All,

        /// <summary>
        /// Filter to include only entities that are marked as deleted.
        /// </summary>
        Deleted
    }
}
