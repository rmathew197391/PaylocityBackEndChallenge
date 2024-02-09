using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _employeeService.getEmployeeByIdAsync(id);
            var result = new ApiResponse<GetEmployeeDto> {
                Data = employee,
                Success = true
            };
            return result;
        }
        catch
        {
            return NotFound();
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            var employees = await _employeeService.getAllEmployeesAsync();

            var result = new ApiResponse<List<GetEmployeeDto>?>
            {
                Data = (List<GetEmployeeDto>?)employees,
                Success = true
            };
            return result;
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [SwaggerOperation(Summary = "Create Employee")]
    [HttpPost("")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Create(GetEmployeeDto employee)
    {
        try
        {
            var newEmployee = await _employeeService.createEmployeeAsync(employee);
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = newEmployee,
                Success = true
            };
            return result;
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
