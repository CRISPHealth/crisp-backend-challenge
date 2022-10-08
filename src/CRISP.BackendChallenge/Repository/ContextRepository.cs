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
        //throw new NotImplementedException();
        return _context.Set<T>().ToList();
    }

    /// <inheritdoc />
    public T GetById(int id)
    {
        //throw new NotImplementedException();
        return _context.Set<T>().SingleOrDefault(e => e.Id == id);
    }

    /// <inheritdoc />
    public void Add(T entity)
    {
        //throw new NotImplementedException();
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _context.Set<T>().Add(entity);
        Save();
    }

    /// <inheritdoc />
    public void Delete(T entity)
    {
        //throw new NotImplementedException();
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _context.Set<T>().Remove(entity);
        Save();
    }

    /// <inheritdoc />
    public void Update(T entity)
    {
        //throw new NotImplementedException();

        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        var t=GetById(entity.Id);
        if (t != null)
            Delete(t);
        Add(entity);
    }

    /// <inheritdoc />
    public void Save()
    {
        //throw new NotImplementedException();
        _context.SaveChanges();
    }
}