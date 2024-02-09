using Api.Business;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

//Auto mapper profile for mapping dtos to models
namespace Api.Bootstrap
{
    public class AutoMapperProfile : Profile
    {
        PayCheckCalculator _payCheckCalculator;

        public AutoMapperProfile()
        {
            _payCheckCalculator = new PayCheckCalculator();

            CreateMap<GetDependentDto, Dependent>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GetEmployeeDto, Employee>()
                .ReverseMap();
        }
    }
}

