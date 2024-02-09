using Api.Data;
using Api.Models;
using AutoMapper;
using Api.Validators;
using Api.Dtos.Employee;
using Api.Business;

namespace Api.Services
{
    //Service class to get employees
    //Using fluent validation package to perform validations
    //Using auto mapper package to map between dtos and models
    public class EmployeeService: IEmployeeService
	{
        IDataAdapter _dataAdapter;
        IMapper _mapper;
        IPayCheckCalculator _payCheckCalculator;
        EmployeeValidator _validator;

		public EmployeeService(IDataAdapter dataAdapter, IMapper mapper, IPayCheckCalculator payCheckCalculator)
		{
            _dataAdapter = dataAdapter;
            _mapper = mapper;
            _payCheckCalculator = payCheckCalculator;
            _validator = new EmployeeValidator();
		}

        //Create Employee
        public async Task<GetEmployeeDto?> createEmployeeAsync(GetEmployeeDto employee)
        {
            //Validate that an employee only has 1 spouse or domestic partner (not both)
            _validator.Validate(employee);
            var employeeDto = _mapper.Map<Employee>(employee) ?? throw new ArgumentNullException("Mandetory Parameter", nameof(employee));
            return _mapper.Map<GetEmployeeDto>(await _dataAdapter.CreateAsync(employeeDto));
        }

        //Get all employees
        public async Task<IEnumerable<GetEmployeeDto>?> getAllEmployeesAsync() {
            var results = await _dataAdapter.GetAllAsync();
            return _mapper.Map<IList<GetEmployeeDto>>(results);
        }

        //Get employee by id
        public async Task<GetEmployeeDto?> getEmployeeByIdAsync(int id)
        {
            var result = await _dataAdapter.GetByIdAsync(id);
            return _mapper.Map<GetEmployeeDto>(result);
        }

        //Get employee paychecks
        public async Task<IEnumerable<GetPayCheckDto>> getPayChecks(int id)
        {
            var result = await _dataAdapter.GetByIdAsync(id);
            return _payCheckCalculator.CalculatePayChecks(result);
        }
    }
}

