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

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query()
            .Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                LoginDates = x.Logins.Select(x => x.LoginDate).ToList()
            }).ToList();
        return Ok(result);
    }

    //Implement Search By Id/Name/Department
    [HttpPost("Search")]
    public IActionResult Search([FromBody] EmployeeSearchRequest emp)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Search));
        List<EmployeeResponse> result = _repository.Query().
                                                                Where(x =>(x.Id == emp.Id || emp.Id == null) &&
                                                                           (x.Department == emp.Department || emp.Department == null) &&
                                                                           (emp.Name == null || x.Name.ToLower() == emp.Name.ToLower())).
                                                                Select(x => new EmployeeResponse
                                                                {
                                                                    Id = x.Id,
                                                                    Name = x.Name,
                                                                    LoginDates = x.Logins.Select(x => x.LoginDate).ToList()
                                                                }).ToList();

        if (result == null)
            return NotFound();
        return Ok(result);
    }

    //Implement Delete by Id
    [HttpDelete]
    public IActionResult DeleteById(int empid)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(DeleteById));
        Employee emp = _repository.GetById(empid);
        if (emp != null)
        {
            _repository.Delete(emp);
            _repository.Save();
        }
        else
            return NotFound();
        return Ok();
    }

    // Implement Update by Id
    [HttpPut]
    public IActionResult Update(int Id, [FromBody] Employee emp)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));
        Employee result = _repository.GetById(Id);
        if (result != null)
        {
            result.Id = emp.Id;
            result.Name = emp.Name;
            result.Logins = emp.Logins;
            result.StartDate = emp.StartDate;
            result.EndDate = emp.EndDate;
            result.Department = emp.Department;
            _repository.Save();
        }
        return Ok();
    }

    //Implement Create Employee Record
    [HttpPost]
    public IActionResult Create([FromBody] Employee emp)
    {

        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));
        try
        {
            _repository.Add(emp);
            _repository.Save();
            return Created("", emp);
        }
        catch (Exception ex)
        {
            string guid = Guid.NewGuid().ToString();
            _logger.LogError("errorid :{guid}, message:{message}", guid, ex.Message);
            return new ObjectResult(new { error = $"Unable to create a new record.  ErrorId:{guid} " }) { StatusCode = 500 };
        }
    }
}