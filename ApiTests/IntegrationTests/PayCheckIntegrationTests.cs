using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Employee;
using Api.Models;
using Newtonsoft.Json;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PayCheckIntegrationTests: IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployee1Checks_ShouldReturnChecksWithExpectedAmounts()
    {
        //Explanation of values
        //Employee makes 75420.99 a year.  Modulus of salary divided by 26 is 20.99
        //Employee has no dependents so total of benefits for year is 12000
        ////(12000 for employee).  Modulus of benefits divided by 26 is 14
        //Employee will be paid gross amount of 2900 per pay period for 25 pay periods
        //Employee will pay 461 in benifits per pay period for 25 pay periods
        //Employee will be paid a net amound of 2439 per pay period for 25 pay periods
        //Employee will pay 475 in benifits for the 26th pay period
        //Employee will be paid gross amount of 2920.99 for the 26th pay period
        //Employee will be paid net amount of 2445.99 for the 26th pay period
        var response = await HttpClient.GetAsync("/api/v1/paychecks/1");
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetPayCheckDto>>>(await response.Content.ReadAsStringAsync());
        var firstCheck = apiResponse.Data?.Where(x => x.PayPeriod == 1).First();
        var lastCheck = apiResponse.Data?.Where(x => x.PayPeriod == 26).First();
        Assert.Equal(26, apiResponse.Data?.Count);
        Assert.Equal(2900M, firstCheck?.GrossPay);
        Assert.Equal(2439M, firstCheck?.NetPay);
        Assert.Equal(461M, firstCheck?.BenefitsCost);
        Assert.Equal(2920.99M, lastCheck?.GrossPay);
        Assert.Equal(2445.99M, lastCheck?.NetPay);
        Assert.Equal(475M, lastCheck?.BenefitsCost);
    }

    [Fact]
    public async Task WhenAskedForAllEmployee2Checks_ShouldReturnChecksWithExpectedAmounts()
    {
        //Explanation of values
        //Employee makes 92365.22 a year.  Modulus of salary divided by 26 is 13.22
        //Employee has 3 dependents and makes over 80K a year so total of benefits for year is 35447.30
        ////(12000 for employee, 21600 for dependents, 1847.30 for making over 80k).  Modulus of benefits divided by 26 is 9.3
        //Employee will be paid gross amount of 3552 per pay period for 25 pay periods
        //Employee will pay 1363 in benifits per pay period for 25 pay periods
        //Employee will be paid a net amound of 2189 per pay period for 25 pay periods
        //Employee will pay 1372.30 in benifits for the 26th pay period
        //Employee will be paid gross amount of 3565.22 for the 26th pay period
        //Employee will be paid net amount of 2192.92 for the 26th pay period
        var response = await HttpClient.GetAsync("/api/v1/paychecks/2");
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetPayCheckDto>>>(await response.Content.ReadAsStringAsync());
        var firstCheck = apiResponse.Data?.Where(x => x.PayPeriod == 1).First();
        var lastCheck = apiResponse.Data?.Where(x => x.PayPeriod == 26).First();
        Assert.Equal(26, apiResponse.Data?.Count);
        Assert.Equal(3552M, firstCheck?.GrossPay);
        Assert.Equal(2189M, firstCheck?.NetPay);
        Assert.Equal(1363M, firstCheck?.BenefitsCost);
        Assert.Equal(3565.22M, lastCheck?.GrossPay);
        Assert.Equal(2192.92M, lastCheck?.NetPay);
        Assert.Equal(1372.3M, lastCheck?.BenefitsCost);
    }

    [Fact]
    public async Task WhenAskedForAllEmployee3Checks_ShouldReturnChecksWithExpectedAmounts()
    {
        //Explanation of values
        //Employee makes 143211.12 a year.  Modulus of salary divided by 26 is 3.12
        //Employee has 1 dependent over 50 so total of benefits for year is 24464.22
        ////(12000 for employee, 9600 for dependent, 2864.22 for making over 80k).  Modulus of benefits divided by 26 is 24.22
        //Employee will be paid gross amount of 5508 per pay period for 25 pay periods
        //Employee will pay 940 in benifits per pay period for 25 pay periods
        //Employee will be paid a net amound of 4568 per pay period for 25 pay periods
        //Employee will pay 964.22 in benifits for the 26th pay period
        //Employee will be paid gross amount of 5511.12 for the 26th pay period
        //Employee will be paid net amount of 4546.90 for the 26th pay period
        var response = await HttpClient.GetAsync("/api/v1/paychecks/3");
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetPayCheckDto>>>(await response.Content.ReadAsStringAsync());
        var firstCheck = apiResponse.Data?.Where(x => x.PayPeriod == 1).First();
        var lastCheck = apiResponse.Data?.Where(x => x.PayPeriod == 26).First();
        Assert.Equal(26, apiResponse.Data?.Count);
        Assert.Equal(5508M, firstCheck?.GrossPay);
        Assert.Equal(4568M, firstCheck?.NetPay);
        Assert.Equal(940M, firstCheck?.BenefitsCost);
        Assert.Equal(5511.12M, lastCheck?.GrossPay);
        Assert.Equal(4546.9M, lastCheck?.NetPay);
        Assert.Equal(964.22M, lastCheck?.BenefitsCost);
    }

    [Fact]
    public async Task WhenAskedForANonexistentEmployeesChecks_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/paychecks/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

}
