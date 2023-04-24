using FluentValidation;

namespace EmployeeManagement.Modules.EmployeeManagement.Query.GetById
{
    public class GetEmployeeByIdValidator:AbstractValidator<GetEmployeeByID>
    {
        public GetEmployeeByIdValidator()
        {
            RuleFor(x => x.EmployeeId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
