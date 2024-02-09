using Api.Models;

namespace Api.Data
{
	public interface IDataAdapter
	{
		Task<IEnumerable<Employee>> GetAllAsync();
		Task<Employee> GetByIdAsync(int id);
		Task<Employee> CreateAsync(Employee employeeDto);
	}
}

