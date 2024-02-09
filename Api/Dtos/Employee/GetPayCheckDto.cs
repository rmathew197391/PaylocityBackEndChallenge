using System;
namespace Api.Dtos.Employee
{
	public class GetPayCheckDto
	{
        public int PayPeriod { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal BenefitsCost { get; set; }
    }
}

