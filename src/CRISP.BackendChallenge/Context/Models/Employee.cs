using CRISP.Backend.Challenge.Context.Models;

namespace CRISP.BackendChallenge.Context.Models;


/// <summary>
/// Person Entity Model
/// </summary>
public class Employee : BaseEntity
{
    public Employee()
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
    /// Navigation property to the logins of the person
    /// </summary>
    public virtual ICollection<Login> Logins { get; set; }
}