using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace CRISP.BackendChallenge.Repository;

public class ContextRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public ContextRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>().AsQueryable().Include("Logins");
    }

    /// <inheritdoc />
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        return Query().SingleOrDefault(t => t.Id == id);
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        _context.Add(entity);
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        _context.Update(entity);
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}
