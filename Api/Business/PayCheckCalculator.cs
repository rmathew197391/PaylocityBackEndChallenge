using Api.Dtos.Employee;
using Api.Models;

namespace Api.Business
{
	public class PayCheckCalculator: IPayCheckCalculator
	{
        public IEnumerable<GetPayCheckDto> CalculatePayChecks(Employee employee)
        {
            var numberOfPayPeriods = 26;
            //Get yearly benefit cost
            var yearlyBenefitCost = CalculateYearlyBenefitCost(employee);
            //Get additional benefit cost to be subtracted from last check
            var lastCheckBenefitCostAdditional = yearlyBenefitCost % numberOfPayPeriods;
            //Set yearl benefit cost minus the additional from the last check to spread cost evenly accross 25 pay periods.
            //26th pay period will be benefit cost the last plus additional calculated above
            yearlyBenefitCost -= lastCheckBenefitCostAdditional;
            //Get benefit cost per pay period
            var benefitCostPerPayPeriod = yearlyBenefitCost / numberOfPayPeriods;
            //Yearly salary from employee record rounded to 2 decimal places
            var yearlySalary = Math.Round(employee.Salary, 2);
            //Get additional pay for last pay period to get pay spread evenly over 25 pay periods
            var lastCheckPayAdditional = yearlySalary % numberOfPayPeriods;
            //Set yearly salary minus the additional for the last check above
            //26th pay period will be the last salary plus additional calculated above
            yearlySalary -= lastCheckPayAdditional;
            //Get salary per pay period
            var salaryPerPayPeriod = yearlySalary / numberOfPayPeriods;
            //Init list of paychecks
            var payChecks = new List<GetPayCheckDto>();
            for (int i = 1; i <= numberOfPayPeriods; i++)
            {
                if(i == numberOfPayPeriods)
                {
                    //Last check will have additional added to gross pay and additional added to benefit cost
                    payChecks.Add(new GetPayCheckDto
                    {
                        PayPeriod = i,
                        GrossPay = salaryPerPayPeriod + lastCheckPayAdditional,
                        BenefitsCost = benefitCostPerPayPeriod + lastCheckBenefitCostAdditional,
                        NetPay = (salaryPerPayPeriod + lastCheckPayAdditional) - (benefitCostPerPayPeriod + lastCheckBenefitCostAdditional)
                    });
                }
                else
                {
                    payChecks.Add(new GetPayCheckDto
                    {
                        PayPeriod = i,
                        GrossPay = salaryPerPayPeriod,
                        BenefitsCost = benefitCostPerPayPeriod,
                        NetPay = salaryPerPayPeriod - benefitCostPerPayPeriod
                    });
                }
            }
            return payChecks;
        }

        private decimal CalculateYearlyBenefitCost(Employee employee)
        {
            //Calculate employee cost per year
            var employeeCost = employee.Salary >= 80000 ? 12000 + (Math.Round(employee.Salary, 2) * 0.02M) : 12000;
            //Calculate dependent cost per year
            var dependentCost = employee.Dependents.Select(x => GetAgeInYears(x.DateOfBirth) >= 50 ? 800 : 600).Sum() * 12;
            //Return sum of boath rounding to 2 decimal places
            return Math.Round(employeeCost + dependentCost, 2);
        }

        private int GetAgeInYears(DateTime birthdate)
        {
            var age = DateTime.Today.Year - birthdate.Year;
            if (birthdate.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }
}

