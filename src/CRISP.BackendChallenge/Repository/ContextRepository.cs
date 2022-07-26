using System.Linq.Expressions;
using CRISP.Backend.Challenge.Context;
using CRISP.BackendChallenge.Context;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Repository;

public class ContextRepository : IRepository
{
    private readonly ApplicationDbContext _context;

    public ContextRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query<T>() where T : class
        => _context.Set<T>().AsQueryable();

    /// <inheritdoc />
    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>().AsNoTracking();
    }

    /// <inheritdoc />
    public T GetById<T>(int id) where T : class
    {
        DbSet<T> table = _context.Set<T>();
        var result = table
            .Find(id);

        return result;
    }

    /// <inheritdoc />
    public void Add<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    /// <inheritdoc />
    public void Delete<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    /// <inheritdoc />
    public void Save()
    {
        throw new NotImplementedException();
    }
}

public interface IRepository
{
    /// <summary>
    /// Generic method that will allow for more complex EF queries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IQueryable<T> Query<T>() where T : class;

    /// <summary>
    /// Get All Entities available in the database.
    /// </summary>
    /// <typeparam name="T">The type of entity</typeparam>
    /// <returns>An enumerable of the Entity Type</returns>
    IEnumerable<T> GetAll<T>() where T : class;

    /// <summary>
    /// Get an instance of the Entity by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetById<T>(int id) where T : class;

    /// <summary>
    /// Add an entity to the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    void Add<T>(T entity) where T : class;

    /// <summary>
    /// Delete an entity from the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    void Delete<T>(T entity) where T : class;

    /// <summary>
    /// Update an entity in the database.
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    void Update<T>(T entity) where T : class;

    /// <summary>
    /// Save the changes to the database.
    /// </summary>
    void Save();
}