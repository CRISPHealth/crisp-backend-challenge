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

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));
        var result = _repository.Query()
            .ToList().Select(x => new EmployeeResponse
            {
                Id = x.Id,
                Name = x.Name,
                LoginDates = (from d in x.Logins
                              select d.LoginDate).ToList()
            });
        return Ok(result);
    }

    [HttpGet(nameof(Search))]
    public IActionResult Search([FromQuery] int? id, [FromQuery] string? name, [FromQuery] Department? department)
    {
        _logger.LogDebug($":: Performing {nameof(Search)} {nameof(id)}:{id} {nameof(name)}:{name} {nameof(department)}:{department}");

        var result =
            (from r in _repository.Query()
             where
                (!id.HasValue || r.Id == id.Value)
             &&
             (string.IsNullOrWhiteSpace(name) || r.Name == name)
             &&
             (!department.HasValue || r.Department == department.Value)

             select new EmployeeResponse()
             {
                 Id = r.Id,
                 Name = r.Name,
                 LoginDates = (from d in r.Logins
                               select d.LoginDate).ToList()
             }).ToList();

        if (result.Count == 0)
            return NotFound();
        else
            return Ok(result);
    }

    [HttpDelete()]
    public IActionResult Delete([FromQuery] int id)
    {
        _logger.LogDebug($":: Performing {nameof(Delete)} {nameof(id)}:{id}");

        var result = (from r in _repository.Query()
                      where r.Id == id
                      select r).FirstOrDefault();

        if (result == null)
        {
            return NotFound();
        }
        else
        {
            _repository.Delete(result);
            return Ok();
        }
    }

    [HttpPost()]
    public IActionResult Update([FromBody] int Id, [FromQuery] string? name, [FromQuery] Department? department)
    {
        _logger.LogDebug($":: Performing {nameof(Update)} Id:{Id}");

        var result = (from r in _repository.Query()
                      where r.Id == Id
                      select r).FirstOrDefault();

        if (result == null)
        {
            return NotFound();
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(name))
                result.Name = name;

            if (department.HasValue)
                result.Department = department.Value;

            _repository.Update(result);
            return Ok();
        }
    }
}