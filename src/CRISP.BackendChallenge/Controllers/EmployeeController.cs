using CRISP.Backend.Challenge.Context;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.DTO;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CRISP.BackendChallenge.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SQLitePCL;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
	private readonly ILogger<EmployeeController> _logger;
	private readonly IRepository _repository;

	public EmployeeController(ILogger<EmployeeController> logger, IRepository repository)
	{
		_logger = logger;
		_repository = repository;
	}

	// POST: api/CreateEmployee
	[HttpPost, Route("api/create")]
	public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto employeeDTO)
	{
		var employee = new Employee
		{
			Name = employeeDTO.Name
		};

		await _repository.Add<Employee>(employee);

		//return Ok(); //CreatedAtAction(nameof(GetById), new { id = employee.Id }, todoItem);
		return CreatedAtAction("GetById", new {id = employee.Id}, employee.ConvertToEmployeeDto());
	}

	[HttpGet, Route("api/all")]
	public IActionResult GetAll()
	{
		_logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));

		var result = _repository.Query<Employee>()
			.Include(l => l.Logins.OrderBy(login => login.LoginDate)).ToList();

		return Ok(result);
	}

	// GET: api/employee/id
	[HttpGet("api/{id}")]
	public ActionResult<Employee> GetById(int id)
	{
		_logger.LogDebug(":: Performing {MethodName}", nameof(GetById));

		var employee = _repository.GetById<Employee>(id);

		if (employee == null)
		{
			return NotFound();
		}

		var result = _repository.GetEmployeeWithLogins(id);

		return result;
	}

	// DELETE: api/employee/id
	[HttpDelete("api/delete/{id}")]
	public async Task<IActionResult> DeleteEmployee(int id)
	{
		_logger.LogDebug(":: Performing {MethodName}", nameof(DeleteEmployee));

		Employee existingEmployee = _repository.GetById<Employee>(id);

		if (existingEmployee == null)
		{
			return NotFound();
		}

		await _repository.Delete<Employee>(existingEmployee);

		return NoContent();
	}

	// PUT: api/employee/id
	[HttpPut("api/updatebyid/{id}")]
	public async Task<IActionResult> UpdateById(int id, EmployeeUpdateRequest employeeDto)
	{
		_logger.LogDebug(":: Performing {MethodName}", nameof(UpdateById));

		var existingEmployee = _repository.GetById<Employee>(id);

		if (existingEmployee == null)
		{
			return NotFound();
		}

		existingEmployee.Name = employeeDto.Name;

		try
		{
			_repository.Save(existingEmployee);
		}
		catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
		{
			return NotFound();
		}

		return NoContent();
	}

	// PUT: api/employee/name
	[HttpPut("api/updatebyname/{name}")]
	public async Task<IActionResult> UpdateByName(string name, EmployeeUpdateRequest employeeDto)
	{
		_logger.LogDebug(":: Performing {MethodName}", nameof(UpdateByName));

		if (string.IsNullOrWhiteSpace(employeeDto.Name))
		{
			return BadRequest("A valid name must be specified.");
		}

		var result = _repository.Query<Employee>()
			.Where(e => e.Name.Contains(name))
			.FirstOrDefault();

		if (result == null)
		{
			return NotFound();
		}

		var existingEmployee = _repository.GetById<Employee>(result.Id);

		// Assign the value to be updated
		existingEmployee.Name = employeeDto.Name;

		try
		{
			_repository.Save(existingEmployee);
		}
		catch (DbUpdateConcurrencyException) when (!EmployeeExists(result.Id))
		{
			return NotFound();
		}

		return NoContent();
	}

	[HttpGet("api/search")]
	public async Task<ActionResult<IEnumerable<Employee>>> Search(int? id, string? name, Department? department)
	{
		if ((id == null) && (string.IsNullOrEmpty(name)) && (department == null))
		{
			return BadRequest("At least one search parameter must be present.");
		}

		try
		{
			var result = await _repository.Search(id, name, department);

			if (result.Any())
			{
				return Ok(result);
			}

			return NotFound();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrieving data from the database");
		}
	}

	/// <summary>
	/// A private helper method to determine if an employee exists in the repository
	/// </summary>
	/// <param name="employeeId">The id of the employee to find</param>
	/// <returns></returns>
	private bool EmployeeExists(int employeeId)
	{
		return _repository.FindEmployee(employeeId);
	}
}