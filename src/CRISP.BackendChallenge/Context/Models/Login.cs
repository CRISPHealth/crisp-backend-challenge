namespace CRISP.BackendChallenge.Context.Models;

/// <summary>
/// Login Entity Model
/// </summary>
public class Login : BaseEntity
{
    /// <summary>
    /// Id of <see cref="Employee"/> entity associated with the Login
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Login Date
    /// </summary>
    public DateTime LoginDate { get; set; }
}