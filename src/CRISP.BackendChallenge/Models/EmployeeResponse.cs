using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.Models;

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
    /// Deparment of the person
    /// </summary>
    public Department Department { get; set; }

    /// <summary>
    /// Dates of the login for the person
    /// </summary>
    public List<DateTime>? LoginDates { get; set; }
}