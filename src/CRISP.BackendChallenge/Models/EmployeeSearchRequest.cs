using CRISP.BackendChallenge.Context.Models;

namespace CRISP.BackendChallenge.Models
{
    public class EmployeeSearchRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Department? Department { get; set; }
    }
}
