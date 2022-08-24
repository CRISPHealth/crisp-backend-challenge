using CRISP.BackendChallenge.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRISP.BackendChallenge.Test
{
    public class TestData
    {
        public static Employee Employee1 => new Employee { Id = 1, Name = "employee 1", Department = Department.None };

        public static Employee Employee2 => new Employee { Id = 2, Name = "employee 2", Department = Department.Engineering };

        public static Employee Employee3 => new Employee { Id = 3, Name = "employee 3", Department = Department.Management };

        public static IQueryable<Employee> Employees => new List<Employee> { Employee1, Employee2, Employee3 }.AsQueryable();

        public static IQueryable<Login> Logins => new List<Login>().AsQueryable();
    }
}
