using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Controllers;
using CRISP.BackendChallenge.Repository;
using CRISP.BackendChallenge.Test.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CRISP.BackendChallenge.Test
{
    public class TestEmployeeController
    {
        public TestEmployeeController()
        {
        }

        [Fact]
        public void GetAllReturnsExpectedData()
        {
            // act
            var repo = new Mock<IRepository>();
            var sut = new EmployeeController(
                NullLogger<EmployeeController>.Instance, repo.Object);
            var okResult = sut.GetAll();


            // assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAllShouldReturn200Status()
        {
            /// Arrange
            var repo = new Mock<IRepository>();
            repo.Setup(_ => _.GetAll<Employee>()).Returns(EmployeeMockData.GetAllEmployees());

            var sut = new EmployeeController(
                NullLogger<EmployeeController>.Instance, repo.Object);

            /// Act
            var result = (OkObjectResult) sut.GetAll();


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetAllShouldReturn204NoContentStatus()
        {
            /// Arrange
            var repo = new Mock<IRepository>();
            repo.Setup(_ => _.GetAll<Employee>()).Returns(EmployeeMockData.GetEmptyEmployeeList());
            var sut = new EmployeeController(
                NullLogger<EmployeeController>.Instance, repo.Object);

            /// Act
            var result = (NoContentResult) sut.GetAll();

            /// Assert
            result.StatusCode.Should().Be(204);
        }
    }
}