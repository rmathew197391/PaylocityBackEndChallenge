using Api.Dtos.Employee;

namespace Api.Services
{
	public interface IEmployeeService
	{
		Task<IEnumerable<GetEmployeeDto>?> getAllEmployeesAsync();
		Task<GetEmployeeDto?> getEmployeeByIdAsync(int id);
		Task<GetEmployeeDto?> createEmployeeAsync(GetEmployeeDto employeeDto);
		Task<IEnumerable<GetPayCheckDto>> getPayChecks(int id);
	}
}

