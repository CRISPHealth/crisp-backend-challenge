using CRISP.BackendChallenge.Data.Context.Models;
using CRISP.BackendChallenge.Data.Controllers;
using CRISP.BackendChallenge.Data.Models;
using CRISP.BackendChallenge.Data.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CRISP.BackendChallenge.Test;

public class EmployeeControllerTests
{
    EmployeeController controller;
    Mock<IRepository<Employee>> EmployeeRepoMock;
    Mock<IRepository<Login>> LoginRepoMock;
    List<Login> loginDataForMock;

    public EmployeeControllerTests()
    {
        EmployeeRepoMock = new Mock<IRepository<Employee>>(MockBehavior.Loose);
        LoginRepoMock = new Mock<IRepository<Login>>(MockBehavior.Loose);
        loginDataForMock = new List<Login>();

        controller = new EmployeeController(NullLogger<EmployeeController>.Instance, EmployeeRepoMock.Object, LoginRepoMock.Object);
        LoginRepoMock.Setup(m => m.Query()).Returns(loginDataForMock.AsQueryable());
    } 

    [Fact]
    public void GetById_ReturnsExpectedRecordIfItExists()
    {
        var expected = new Employee { Id = 2, Name = "2" };

        EmployeeRepoMock.Setup(m => m.GetById(2)).Returns(expected);

        var result = controller.GetById(2) as OkObjectResult;

        result.Should().NotBeNull();

        var actual = result.Value as EmployeeResponse;

        verifyResultsAreExpected(new[] { expected }, new[] { actual } );
    }

    [Fact]
    public void GetById_ReturnsBadRequestIfItDoesntExist()
    {
        _ = EmployeeRepoMock.Setup(m => m.GetById(2)).Returns((Employee)null);

        var result = controller.GetById(2) as BadRequestResult;

        result.Should().NotBeNull();
    }

    [Fact]
    public void GetById_VerifyLoginDatesAreOrderedCorrectly()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void GetById_ProperErrorReturnedIfDbConnectionFails()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Search_ReturnsAllIfNoFilters()
    {
        var expected = new Employee[]
        {
            new Employee { Id = 1, Name = "1", Logins = getMockedLogins(1, 3) },
            new Employee { Id = 2, Name = "2", Logins = getMockedLogins(2, 1) },
            new Employee { Id = 3, Name = "3", Logins = getMockedLogins(3, 5) }
        };

        EmployeeRepoMock.Setup(m => m.Query()).Returns(expected.AsQueryable());

        var result = controller.Search(null, null, null) as OkObjectResult;

        result.Should().NotBeNull();
        var actual = result.Value as EnumerableQuery<EmployeeResponse>;

        verifyResultsAreExpected(expected, (from er in actual select er).ToArray());
    }

    [Fact]
    public void Search_VerifyLoginDatesAreOrderedCorrectly()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Search_ProperErrorReturnedIfDbConnectionFails()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Create_CanCreateEmployeeRecord()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Create_CanCreateNewEmployeeRecordWithExistingId()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Create_DoAllKindsOfTestsForValidationOfCreation()
    {
        // Can be quite a few tests here.  Just creating a placeholder.
        throw new NotImplementedException();
    }

    [Fact]
    public void Update_CanUpdateToPreviousValues()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Update_CanChangeName()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Update_VerifyBusinessRules()
    {
        // Do we allow blank names?  Adding login data?  All these answers should be known and tested.
        // Create a until test for each rule.
        throw new NotImplementedException();
    }

    [Fact]
    public void Delete_CanDeleteExistingEmployee()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Delete_HandleDeleteNonExisting()
    {
        // How do we handle this case?  Allow like all is good?  Throw a bad request?  Prefer the latter.
        throw new NotImplementedException();
    }

    private ICollection<Login> getMockedLogins(int employeeId, int count)
    {
        var result = new List<Login>();

        for(var i = 0; i < count; i++)
        {
            var login = new Login() { EmployeeId = employeeId, LoginDate = DateTime.Now.AddDays(-i) };
            result.Add(login);
            loginDataForMock.Add(login);
        }

        return result;
    }

    private void verifyResultsAreExpected(Employee[] expected, EmployeeResponse[] actual)
    {
        for(var i = 0; i < expected.Length; i++)
        {
            Assert.True(expected[i].Name == actual[i].Name);
            Assert.True(expected[i].Id == actual[i].Id);

            var expectedLoginDates = from l in expected[i].Logins select l.LoginDate;

            Assert.True(Enumerable.SequenceEqual(expectedLoginDates, actual[i].LoginDates));
        }
    }
}