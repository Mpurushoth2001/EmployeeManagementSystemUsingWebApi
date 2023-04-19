using EmployeeManagement.Validator;
using FluentValidation;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployee>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().NotNull().GreaterThan(0);

            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty().WithMessage("{PropertyName} is empty")
                 .Length(3, 25).WithMessage("{PropertyName} must contain valid Name")
                 .SetValidator(new NameValidator());

            RuleFor(x => x.Lastname).SetValidator(new NameValidator())
                .When(y => !string.IsNullOrEmpty(y.Lastname));

            RuleFor(x => x.Gender).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("The Field is empty")
                .SetValidator(new GenderValidator());
                

            RuleFor(x => x.DOB).NotEmpty().NotNull().SetValidator(new DateOfBirthValitator());

            RuleFor(x => x.Designation).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("The Field is empty")
                .Length(3, 25)
                .SetValidator(new DesignationValidator());

        }
        
    }
}
