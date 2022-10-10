using CRISP.BackendChallenge.Data.Context.Models;
using CRISP.BackendChallenge.Data.Models;
using CRISP.BackendChallenge.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRISP.BackendChallenge.Data.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Login> _loginRepository;

    public EmployeeController(ILogger<EmployeeController> logger, IRepository<Employee> empRepository, IRepository<Login> loginRepository)
    {
        if (empRepository == null)
        {
            throw new ArgumentNullException(nameof(empRepository));
        }

        if (loginRepository == null)
        {
            throw new ArgumentNullException(nameof(loginRepository));
        }

        _logger = logger;
        _employeeRepository = empRepository;
        _loginRepository = loginRepository;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));
        var employee = _employeeRepository.GetById(id);

        if (employee == null) return BadRequest();

        var result = new EmployeeResponse(employee).PopulateLoginData(_loginRepository);

        return Ok(result);
    }

    [HttpGet("search")]
    public IActionResult Search(int? id, string? name, Department? department)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));

        // The biggest problem with the Generic Repository Anti-Pattern.  Unable to join the data, have to do it as a second call.  :s
        var employees = (
            from e
            in _employeeRepository.Query()
            where
                (id == null || e.Id == id) &&
                (name == null || e.Name == name) &&
                (department == null || e.Department == department)
            select new EmployeeResponse(e).PopulateLoginData(_loginRepository)
        );

        return Ok(employees);
    }

    [HttpPost()]
    public IActionResult Create(Employee newEmployee)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Create));

        // TODO: Task [n] => Does PM want to be able to allow a new employee to have faked login data?
        // If not we should block in the repository if nobody should be allowed, here only if we disallow only web and wish to allow
        // other users of the repository.
        _employeeRepository.Add(newEmployee);
        _employeeRepository.Save();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Delete));

        var employee = _employeeRepository.GetById(id);

        if (employee == null) return BadRequest();

        _employeeRepository.Delete(employee);
        _employeeRepository.Save();

        return Ok();
    }

    [HttpPut()]
    public IActionResult Update(Employee employeeData)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(Update));

        // TODO: Task [n] => Do we want ever want to lose login history?  Seems like we should pull in the existing record.  If not we should update repository.
        _employeeRepository.Update(employeeData);
        _employeeRepository.Save();

        return Ok();
    }
}