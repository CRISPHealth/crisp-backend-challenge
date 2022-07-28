using CRISP.Backend.Challenge.Context.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CRISP.BackendChallenge.Context.Models;

/// <summary>
/// Employee Entity Model
/// </summary>
public class Employee
{
	public Employee()
	{
		// ReSharper disable once VirtualMemberCallInConstructor
		Logins = new HashSet<Login>();
	}

	/// <summary>
	/// Id of the employee
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Name of the employee
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The department of the employee
	/// </summary>
	public Department Department { get; set; }

	/// <summary>
	/// Navigation property to the logins of the employee
	/// </summary>
	public virtual ICollection<Login> Logins { get; set; }
}