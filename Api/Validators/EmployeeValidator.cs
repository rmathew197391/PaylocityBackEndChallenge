using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using FluentValidation;

namespace Api.Validators
{
    //Validation rules used by fluentvalidation
    public class EmployeeValidator : AbstractValidator<GetEmployeeDto>
    {
        const string MaxSpousesExceededMessage = "Please include only one spouse or domestic partner.";

        public EmployeeValidator()
        {
            RuleFor(x => x.Dependents)
                .Must(CheckSpouse)
                .WithMessage(MaxSpousesExceededMessage);
        }

        private bool CheckSpouse(ICollection<GetDependentDto> arg)
        {
            if (arg.Where(x => x.Relationship == Models.Relationship.Spouse || x.Relationship == Models.Relationship.DomesticPartner).Count() > 1)
            {
                return false;
            }
            return true;
        }
    }
}

