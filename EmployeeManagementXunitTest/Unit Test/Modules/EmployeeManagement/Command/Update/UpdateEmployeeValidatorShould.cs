using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployeeValidatorShould
    {
        UpdateEmployeeValidator validator;
        public UpdateEmployeeValidatorShould()
        {
            validator = new UpdateEmployeeValidator();
        }
        #region Unit Test For Employee ID
        [Fact]
        public void FailsOnNullEmployeeID() 
        {
            var request = new UpdateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x=>x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnEmptyEmployeeID()
        {
            var request = new UpdateEmployee() { EmployeeId=000};
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void FailsOnInvalidEmployeeID()
        {
            var request = new UpdateEmployee() {EmployeeId=-100 };
            validator.ShouldHaveValidationErrorFor(x => x.EmployeeId, request);
        }
        [Fact]
        public void ValidEmployeeId()
        {
            var request = new UpdateEmployee() { EmployeeId = 9 };
            validator.ShouldNotHaveValidationErrorFor(x => x.EmployeeId, request);
        }

        #endregion

        #region Unit Test For First Name
        [Fact]
        public void FailsOnEmptyFirstName()
        {
            var request = new UpdateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
        }
        [Fact]
        public void FailsOnLowCharactersInFirstName()
        {
            var request = new UpdateEmployee() { FirstName = "we" };
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
        }
        [Fact]
        public void FailsOnInvalidFirstName()
        {
            var request = new UpdateEmployee() { FirstName = "First3name" };
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
        }
        [Fact]
        public void ValidFirstName() 
        {
            var request = new UpdateEmployee() { FirstName = "FirstName" };
            validator.ShouldNotHaveValidationErrorFor(x=>x.FirstName, request);
        }
        #endregion

        #region unit test for Last Name
        [Fact]
        public void FailsOnInvalidLastName()
        {
            var request = new UpdateEmployee() { Lastname = "000" };
            validator.ShouldHaveValidationErrorFor(x => x.Lastname, request);
        }
        [Fact]
        public void ValidLastName()
        {
            var request = new UpdateEmployee() { Lastname = "LastName" };
            validator.ShouldNotHaveValidationErrorFor(x => x.Lastname, request);
        }
        #endregion

        #region Unit Test For Gender
        [Fact]
        public void FailsOnNullGender()
        {
            var request = new UpdateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.Gender, request);
        }
        [Fact]
        public void FailsOnInvalidGender()
        {
            var request = new UpdateEmployee() { Gender = 'g' };
            validator.ShouldHaveValidationErrorFor(x => x.Gender, request);
        }
        [Fact]
        public void ValidGenderName()
        {
            var request = new UpdateEmployee() { Gender = 'M' };
            validator.ShouldNotHaveValidationErrorFor(x => x.Gender, request);
        }

        #endregion

        #region Unit Test For Date Of Birth
        [Fact]
        public void FailsOnNullDOB()
        {
            var request = new UpdateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.DOB, request);
        }
        [Fact]
        public void FailsOnInvalidDOB()
        {
            var request = new UpdateEmployee() { DOB = DateTime.MinValue };
            validator.ShouldHaveValidationErrorFor(x => x.DOB, request);
        }
        [Fact]
        public void FailsOnTodayDateDOB()
        {
            var request = new UpdateEmployee() { DOB = DateTime.Today };
            validator.ShouldHaveValidationErrorFor(x => x.DOB, request);
        }
        [Fact]
        public void ValidDOB()
        {
            var request = new UpdateEmployee() { DOB =Convert.ToDateTime("2001/10/23") };
            validator.ShouldNotHaveValidationErrorFor(x => x.DOB, request);
        }

        #endregion

        #region Unit Test For Designation
        [Fact]
        public void FailsOnEmptyDesignation()
        {
            var request = new UpdateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void FailsOnLowCharactersInDesignation()
        {
            var request = new UpdateEmployee() { FirstName = "de" };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void FailsOnInvalidDesignation()
        {
            var request = new UpdateEmployee() { FirstName = "Desig23@#nation" };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void ValidDesignation()
        {
            var request = new UpdateEmployee() { Designation = "Frontend Developer" };
            validator.ShouldNotHaveValidationErrorFor(x => x.Designation, request);
        }
        #endregion
    }
}
