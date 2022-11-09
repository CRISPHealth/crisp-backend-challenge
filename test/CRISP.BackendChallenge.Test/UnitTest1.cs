using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Repository;
using FluentAssertions;
using Xunit;

namespace CRISP.BackendChallenge.Test;

public class Tests
{
    ContextRepository<Employee> EmployeeDB;

    public Tests()
    {
        EmployeeDB = new ContextRepository<Employee>(new Context.ApplicationDbContext());
    }


    [Fact]
    public void GetById()
    {
        int Id = 1;

        Employee CurrentEmployee = EmployeeDB.GetById(Id);

        (CurrentEmployee.Id).Should().Be(Id);
    }
    [Fact]
    public void Create()
    {
        Employee NewEmployee = new Employee()
        {
            Id = 10000,
            Department = Department.Engineering,
            Name = "New Employee"
        };

        EmployeeDB.Add(NewEmployee);

        Employee CurrentEmployee = EmployeeDB.GetById(NewEmployee.Id);

        (NewEmployee.Id).Should().Be(CurrentEmployee.Id);
    }
    [Fact]
    public void Update()
    {
        Employee PreviousEmployee = EmployeeDB.GetById(1);
        PreviousEmployee.Name = "Person with a new name";

        EmployeeDB.Update(PreviousEmployee);

        Employee CurrentEmployee = EmployeeDB.GetById(PreviousEmployee.Id);

        (PreviousEmployee.Name).Should().Be(CurrentEmployee.Name);
    }
    [Fact]
    public void Delete()
    {
        Employee PreviousEmployee = EmployeeDB.GetById(1);

        EmployeeDB.Delete(PreviousEmployee);

        Employee CurrentEmployee = EmployeeDB.GetById(PreviousEmployee.Id);

        (CurrentEmployee == null).Should().Be(true);
    }
}