namespace CRISP.BackendChallenge.Data.Context.Models;


/// <summary>
/// Person Entity Model
/// </summary>
public class Employee : BaseEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Employee()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Logins = new HashSet<Login>();
    }

    /// <summary>
    /// Name of the person
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The department of the person
    /// </summary>
    public Department Department { get; set; }

    /// <summary>
    /// The date which the employee was hired
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The date in which the employee was terminated
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Navigation property to the logins of the person
    /// </summary>
    public virtual ICollection<Login> Logins { get; set; }
}