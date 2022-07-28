using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.DTO;

namespace CRISP.BackendChallenge.Context.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDto ConvertToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name
            };
        }
    }
}