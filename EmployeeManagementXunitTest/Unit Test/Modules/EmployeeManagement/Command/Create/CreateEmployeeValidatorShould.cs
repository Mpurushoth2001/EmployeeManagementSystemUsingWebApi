using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using FluentValidation.TestHelper;

namespace EmployeeManagement.Unit_Test.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployeeValidatorShould
    {
        CreateEmployeeValidator validator;
        public CreateEmployeeValidatorShould()
        {
            validator=new CreateEmployeeValidator();
        }
        #region Unit Test For First Name
        [Fact]
        public void FailsOnEmptyFirstName()
        {
            var request = new CreateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.FirstName,request);
        }
        [Fact]
        public void FailsOnLowCharactersInFirstName() 
        {
            var request = new CreateEmployee() { FirstName="we"};
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
        }
        [Fact]
        public void FailsOnInvalidFirstName()
        {
            var request = new CreateEmployee() { FirstName = "First3name" };
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
        }
        [Fact]
        public void ValidFirstName()
        {
            var request = new CreateEmployee() { FirstName = "FirstName" };
            validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, request);
        }
        #endregion

        #region unit test for Last Name
        [Fact]
        public void FailsOnInvalidLastName()
        {
            var request = new CreateEmployee() { Lastname = "000" };
            validator.ShouldHaveValidationErrorFor(x => x.Lastname, request);
        }
        [Fact]
        public void ValidLastName()
        {
            var request = new CreateEmployee() { Lastname = "LastName" };
            validator.ShouldNotHaveValidationErrorFor(x => x.Lastname, request);
        }
        #endregion

        #region Unit Test For Gender
        [Fact]
        public void FailsOnNullGender()
        {
            var request = new CreateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x=>x.Gender, request);
        }
        [Fact]
        public void FailsOnInvalidGender()
        {
            var request = new CreateEmployee() {Gender='g' };
            validator.ShouldHaveValidationErrorFor(x => x.Gender, request);
        }
        [Fact]
        public void ValidGender()
        {
            var request = new CreateEmployee() { Gender = 'M' };
            validator.ShouldNotHaveValidationErrorFor(x => x.Gender, request);
        }
        #endregion

        #region Unit Test For Date Of Birth
        [Fact]
        public void FailsOnNullDOB() 
        {
            var request = new CreateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x=>x.DOB,request);
        }
        [Fact]
        public void FailsOnInvalidDOB()
        {
            var request = new CreateEmployee() { DOB=DateTime.MinValue};
            validator.ShouldHaveValidationErrorFor(x => x.DOB, request);
        }
        [Fact]
        public void FailsOnTodayDateDOB()
        {
            var request = new CreateEmployee() { DOB = DateTime.Today };
            validator.ShouldHaveValidationErrorFor(x => x.DOB, request);
        }
        [Fact]
        public void ValidDOB()
        {
            var request = new CreateEmployee() { DOB = Convert.ToDateTime("2001/10/23") };
            validator.ShouldNotHaveValidationErrorFor(x => x.DOB, request);
        }
        #endregion

        #region Unit Test For Designation
        [Fact]
        public void FailsOnEmptyDesignation()
        {
            var request = new CreateEmployee() { };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void FailsOnLowCharactersInDesignation()
        {
            var request = new CreateEmployee() { Designation = "de" };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void FailsOnInvalidDesignation()
        {
            var request = new CreateEmployee() { Designation = "Desig23@#nation" };
            validator.ShouldHaveValidationErrorFor(x => x.Designation, request);
        }
        [Fact]
        public void ValidDesignation()
        {
            var request = new CreateEmployee() { Designation = "Frontend Developer" };
            validator.ShouldNotHaveValidationErrorFor(x => x.Designation, request);
        }
        #endregion
    }
}
