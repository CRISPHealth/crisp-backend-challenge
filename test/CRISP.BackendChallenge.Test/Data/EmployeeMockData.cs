using CRISP.BackendChallenge.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRISP.BackendChallenge.Test.Data
{
    public class EmployeeMockData
    {
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Department = Department.Management
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Doe",
                    Department = Department.Engineering
                },
                new Employee
                {
                    Id = 3,
                    Name = "Jim Doe",
                    Department = Department.None
                },
            };
        }

        public static List<Employee> GetEmptyEmployeeList()
        {
            return new List<Employee>();
        }
    }
}