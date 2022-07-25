namespace CRISP.Backend.Challenge.Context.Models;

/// <summary>
/// Login Entity Model
/// </summary>
public class Login
{
    /// <summary>
    /// Id of the Login
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of <see cref="Person"/> entity associated with the Login
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Login Date
    /// </summary>
    public DateTime LoginDate { get; set; }
}