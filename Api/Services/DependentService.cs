using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;

namespace Api.Services
{
    //Service class to return all or one dependents
    //Using auto mapper package to map between dtos and models
    public class DependentService: IDependentService
	{
        IEmployeeService _employeeService;
        IMapper _mapper;

		public DependentService(IEmployeeService employeeService, IMapper mapper)
		{
            _employeeService = employeeService;
            _mapper = mapper;
		}

        //Get all dependents
        public async Task<IEnumerable<GetDependentDto>?> GetDependentAsync()
        {
            var employeeDtos = await _employeeService.getAllEmployeesAsync() ?? throw new Exception("Unable to retrieve employees");            
            var dependentDtos = employeeDtos
            .SelectMany(x => x.Dependents)
            .ToList();
            return dependentDtos;
        }

        //Get dependent by id
        public async Task<GetDependentDto> GetDependentByIdAsync(int id)
        {
            var dependents = await GetDependentAsync() ?? throw new Exception("Unable to retrieve dependents");
            return dependents.Where(x => x.Id == id).First();
        }
    }
}

