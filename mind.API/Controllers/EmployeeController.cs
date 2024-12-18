using System.Net;
using Microsoft.AspNetCore.Mvc;
using mind.Core.Interfaces.IServices;
using mind.Core.Models;
using mind.Core.Models.DbModels;

namespace mind.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    protected BaseApiResponse _response;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
        _response = new BaseApiResponse(); 
    }

    [HttpGet(Name = "GetEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        if (employees == null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.Result = "No departments found";
            return NotFound(_response);
        }
        _response.Result = employees;
        return Ok(_response);
    }

    [HttpGet("{id}", Name = "GetEmployeeById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.Result = "No employee found";
            return NotFound(_response);
        }
        _response.Result = employee;
        return Ok(_response);
    }

    [HttpPost(Name = "CreateEmployee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> CreateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Result = "Invalid employee data";
            return BadRequest(_response);
        }

        var newEmployee = await _employeeService.CreateEmployee(employee);
        if (newEmployee == null)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Result = "Failed to add employee";
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
        _response.Result = newEmployee;
        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, _response);
    }

    [HttpPut(Name = "UpdateEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> UpdateEmployee([FromBody] Employee employee)
    {
        if (employee == null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Result = "Invalid employee data";
            return BadRequest(_response);
        }

        var updatedEmployee = await _employeeService.UpdateEmployee(employee);
        if (updatedEmployee == null)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Result = "Failed to update employee";
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
        _response.Result = updatedEmployee;
        return Ok(_response);
    }

    [HttpDelete("{id}", Name = "DeleteEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> DeleteEmployee(int id)
    {
        var result = await _employeeService.DeleteEmployee(id);
        if (!result)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.Result = "Employee not found";
            return NotFound(_response);
        }
        _response.Result = "Employee deleted";
        return Ok(_response);
    }
}
