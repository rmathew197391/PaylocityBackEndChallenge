// This is a mock data adapter.  In production we would connect to a data source and get data async

using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Data
{
	public class DataAdapter: IDataAdapter
	{
        //This is temporaray until data source is created.
        //This class and methods will be replaces by SQL Data Adapter in production
        IEnumerable<Employee> employees;

		public DataAdapter()
		{
            employees = new List<Employee>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "LeBron",
                        LastName = "James",
                        Salary = 75420.99m,
                        DateOfBirth = new DateTime(1984, 12, 30)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Ja",
                        LastName = "Morant",
                        Salary = 92365.22m,
                        DateOfBirth = new DateTime(1999, 8, 10),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 1,
                                FirstName = "Spouse",
                                LastName = "Morant",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1998, 3, 3)
                            },
                            new()
                            {
                                Id = 2,
                                FirstName = "Child1",
                                LastName = "Morant",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2020, 6, 23)
                            },
                            new()
                            {
                                Id = 3,
                                FirstName = "Child2",
                                LastName = "Morant",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2021, 5, 18)
                            }
                        }
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Michael",
                        LastName = "Jordan",
                        Salary = 143211.12m,
                        DateOfBirth = new DateTime(1963, 2, 17),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 4,
                                FirstName = "DP",
                                LastName = "Jordan",
                                Relationship = Relationship.DomesticPartner,
                                DateOfBirth = new DateTime(1974, 1, 2)
                            }
                        }
                    }
                };
        }

        //Get all employee records
        public async Task<IEnumerable<Employee>> GetAllAsync() => await Task.FromResult(employees);

        //Get employee record by id
        public async Task<Employee> GetByIdAsync(int id) => await Task.FromResult(employees.First(x => x.Id == id));

        //Create employee record
        public async Task<Employee> CreateAsync(Employee employeeDto)
        {
            var allEmployees = await GetAllAsync() ?? throw new Exception("Unable To Get Next Id");
            employeeDto.Id = allEmployees.Select(x => x.Id).Max() + 1;
            var dependentId = allEmployees.SelectMany(x => x.Dependents).Select(x => x.Id).Max() + 1;
            foreach(var dependent in employeeDto.Dependents)
            {
                dependent.Id = dependentId;
                dependentId += 1;
            }
            //Add new employee to in memory data store
            //This will be reset to whats in the init every time the service is restarted
            //This should not be used in production
            //Please replace with a real data adapter
            employees = employees.Append(employeeDto);
            return employeeDto;
        }
    }
}

