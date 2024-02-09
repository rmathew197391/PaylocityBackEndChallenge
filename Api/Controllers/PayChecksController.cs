using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PayChecksController : Controller
    {
        IEmployeeService _employeeService;

        public PayChecksController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [SwaggerOperation(Summary = "Get Employee Pay Checks")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetPayCheckDto>>>> Get(int id)
        {
            try
            {
                var payChecks = await _employeeService.getPayChecks(id);
                var result = new ApiResponse<List<GetPayCheckDto>>
                {
                    Data = (List<GetPayCheckDto>)payChecks,
                    Success = true
                };
                return result;
            }
            catch
            {
                return NotFound();
            }
        }
    }
}

