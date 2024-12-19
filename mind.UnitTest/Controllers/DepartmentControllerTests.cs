using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using mind.API.Controllers;
using mind.Core.Interfaces.IServices;
using mind.Core.Models;
using mind.Core.Models.DbModels;
using System.Net;

namespace mind.UnitTest.Controllers;

public class DepartmentControllerTests
{
    private readonly IDepartmentService _departmentService;
    private readonly DepartmentController _controller;

    public DepartmentControllerTests()
    {
        _departmentService = A.Fake<IDepartmentService>();
        _controller = new DepartmentController(_departmentService);
    }

    [Fact]
    public async Task GetDepartments_ReturnsOkResult_WhenDepartmentsExist()
    {
        // Arrange
        var fakeDepartments = A.CollectionOfDummy<Department>(3).ToList();
        A.CallTo(() => _departmentService.GetAllDepartments()).Returns(fakeDepartments);

        // Act
        var result = await _controller.GetDepartments();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        var response = okResult?.Value as BaseApiResponse;
        response?.Result.Should().BeEquivalentTo(fakeDepartments);
    }

    [Fact]
    public async Task GetDepartments_ReturnsNotFound_WhenNoDepartmentsExist()
    {
        // Arrange
        A.CallTo(() => _departmentService.GetAllDepartments()).Returns(new List<Department>());

        // Act
        var result = await _controller.GetDepartments();

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = result.Result as NotFoundObjectResult;
        var response = notFoundResult?.Value as BaseApiResponse;
        response?.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response?.Result.Should().Be("No departments found");
    }
} 