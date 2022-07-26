using CRISP.Backend.Challenge.Context;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.DTO;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetAll));

        var result = _repository.Query<Employee>()
            .ToList().Select(x => new PersonResponse
            {
                Id = x.Id,
                Name = x.Name,
                // TODO: Include the login date information...
                LoginDates = null
            });

        return Ok(result);
    }

    [HttpGet, Route("/{id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogDebug(":: Performing {MethodName}", nameof(GetById));

        var result = _repository.GetById<Employee>(id);

        if (result == null)
        {
            return BadRequest("Employee not found.");
        }

        return Ok(result);
    }

    [HttpPost, Route("delete/{id:int}")]
    public IActionResult DeleteEmployee(int id)
    {
        try
        {
            var employee = _repository.GetById<Employee>(id);

            _repository.Delete(employee);
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    [Route("UpdateById")]
    public IActionResult UpdateById([FromBody] Employee model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _repository.Update(model);

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        return Ok();
    }

    [HttpPost]
    [Route("UpdateByName")]
    public IActionResult UpdateByName([FromBody] Employee model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _repository.Update(model);

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }

                return BadRequest();
            }
        }

        return Ok();
    }
}