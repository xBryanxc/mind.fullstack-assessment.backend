using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using mind.API.Controllers;
using mind.Core.Interfaces.IServices;
using mind.Core.Models;
using mind.Core.Models.DbModels;
using mind.Core.Models.DTOs;
using System.Net;

namespace mind.UnitTest.Controllers;

public class EmployeeControllerTests
{
    private readonly IEmployeeService _employeeService;
    private readonly EmployeeController _controller;

    public EmployeeControllerTests()
    {
        _employeeService = A.Fake<IEmployeeService>();
        _controller = new EmployeeController(_employeeService);
    }

    [Fact]
    public async Task GetEmployees_ReturnsOkResult_WhenEmployeesExist()
    {
        // Arrange
        var fakeEmployees = A.CollectionOfDummy<Employee>(3).ToList();
        A.CallTo(() => _employeeService.GetAllEmployees()).Returns(fakeEmployees);

        // Act
        var result = await _controller.GetEmployees();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        var response = okResult?.Value as BaseApiResponse;
        response?.Result.Should().BeEquivalentTo(fakeEmployees);
    }

    [Fact]
    public async Task GetEmployees_ReturnsNotFound_WhenNoEmployeesExist()
    {
        // Arrange
        A.CallTo(() => _employeeService.GetAllEmployees()).Returns(new List<Employee>());

        // Act
        var result = await _controller.GetEmployees();

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = result.Result as NotFoundObjectResult;
        var response = notFoundResult?.Value as BaseApiResponse;
        response?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsOkResult_WhenEmployeeExists()
    {
        // Arrange
        var fakeEmployee = A.Dummy<Employee>();
        A.CallTo(() => _employeeService.GetEmployeeById(A<int>._)).Returns(fakeEmployee);

        // Act
        var result = await _controller.GetEmployeeById(1);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        var response = okResult?.Value as BaseApiResponse;
        response?.Result.Should().BeEquivalentTo(fakeEmployee);
    }

    [Fact]
    public async Task CreateEmployee_ReturnsCreatedAtAction_WhenValidInput()
    {
        // Arrange
        var employeeDto = A.Dummy<EmployeeDto>();
        var createdEmployee = A.Dummy<Employee>();
        A.CallTo(() => _employeeService.CreateEmployee(employeeDto)).Returns(createdEmployee);

        // Act
        var result = await _controller.CreateEmployee(employeeDto);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        var response = createdResult?.Value as BaseApiResponse;
        response?.Result.Should().BeEquivalentTo(createdEmployee);
    }

    [Fact]
    public async Task CreateEmployee_ReturnsBadRequest_WhenInputIsNull()
    {
        // Arrange
        EmployeeDto? employeeDto = null;

        // Act
        var result = await _controller.CreateEmployee(employeeDto!);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        var response = badRequestResult?.Value as BaseApiResponse;
        response?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsOkResult_WhenValidInput()
    {
        // Arrange
        var updateDto = A.Dummy<UpdateEmployeeDto>();
        var updatedEmployee = A.Dummy<Employee>();
        A.CallTo(() => _employeeService.UpdateEmployee(updateDto)).Returns(updatedEmployee);

        // Act
        var result = await _controller.UpdateEmployee(updateDto);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        var response = okResult?.Value as BaseApiResponse;
        response?.Result.Should().BeEquivalentTo(updatedEmployee);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        A.CallTo(() => _employeeService.DeleteEmployee(A<int>._)).Returns(true);

        // Act
        var result = await _controller.DeleteEmployee(1);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        var response = okResult?.Value as BaseApiResponse;
        response?.Result.Should().Be("Employee deleted");
    }
} 