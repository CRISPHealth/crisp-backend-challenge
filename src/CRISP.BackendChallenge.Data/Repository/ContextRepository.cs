using CRISP.BackendChallenge.Data.Context;
using CRISP.BackendChallenge.Data.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Data.Repository;

public class ContextRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> entities;

    public ContextRepository(ApplicationDbContext context)
    {
        _context = context;
        this.entities = context.Set<T>();
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable();
    }

    /// <inheritdoc />
    public IEnumerable<T> GetAll()
    {
        return entities.AsEnumerable();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        var result = (from e in entities where e.Id == id select e);
#pragma warning disable CS8603 // Possible null reference return.
        return result.FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        // TODO: If we don't allow logins to be created, block it here not in the consumer.  Problem with generic repository though...
        entities.Add(entity);
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        // TODO: If we don't allow logins to be updated, block it here not in the consumer.  Problem with generic repository though...
        _context.Update(entity);
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges(true);
    }
}