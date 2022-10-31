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
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        if (entity != null)
        {
            _context.Add(entity);
            Save();
        }  
        else
        {
            throw new ArgumentNullException("Entity was null.");
        }
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {    
        if (entity != null)
        {
            _context.Remove(entity);
            Save();
        }
        else
        {
            throw new ArgumentNullException("Entity was null.");
        }
    }

    /// <inheritdoc />
    public void Update(T entity)
    {        
        if (entity != null)
        {
            _context.Update(entity);
            Save();
        }
        else
        {
            throw new ArgumentNullException("Entity was null.");
        }
    }

    /// <inheritdoc />
    public void Save()
    {
        _context.SaveChanges();
    }
}