using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using FluentValidation.TestHelper;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployeeValidatorShould
    {
        DeleteEmployeeValidator validator;
        public DeleteEmployeeValidatorShould() 
        {
            validator = new DeleteEmployeeValidator();
        }
        #region Unit Test For Employee ID
        [Fact]
        public void FailsOnNullEmployeeID()
        {
            var request = new DeleteEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnEmptyEmployeeID()
        {
            var request = new DeleteEmployee() { EmployeeId = 000 };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnInvalidEmployeeID()
        {
            var request = new DeleteEmployee() { EmployeeId = -100 };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void PassOnValidEmployeeId()
        {
            var request = new DeleteEmployee() { EmployeeId = 9 };
            validator.ShouldNotHaveValidationErrorFor(x => x.EmployeeId, request);
        }

        #endregion

    }
}
