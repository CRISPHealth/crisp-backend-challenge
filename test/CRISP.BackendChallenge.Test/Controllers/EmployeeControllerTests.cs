using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Controllers;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace CRISP.BackendChallenge.Test.Controllers
{
    public class EmployeeControllerTests
    {
        //private MockRepository mockRepository;

        private Mock<ILogger<EmployeeController>> mockLogger;
        private Mock<IRepository<Employee>> mockRepository;

        public EmployeeControllerTests()
        {
            //this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = new Mock<ILogger<EmployeeController>>();
            this.mockRepository = new Mock<IRepository<Employee>>();
        }

        private EmployeeController CreateEmployeeController()
        {
            return new EmployeeController(
                this.mockLogger.Object,
                this.mockRepository.Object);
        }
        private List<Employee> GetTestEmployees()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee()
            {
                Id = 1,
                Name = "John Doe",
                Department = Department.Engineering,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            }
            );
            employees.Add(new Employee()
            {
                Id = 2,
                Name = "Jane Doe",
                Department = Department.Management,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            }
            );
            employees.Add(new Employee()
            {
                Id = 3,
                Name = "Joe Doe",
                Department = Department.Engineering,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            }
            );

            employees.Add(new Employee()
            {
                Id = 4,
                Name = "Leroy Jenkins",
                Department = Department.Engineering,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            }
            );
            return employees;
        }

        [Fact]
        public void GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            mockRepository.Setup(repo => repo.Query())
       .Returns(GetTestEmployees().AsQueryable());
            // Act
            var result = employeeController.GetAll();

            // Assert
            mockLogger.Verify(
        m => m.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("GetAll")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmployeeResponse>>(okResult.Value);
            var e = returnValue.FirstOrDefault();
            Assert.NotNull(e);
            Assert.Equal("John Doe", e.Name);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            
            var employeeController = this.CreateEmployeeController();
            int id = 1;
            mockRepository.Setup(repo => repo.Query())
     .Returns(GetTestEmployees().AsQueryable());
            // Act
            var result = employeeController.Get(
                id);

            // Assert
            mockLogger.Verify(
        m => m.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Get")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
            var actionResult = Assert.IsType<ActionResult<EmployeeResponse>>(result);
            var e = Assert.IsType<EmployeeResponse>(actionResult.Value);
            Assert.NotNull(e);
            Assert.Equal("John Doe", e.Name);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Get_StateUnderTest_ReturnsNotFoundObjectResultForNonexistentId()
        {
            // Arrange
            mockRepository.Setup(repo => repo.Query())
     .Returns(GetTestEmployees().AsQueryable());
            var employeeController = this.CreateEmployeeController();
            int id = 9;

            // Act
            var result = employeeController.Get(
                id);

            // Assert
           
            var actionResult = Assert.IsType<ActionResult<EmployeeResponse>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            Employee e = new Employee()
            {
                Id =12,
                Name = "TestCreate",
                Department = Department.Engineering,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            };
            mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
       .Returns((Employee)null);
            // Act
            var result = employeeController.Create(
                e);

            // Assert
            mockLogger.Verify(
        m => m.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Create")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
            mockRepository.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
            var actionResult = Assert.IsType<ActionResult<bool>>(result);
            Assert.True(actionResult.Value);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int id = 12;
            Employee e = new Employee()
            {
                Id = 12,
                Name = "TestUpdate",
                Department = Department.Engineering,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            };

            // Act
            var result = employeeController.Update(
                id,
                e);

            // Assert
            mockLogger.Verify(
       m => m.Log(
           LogLevel.Debug,
           It.IsAny<EventId>(),
           It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Update")),
           It.IsAny<Exception>(),
           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
       Times.Once);
            mockRepository.Verify(m => m.Update(It.IsAny<Employee>()), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int id = 2;

           var e = new Employee()
            {
                Id = 2,
                Name = "Jane Doe",
                Department = Department.Management,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                EndDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            };
            mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(e);
             // Act
             var result = employeeController.Delete(
                 id);


            // Assert
            mockLogger.Verify(
      m => m.Log(
          LogLevel.Debug,
          It.IsAny<EventId>(),
          It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Delete")),
          It.IsAny<Exception>(),
          It.IsAny<Func<It.IsAnyType, Exception, string>>()),
      Times.Once);
            mockRepository.Verify(m => m.Delete(It.IsAny<Employee>()), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SearchById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int id = 1;
            mockRepository.Setup(repo => repo.Query())
      .Returns(GetTestEmployees().AsQueryable());
            // Act
            var result = employeeController.SearchById(
                id);

            // Assert
            mockLogger.Verify(
       m => m.Log(
           LogLevel.Debug,
           It.IsAny<EventId>(),
           It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("SearchById")),
           It.IsAny<Exception>(),
           It.IsAny<Func<It.IsAnyType, Exception, string>>()),
       Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmployeeResponse>>(okResult.Value);
            var e = returnValue.FirstOrDefault();
            Assert.NotNull(e);
            Assert.Equal("John Doe", e.Name);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SearchByName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            mockRepository.Setup(repo => repo.Query())
       .Returns(GetTestEmployees().AsQueryable());
            string name = "Doe";

            // Act
            var result = employeeController.SearchByName(
                name);

            // Assert
            mockLogger.Verify(
        m => m.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("SearchByName")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmployeeResponse>>(okResult.Value);
            var e = returnValue.FirstOrDefault();
            Assert.NotNull(e);
            Assert.Equal("John Doe", e.Name);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void SearchByDepartment_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var employeeController = this.CreateEmployeeController();
            int department = 1;
            mockRepository.Setup(repo => repo.Query())
       .Returns(GetTestEmployees().AsQueryable());
            // Act
            var result = employeeController.SearchByDepartment(
                department);

            // Assert
            mockLogger.Verify(
        m => m.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("SearchByDepartment")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmployeeResponse>>(okResult.Value);
            var e = returnValue.FirstOrDefault();
            Assert.NotNull(e);
            Assert.Equal("John Doe", e.Name);
            this.mockRepository.VerifyAll();
        }
    }
}
