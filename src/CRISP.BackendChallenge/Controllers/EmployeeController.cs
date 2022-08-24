using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRISP.BackendChallenge.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository<Employee> _repository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository<Employee> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// Get all Employees
    /// </summary>
    /// <returns>All Employees</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query()
            .ToList().Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                Department = x.Department,
                LoginDates = x.Logins.Select(l => l.LoginDate).OrderByDescending(d => d).ToList(),
            });

        return Ok(result);
    }

    /// <summary>
    /// Get an Employee by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A single Employee</returns>
    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));
        var employee = _repository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        var result = new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Department = employee.Department,
            LoginDates = employee.Logins.Select(l => l.LoginDate).OrderByDescending(d => d).ToList(),
        };

        return Ok(result);
    }

    /// <summary>
    /// Delete an Employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status code</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Delete));
        var employee = _repository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        _repository.Delete(employee);
        _repository.Save();

        return Ok();
    }

    /// <summary>
    /// Update an Employee
    /// </summary>
    /// <param name="updatedEmployee"></param>
    /// <param name="id"></param>
    /// <returns>Updated Employee</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update([FromBody] EmployeeInput updatedEmployee, [FromRoute] int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));
        if (updatedEmployee == null)
        {
            return BadRequest($"{nameof(updatedEmployee)} cannot be null.");
        }
        var employee = _repository.GetById(id);

        if (employee == null)
        {
            return NotFound($"Employee id {id} does not exist.");
        }

        employee.Name = updatedEmployee.Name ?? employee.Name;
        employee.Department = updatedEmployee.Department ?? employee.Department;
        employee.StartDate = updatedEmployee.StartDate ?? employee.StartDate;
        employee.EndDate = updatedEmployee.EndDate ?? employee.EndDate;

        _repository.Update(employee);
        _repository.Save();

        return Ok(new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Department = employee.Department,
            LoginDates = employee.Logins.Select(l => l.LoginDate).OrderByDescending(d => d).ToList(),
        });
    }

    /// <summary>
    /// Create an Employee
    /// </summary>
    /// <param name="employee"></param>
    /// <returns>The created Employee</returns>
    [HttpPost]
    public IActionResult Create([FromBody] EmployeeInput employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));
        if (employee == null)
        {
            return BadRequest($"{nameof(employee)} cannot be null.");
        }

        if (employee.Name == null)
        {
            return BadRequest($"{nameof(employee)} must have a name.");
        }

        var newEmployee = new Employee
        {
            Name = employee.Name,
            Department = employee.Department ?? Department.None,
            StartDate = employee.StartDate,
            EndDate = employee.EndDate,
        };
        
        _repository.Add(newEmployee);
        _repository.Save();

        return Ok(new EmployeeResponse
        {
            Id = newEmployee.Id,
            Name = newEmployee.Name,
            Department = newEmployee.Department,
        });
    }
}
