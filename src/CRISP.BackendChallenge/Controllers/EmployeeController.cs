using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet("/all")]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query()
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                LoginDates = x.Logins.OrderByDescending(l => l.LoginDate).Select(l => l.LoginDate).ToList()
            })
            .ToList();
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));

        var result = new EmployeeResponse();
        try
        {
            var employee = _repository.Query()
                .Where(x => x.Id == id)
                .First();
            result = ToEmployeeResponse(employee);
        }
        catch (Exception e)
        {
            _logger.LogError("Unable to get employee with ID = " + id, e.Message);
            throw;
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));

        var result = new EmployeeResponse();      
        try
        { 
            if (!IsValidEmployee(employee))
            {
                throw new Exception();
            }
            _repository.Add(employee);
            result = ToEmployeeResponse(employee);
        } 
        catch (Exception e)
        {
            _logger.LogError("Unable to create employee", e.Message);            
            throw;
        }
       

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));

        var result = new EmployeeResponse();
        try
        {
            if (!IsValidEmployee(employee))
            {
                throw new Exception();
            }
            _repository.Update(employee);
            result = ToEmployeeResponse(employee);
        }
        catch (Exception e)
        {
            _logger.LogError("Unable to update employee with ID = " + employee.Id, e.Message);
            throw;
        }

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete(Employee employee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Delete));

        var result = new EmployeeResponse();
        try
        {
            _repository.Delete(employee);
            result = ToEmployeeResponse(employee);
        }
        catch (Exception e)
        {
            _logger.LogError("Unable to delete employee with ID = " + employee.Id, e.Message);
            throw;
        }

        return Ok();
    }

    [HttpGet("/search")]
    public IActionResult Search(int? id, string? name, Department? department )
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Search));
        var result = new List<EmployeeResponse>();

        try
        {            
            result = _repository.Query()
                .Where(x => (id.HasValue && x.Id == id) || (!string.IsNullOrEmpty(name) && x.Name == name) || (department.HasValue && x.Department == department))
                .Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    LoginDates = x.Logins.OrderByDescending(l => l.LoginDate).Select(e => e.LoginDate).ToList()
                })
                .ToList();           
        }
        catch(Exception e)
        {
            _logger.LogError("Unable to search employee", e.Message);
            throw;
        }
      

        return Ok(result);
    }

    // Ideally these functions would be in in another class

    private bool IsValidEmployee(Employee employee)
    {     
        if (!string.IsNullOrEmpty(employee.Name) && !employee.Department.Equals(null))
        {
            return true;    
        }
        return false;
    }

    private EmployeeResponse ToEmployeeResponse(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            LoginDates = employee.Logins.OrderByDescending(l => l.LoginDate).Select(l => l.LoginDate).ToList()
        };
    }
}