using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Controllers;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using MediatR;
using Moq;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;

namespace EmployeeManagementXunitTest.Unit_Test.Controllers.EmployeeController1
{
    public class CreateNewEmployeeShould
    {
        private Mock<IMediator> mediatormock;
        private EmployeeController employeecontrollermock;

        public CreateNewEmployeeShould()
        {
            mediatormock = new Mock<IMediator>();
            employeecontrollermock=new EmployeeController(mediatormock.Object);
        }


        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task ReturnsCorrectResponse_CreateNewEmployee(CreateEmployee request)
        {
            #region Assign
            var response = new EntityResponse() {ResponseId=1,AdditionalInfo="Employee Details Added Successfully" };

            mediatormock.Setup(x=>x.Send(request,default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeecontrollermock.CreateNewEmployee(request);
            #endregion

            #region Assert
            Assert.IsAssignableFrom<EntityResponse>(response);
            #endregion
        }


        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task ReturnsFailResponse_CreateNewEmployee(CreateEmployee request)
        {
            #region Assign
            var response = new EntityResponse() { ResponseId = 0 };

            mediatormock.Setup(x => x.Send(request, default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeecontrollermock.CreateNewEmployee(request);
            #endregion

            #region Assert
            Assert.NotEqual(1,response.ResponseId);
            #endregion
        }
        public class TestDataProvider        {            public static IEnumerable<object[]> CreateObject()            {                yield return new object[] {                    new CreateEmployee()                    {
                        FirstName="Vishnu",
                        Lastname="Palani",
                        DOB=Convert.ToDateTime("1995/11/09"),
                        Designation="Developer",
                        Gender='M'
                    }                };            }        }

    }
}
