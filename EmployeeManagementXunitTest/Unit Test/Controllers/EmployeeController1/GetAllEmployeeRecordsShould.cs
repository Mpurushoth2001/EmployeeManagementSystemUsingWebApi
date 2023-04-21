using EmployeeManagement.Controllers;
using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Modules.EmployeeManagement.Query.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagementXunitTest.Unit_Test.Controllers.EmployeeController1
{
    public class GetAllEmployeeRecordsShould
    {
        private Mock<IMediator> mediatormock;
        private EmployeeController employeeController;

        public List<EmployeeModel> employees = new List<EmployeeModel>
        {
            new EmployeeModel{ EmployeeId=1,FirstName="Vishnu",Lastname="Palani",DOB=Convert.ToDateTime("1995/11/09"),Designation="Developer",Gender='M' }
        };

        public GetAllEmployeeRecordsShould()
        {
            mediatormock = new Mock<IMediator>();
            employeeController = new EmployeeController(mediatormock.Object);
        }
        [Theory]
        [InlineData()]
        public async Task GetEmployeeTest()
        {
            #region Assign
            var request = new GetEmployee();
            mediatormock.Setup(x => x.Send(request, default)).ReturnsAsync(employees);
            #endregion

            #region Act
            var result = await employeeController.GetAllEmployeeRecords();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
            #endregion
        }
    }
}
