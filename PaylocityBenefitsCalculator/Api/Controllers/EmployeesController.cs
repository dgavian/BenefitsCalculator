using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto?>>> Get(int id)
    {
        var employee = await _employeeService.GetEmployeeAsync(id);
        return GetActionResultForNullableItem(employee);
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        var sucess = true;
        var response = MakeResponse(employees, sucess);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Get employee paycheck")]
    [HttpGet("{id}/paycheck")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto?>>> GetPaycheck(int id)
    {
        var paycheck = await _employeeService.GetPaycheckAsync(id);
        return GetActionResultForNullableItem(paycheck);
    }
}
