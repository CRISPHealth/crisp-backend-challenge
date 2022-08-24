

using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Controllers;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CRISP.BackendChallenge.Test;

public class EmployeeControllerTests
{
    private readonly Mock<IRepository<Employee>> _mockRepo = new Mock<IRepository<Employee>>();
    private readonly Mock<ILogger<EmployeeController>> _mockLogger = new Mock<ILogger<EmployeeController>>();

    [Fact]
    public void GetAll_ReturnsAllEmployees()
    {
        // Arrange
        _mockRepo.Setup(m => m.Query()).Returns(TestData.Employees);

        var controller = new EmployeeController(_mockLogger.Object, _mockRepo.Object);

        // Act
        var result = controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var employees = Assert.IsAssignableFrom<IEnumerable<EmployeeResponse>>(okResult.Value);
        Assert.Equal(3, employees.Count());
    }

    [Fact]
    public void GetById_ReturnsOneEmployee()
    {
        // Arrange
        _mockRepo.Setup(m => m.GetById(TestData.Employee1.Id)).Returns(TestData.Employee1);

        var controller = new EmployeeController(_mockLogger.Object, _mockRepo.Object);

        // Act
        var result = controller.GetById(TestData.Employee1.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var employee = Assert.IsType<EmployeeResponse>(okResult.Value);
        Assert.Equal(TestData.Employee1.Id, employee.Id);
        Assert.Equal(TestData.Employee1.Name, employee.Name);
        Assert.Equal(TestData.Employee1.Department, employee.Department);
    }

    [Fact]
    public void GetById_EmployeeNotFound()
    {
        // Arrange
        _mockRepo.Setup(m => m.GetById(TestData.Employee1.Id)).Returns(TestData.Employee1);

        var controller = new EmployeeController(_mockLogger.Object, _mockRepo.Object);

        // Act
        var result = controller.GetById(TestData.Employee2.Id);

        // Assert
        var badResult = Assert.IsType<NotFoundResult>(result);
        Assert.True(badResult.StatusCode == 404);
    }

    [Fact]
    public void Update_EmployeeUpdated()
    {
        // Arrange
        _mockRepo.Setup(m => m.GetById(TestData.Employee1.Id)).Returns(TestData.Employee1);

        var controller = new EmployeeController(_mockLogger.Object, _mockRepo.Object);

        var now = DateTime.Now;

        var updatedEmployee = new EmployeeInput
        {
            Name = "employee 1 updated",
            Department = Department.Management,
            StartDate = now,
        };

        // Act
        var result = controller.Update(updatedEmployee, TestData.Employee1.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var employee = Assert.IsType<EmployeeResponse>(okResult.Value);
        _mockRepo.Verify(m => m.Update(It.IsAny<Employee>()));
        _mockRepo.Verify(m => m.Save());
        Assert.Equal(TestData.Employee1.Id, employee.Id);
        Assert.Equal(updatedEmployee.Name, employee.Name);
        Assert.Equal(updatedEmployee.Department, employee.Department);
    }
}
