namespace CRISP.BackendChallenge.Data.Context.Models
{
    /// <summary>
    /// Base entity for all models.
    /// </summary>

    public abstract class BaseEntity
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public int Id { get; set; }
    }
}