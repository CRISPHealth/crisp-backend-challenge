

using CRISP.BackendChallenge.Context;
using CRISP.BackendChallenge.Context.Models;
using CRISP.BackendChallenge.Controllers;
using CRISP.BackendChallenge.Models;
using CRISP.BackendChallenge.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Data.Common;
using Xunit;

namespace CRISP.BackendChallenge.Test;

public class Tests
{
    DbConnection _connection;
    DbContextOptions _contextOptions;
    ApplicationDbContext _dbContext;
    IRepository<Employee> _repository;
    ILogger<EmployeeController> _logger;
    private const string name = "nishantb";
    public Tests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;
        
        _dbContext = new ApplicationDbContext();
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        
        _repository = new ContextRepository<Employee>(_dbContext);
        _logger = new Mock<ILogger<EmployeeController>>().Object;
    }

    [Fact]
    public void Test1()
    {
        true.Should().Be(true);
    }

    [Fact]
    public void TestGetAll()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        var results = _empController.GetAll();
        var okObjectResult = results as OkObjectResult;
        Assert.NotNull(okObjectResult);
        List<EmployeeResponse> records = okObjectResult.Value as List<EmployeeResponse>;
        Assert.NotNull(records);
        Assert.Equal(4, records.Count);
    }

    [Fact]
    public void TestSearch()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        var result = _empController.Search(new EmployeeSearchRequest() { Id = 1 });
        var okObjectResult = result as OkObjectResult;

        Assert.NotNull(okObjectResult);
        List<EmployeeResponse>? records = okObjectResult.Value as List<EmployeeResponse>;
        Assert.NotNull(records);
        Assert.Equal(1, records[0].Id);
    }

    [Fact]
    public void TestDelete()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        _empController.DeleteById(1);
        var results = _empController.GetAll();
        var okObjectResult = results as OkObjectResult;
        Assert.NotNull(okObjectResult);
        List<EmployeeResponse> records = okObjectResult.Value as List<EmployeeResponse>;
        Assert.NotNull(records);
        Assert.Equal(3, records.Count);
    }

    [Fact]
    public void TestDeleteIfDoesntExist()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        _empController.DeleteById(100);
        var results = _empController.GetAll();
        var okObjectResult = results as NotFoundResult;
        Assert.Null(okObjectResult);
    }

    [Fact]
    public void TestUpdate()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        _empController.Update(2, new Employee() { Id = 2, Name = "nishantb", Department = Department.Engineering });
        var results = _empController.Search(new EmployeeSearchRequest() { Id = 2 });
        var okObjectResult = results as OkObjectResult;
        Assert.NotNull(okObjectResult);

        List<EmployeeResponse> records = okObjectResult.Value as List<EmployeeResponse>;
        Assert.NotNull(records);
        Assert.Equal(1, records.Count);
        Assert.Equal(name, records[0].Name);
    }

    [Fact]
    public void TestCreate()
    {
        EmployeeController _empController = new EmployeeController(_logger, _repository);
        _empController.Should().NotBeNull();
        var results = _empController.Create(new Employee() { Id = 5, Name = "nishantb", Department = Department.Engineering, Logins = new List<Login>() });
        var createdresult = results as CreatedResult;
        Assert.NotNull(createdresult);

        Employee records = createdresult.Value as Employee;
        Assert.NotNull(records);
        //Assert.Equal(records.Count, 1);
        Assert.Equal(name, records.Name);
    }
}