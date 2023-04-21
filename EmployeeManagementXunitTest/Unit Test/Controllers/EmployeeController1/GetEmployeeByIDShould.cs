using EmployeeManagement.Controllers;
using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EmployeeManagement.Modules.EmployeeManagement.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagementXunitTest.Unit_Test.Controllers.EmployeeController1
{
    public class GetEmployeeByIDShould
    {
        private Mock<IMediator> mediatormock;
        private EmployeeController employeeController;

        
        public GetEmployeeByIDShould()
        {
            mediatormock = new Mock<IMediator>();
            employeeController = new EmployeeController(mediatormock.Object);
        }

        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task GetEmployeeByIDTest(GetEmployeeByID request)
        {
            #region Assign
            var response = new List<EmployeeModel>();
            mediatormock.Setup(x => x.Send(It.IsAny<EmployeeModel>, default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeeController.GetByEmployeeID(request.EmployeeId);
            #endregion

            #region Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            #endregion
        }
        public class TestDataProvider        {            public static IEnumerable<object[]> CreateObject()            {                yield return new object[] {                    new GetEmployeeByID()                    {
                        EmployeeId=1,
                    }                };            }        }
    }
}
