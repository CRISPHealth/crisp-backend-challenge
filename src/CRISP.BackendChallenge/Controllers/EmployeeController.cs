using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query().Include(x => x.Logins)
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = x.Logins.Select(x => x.LoginDate).OrderByDescending(x => x).ToList()
            }).ToList();
        return Ok(result);
    }
    // TODO: Implement Search By Id
    [HttpGet("{id}")]
    public ActionResult<EmployeeResponse> Get(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Get));
        var employee = _repository.Query().Where(x => x.Id == id).Include(x => x.Logins).SingleOrDefault();

        if (employee == null)
        {
            return NotFound(id);
        }

        return new EmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            // TODO: Include the login date information...
            LoginDates = employee.Logins.Select(x => x.LoginDate).OrderByDescending(x => x).ToList()
        };
    }

    // TODO: Implement Create 
    [HttpPost]
    public ActionResult<bool> Create(Employee e)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));
        if (e == null)
        {
            return BadRequest();
        }

        var employee = _repository.GetById(e.Id);
        if (employee != null)
        {
            return BadRequest();
        }

        _repository.Add(e);
        return true;
    }

    // TODO: Implement Update by Id
    [HttpPut("{id}")]
    public IActionResult Update(int id, Employee e)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));
        if (e == null)
        {
            return BadRequest();
        }

        _repository.Update(e);

        return NoContent();
    }

    // TODO: Implement Delete by Id
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _logger.LogDebug(":: Performing {MethodName}", nameof(Delete));
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _repository.Delete(employee);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // TODO: Implement Search By Id
    [HttpGet("SearchById/{id}")]
    public IActionResult SearchById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(SearchById));

        var result = _repository.Query().Where(x => x.Id == id).Include(x => x.Logins)
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = x.Logins.Select(x => x.LoginDate).OrderByDescending(x => x).ToList()
            }).ToList();
        return Ok(result);
    }

    // TODO: Implement Search By Name
    [HttpGet("SearchByName/{name}")]
    public IActionResult SearchByName(string name)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(SearchByName));
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest();
        }

        var result = _repository.Query().Where(x => x.Name.Contains(name)).Include(x => x.Logins)
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = x.Logins.Select(x => x.LoginDate).OrderByDescending(x => x).ToList()
            }).ToList();
        return Ok(result);
    }

    // TODO: Implement Search By Name
    [HttpGet("SearchByDepartment/{department}")]
    public IActionResult SearchByDepartment(int department)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(SearchByDepartment));

        var result = _repository.Query().Where(x => (int)x.Department == department).Include(x => x.Logins)
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = x.Logins.Select(x => x.LoginDate).OrderByDescending(x => x).ToList()
            }).ToList();
        return Ok(result);
    }

}