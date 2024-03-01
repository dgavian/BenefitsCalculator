using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public DependentsController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _employeeService.GetDependentAsync(id);
        var isFound = dependent != null;
        var func = GetObjectResultFunc(isFound);
        var response = MakeResponse(dependent, isFound);
        return func(response);
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _employeeService.GetAllDependentsAsync();
        var sucess = true;
        var response = MakeResponse(dependents, sucess);
        return Ok(response);
    }
}
