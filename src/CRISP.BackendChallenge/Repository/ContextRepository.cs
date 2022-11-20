using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;

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
        return _context.Set<T>().AsQueryable();
    }

    /// <inheritdoc />
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        return _context.Find<T>(id);
        //throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        _context.Add<T>(entity);
        //throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        _context.Remove<T>(entity);
        //throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        _context.Update<T>(entity);
        //throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}