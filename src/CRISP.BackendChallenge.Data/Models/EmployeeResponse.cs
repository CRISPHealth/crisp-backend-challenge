using CRISP.BackendChallenge.Data.Context.Models;
using CRISP.BackendChallenge.Data.Repository;

namespace CRISP.BackendChallenge.Data.Models;

/// <summary>
/// Response for Employee API
/// </summary>
public class EmployeeResponse
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Dates of the login for the person
    /// </summary>
    public List<DateTime>? LoginDates { get; set; }

    public EmployeeResponse()
    {

    }

    public EmployeeResponse(Employee employee)
    {
        Id = employee.Id;
        Name = employee.Name;
        LoginDates = (from e in employee.Logins select e.LoginDate).ToList();
    }

    // Pulled this out to have a single point for populating the login data.  Otherwise we'd have a mess of places we'd need to update if it changed.
    public EmployeeResponse PopulateLoginData(IRepository<Login> loginRepository)
    {
        LoginDates = (
            from l
            in loginRepository.Query()
            where l.EmployeeId == Id
            orderby l.LoginDate descending
            select l.LoginDate
        ).ToList();

        return this;
    }
}