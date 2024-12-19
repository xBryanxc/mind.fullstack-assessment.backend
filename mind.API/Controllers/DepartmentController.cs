using Microsoft.AspNetCore.Mvc;
using mind.Core.Interfaces.IServices;
using mind.Core.Models;
namespace mind.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    protected BaseApiResponse _response;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
        _response = new BaseApiResponse(); 
    }

    [HttpGet(Name = "GetDepartments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseApiResponse>> GetDepartments()
    {
        var departments = await _departmentService.GetAllDepartments();
        if (departments == null || departments.ToList().Count == 0)
        {
            _response.StatusCode = System.Net.HttpStatusCode.NotFound;
            _response.Result = departments;
            return NotFound(_response);
        }
        _response.Result = departments;
        return Ok(_response);
    }
}
