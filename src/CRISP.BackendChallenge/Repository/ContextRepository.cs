using System.Linq.Expressions;
using CRISP.Backend.Challenge.Context;
using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;
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
	public async Task Add<T>(T entity) where T : class
	{
		_context.Set<T>().Add(entity);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc />
	public async Task Delete<T>(T entity) where T : class
	{
		_context.Set<T>().Remove(entity);
		await _context.SaveChangesAsync();
	}

	/// <inheritdoc />
	public void Update<T>(T entity) where T : class
	{
		_context.Set<T>().Update(entity);
		_context.SaveChanges();
	}

	/// <inheritdoc />
	public void Save(Employee employee)
	{
		_context.Entry(employee).State = EntityState.Modified;
		_context.SaveChanges();
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Employee>> Search(int? id, string? name, Department? department)
	{
		IQueryable<Employee> query = _context.Employees;

		if (id != null)
		{
			query = query.Where(e => e.Id == id);
		}

		if (!string.IsNullOrEmpty(name))
		{
			query = query.Where(e => e.Name.Contains(name));
		}

		if (department != null)
		{
			query = query.Where(e => e.Department == department);
		}

		return await query
			.Include(e => e.Logins.OrderByDescending(l => l.LoginDate))
			.ToListAsync();
	}

	/// <inheritdoc />
	public bool FindEmployee(int employeeId)
	{
		return _context.Employees.Any(e => e.Id == employeeId);
	}

	public Employee GetEmployeeWithLogins(int id)
	{
		var employee = _context.Employees
			.Where(b => b.Id == id)
			.Include(b => b.Logins)
			.FirstOrDefault();

		return employee;
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
	Task Add<T>(T entity) where T : class;

	/// <summary>
	/// Delete an entity from the database.
	/// </summary>
	/// <param name="entity"></param>
	/// <typeparam name="T"></typeparam>
	Task Delete<T>(T entity) where T : class;

	/// <summary>
	/// Update an entity in the database.
	/// </summary>
	/// <param name="entity"></param>
	/// <typeparam name="T"></typeparam>
	void Update<T>(T entity) where T : class;

	/// <summary>
	/// Save the changes to the database.
	/// </summary>
	void Save(Employee employee);

	/// <summary>
	/// Finds an employee in the repository, if it exists
	/// </summary>
	/// <param name="employeeId">The id of the employee to find</param>
	/// <returns></returns>
	bool FindEmployee(int employeeId);

	/// <summary>
	/// Allows for a search for an employee based on one or more criteria
	/// </summary>
	/// <param name="id">The id of the employee</param>
	/// <param name="name">The fullname of the employee</param>
	/// <param name="department">The enumerated department of the employee</param>
	/// <returns></returns>
	Task<IEnumerable<Employee>> Search(int? id, string? name, Department? department);

	/// <summary>
	/// Gets an employee in the repository with the associated logins
	/// </summary>
	/// <param name="employeeId">The id of the employee to find</param>
	/// <returns></returns>
	Employee GetEmployeeWithLogins(int employeeId);
}