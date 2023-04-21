using EmployeeManagement.Controllers;
using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using MediatR;
using Moq;
using System.Reflection;

namespace EmployeeManagementXunitTest.Unit_Test.Controllers.EmployeeController1
{
    public class UpdateEmployeeRecordShould
    {
        private Mock<IMediator> mediatormock;
        private EmployeeController employeeControllerMock;
        
        public UpdateEmployeeRecordShould()
        {
            mediatormock = new Mock<IMediator>();
            employeeControllerMock = new EmployeeController(mediatormock.Object);
        }
        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject),MemberType=typeof(TestDataProvider))]
        public async Task ReturnCorrectResponse_UpdateEmployee(UpdateEmployee request)
        {
            #region Assign
            var response = new EntityResponse() { ResponseId=1,AdditionalInfo= "Employee Details Are Updated" };
            mediatormock.Setup(x=>x.Send(request,default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeeControllerMock.UpdateEmployeeRecord(request);
            #endregion

            #region Assert
            Assert.IsAssignableFrom<EntityResponse>(result);
            #endregion
        }
        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task ReturnFailResponse_UpdateEmployee(UpdateEmployee request)
        {
            #region Assign
            var response = new EntityResponse() { ResponseId = 0, AdditionalInfo = "Invalid ID" };
            mediatormock.Setup(x=>x.Send(request,default)).ReturnsAsync(response);
            #endregion

            #region act
            var result = await employeeControllerMock.UpdateEmployeeRecord(request);
            #endregion

            #region Assert
            Assert.NotEqual(1,response.ResponseId);
            #endregion
        }
        public class TestDataProvider        {            public static IEnumerable<object[]> CreateObject()            {                yield return new object[] {                    new UpdateEmployee()                    {                                                EmployeeId=1,
                        FirstName="Vishnu",
                        Lastname="Palani",
                        DOB=Convert.ToDateTime("1995/11/09"),
                        Designation="Developer",
                        Gender='M'
                    }                };            }        }
    }
}
