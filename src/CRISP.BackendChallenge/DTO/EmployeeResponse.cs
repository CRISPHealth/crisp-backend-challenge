namespace CRISP.BackendChallenge.DTO;
using CRISP.BackendChallenge.Context.Models;

/// <summary>
/// Response for Person API
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

    public static EmployeeResponse Convert(Employee e)
    {
        return e != null ? new EmployeeResponse
        {
            Name = e.Name,
            Id = e.Id,
            LoginDates = e.Logins?.Where(l => l.PersonId == e.Id)?.Select(l => l.LoginDate)?.ToList()
        } : new EmployeeResponse();
    }
}