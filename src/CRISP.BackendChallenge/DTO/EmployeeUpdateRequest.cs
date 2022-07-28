using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.DTO
{
    public class EmployeeUpdateRequest
    {
        /// <summary>
        /// Name of the employee
        /// </summary>
        public string? Name { get; set; }
    }
}