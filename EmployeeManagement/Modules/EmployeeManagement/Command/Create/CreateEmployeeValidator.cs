using EmployeeManagement.Validator;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployee>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(3, 20).WithMessage("{PropertyName} must contain valid Name")
                .SetValidator(new NameValidator()).WithMessage("{PropertyName} should be Letters");

            RuleFor(x => x.Lastname).SetValidator(new NameValidator())
                .When(y => !string.IsNullOrEmpty(y.Lastname));

            RuleFor(x => x.Gender).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .SetValidator(new GenderValidator())
                .SetValidator(new NameValidator());

            RuleFor(x => x.DOB).NotEmpty().NotNull().Must(BeAValidAge).WithMessage("Invalid Date Of Birth");

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .SetValidator(new NameValidator());
        }
        protected bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > currentYear - 120)
            {
                return true;
            }

            return false;
        }
    }
}
