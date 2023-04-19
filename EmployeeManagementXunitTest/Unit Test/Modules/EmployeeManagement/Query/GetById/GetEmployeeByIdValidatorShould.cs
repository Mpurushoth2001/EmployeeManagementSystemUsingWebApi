using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using EmployeeManagement.Modules.EmployeeManagement.Query.GetById;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Query.GetById
{
    public class GetEmployeeByIdValidatorShould
    {
        GetEmployeeByIdValidator validator;
        public GetEmployeeByIdValidatorShould() 
        {
            validator = new GetEmployeeByIdValidator();
        }
        #region Unit Test For Employee ID
        [Fact]
        public void FailsOnNullEmployeeID()
        {
            var request = new GetEmployeeByID() { };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnEmptyEmployeeID()
        {
            var request = new GetEmployeeByID() { EmployeeId = 000 };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnInvalidEmployeeID()
        {
            var request = new GetEmployeeByID() { EmployeeId = -100 };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void ValidEmployeeId()
        {
            var request = new GetEmployeeByID() { EmployeeId = 9 };
            validator.ShouldNotHaveValidationErrorFor(x => x.EmployeeId, request);
        }

        #endregion
    }
}
