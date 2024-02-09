using Api.Dtos.Dependent;

namespace Api.Services
{
	public interface IDependentService
	{
		Task<IEnumerable<GetDependentDto>?> GetDependentAsync();
		Task<GetDependentDto> GetDependentByIdAsync(int id);
	}
}

