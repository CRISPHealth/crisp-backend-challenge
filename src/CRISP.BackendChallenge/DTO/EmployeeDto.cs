using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.DTO
{
    public class EmployeeDto
    {
        /// <summary>
        /// Id of the employee
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the employee
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The department of the employee
        /// </summary>
        public Department Department { get; set; }
    }
}