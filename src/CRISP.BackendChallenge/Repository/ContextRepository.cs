using CRISP.Backend.Challenge.Context.Models;
using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Repository;

public class ContextRepository<T> : IRepository<T>  where T: BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> entities;

    public ContextRepository(ApplicationDbContext context)
    {
        _context = context;
        entities = context.Set<T>();
    }

    public IQueryable<T> Query()
        => _context.Set<T>().AsQueryable();

    /// <inheritdoc />
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        return entities.SingleOrDefault(e => e.Id == id);
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        entities.Remove(entity);
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        entities.Update(entity);
    }

    /// <inheritdoc />
    public void Save()
    {
        throw new NotImplementedException();
    }
}

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