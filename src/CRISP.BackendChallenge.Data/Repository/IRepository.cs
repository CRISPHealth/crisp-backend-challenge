using CRISP.BackendChallenge.Data.Context.Models;

namespace CRISP.BackendChallenge.Data.Repository
{
    /// <summary>
    /// Interface for the repository.
    /// </summary>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Generic method that will allow for more complex EF queries.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Query();

        /// <summary>
        /// Get All Entities available in the database.
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <returns>An enumerable of the Entity Type</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get an instance of the Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Add an entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        void Add(T entity);

        /// <summary>
        /// Delete an entity from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        void Delete(T entity);

        /// <summary>
        /// Update an entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        void Update(T entity);

        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        void Save();
    }
}