using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.Models;

/// <summary>
/// Response for Employee API
/// </summary>
public class EmployeeInput
{
    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Deparment of the person
    /// </summary>
    public Department? Department { get; set; }

    /// <summary>
    /// Start Date of the person
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End Date of the person
    /// </summary>
    public DateTime? EndDate { get; set; }
}
