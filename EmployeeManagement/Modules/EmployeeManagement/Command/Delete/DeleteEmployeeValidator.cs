using FluentValidation;

namespace EmployeeManagement.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployee>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}
