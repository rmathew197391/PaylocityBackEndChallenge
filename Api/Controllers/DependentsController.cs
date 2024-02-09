using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _dependentService.GetDependentByIdAsync(id) ?? throw new Exception("Dependent not found");
            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };

            return result;
        }
        catch
        {
            return NotFound();
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependents = await _dependentService.GetDependentAsync();
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = (List<GetDependentDto>?)dependents,
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
