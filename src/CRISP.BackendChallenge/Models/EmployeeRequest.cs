namespace CRISP.BackendChallenge.Models;

/// <summary>
/// Response for Person API
/// </summary>
public class EmployeeRequest
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string? Name { get; set; }   
}