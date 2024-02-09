using System;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Business
{
	public interface IPayCheckCalculator
	{
        IEnumerable<GetPayCheckDto> CalculatePayChecks(Employee employee);

    }
}

