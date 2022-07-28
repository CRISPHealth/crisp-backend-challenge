using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.DTO;
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
            .ToList().Select(x => EmployeeResponse.Convert(x));
        return Ok(result);
    }
   
    [HttpGet]
    [Route("[action]/{id}")]
    public IActionResult Get([FromRoute]int id)
    {        
        _logger.LogDebug(":: Performing {MethodName}", nameof(Get));
        var result = _repository.GetById(id);
        return Ok(EmployeeResponse.Convert(result));
    }

    
    [HttpDelete]
    [Route("[action]/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Get));
        var result = _repository.GetById(id);
        
        if(result == null)        
            return NotFound();        

        _repository.Delete(result);
        
        return Ok();
    }

    
    [HttpPost]
    [Route("[action]/{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] EmployeeRequest employeeRequest)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Get));
        
        var result = _repository.GetById(id);

        if (result == null)
            return NotFound();

        result.Name = employeeRequest.Name;

        _repository.Update(result);

        return Ok();
    }

    // TODO: Implement Update by name
    /*[HttpPost]
    [Route("[action]/{NAME}")]
    public IActionResult Update([FromRoute] string name, [FromBody] EmployeeRequest employeeRequest)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Get));

        var result = _repository.GetById(name);

        if (result == null)
            return NotFound();

        result.Id = employeeRequest.Id;

        _repository.Update(result);

        return Ok();
    }*/
}