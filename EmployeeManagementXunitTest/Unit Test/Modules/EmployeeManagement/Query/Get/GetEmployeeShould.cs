using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Query.Get;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Modules.EmployeeManagement.Query.Get.GetEmployee;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Query.Get
{
    public class GetEmployeeShould
    {
        DbContextOptions<EmployeeDbcontext> dbContextOptions { get; } = new DbContextOptionsBuilder<EmployeeDbcontext>().Options;
        

        [Fact]
        public void PassValidation()
        {
            DbContextMock<EmployeeDbcontext> dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptions);
            GetEmployeeHandler handler = new GetEmployeeHandler(dbContextMock.Object);
            List<EmployeeModel> employeeModels = new List<EmployeeModel>() { new EmployeeModel { EmployeeId = 1, FirstName = "Anu", Lastname = "", DOB = Convert.ToDateTime("2000-11-26"), Designation = "Developer", Gender = 'm' } };                             
            dbContextMock.CreateDbSetMock(x => x.Employees, employeeModels);

            var request = new GetEmployee();
            var response = handler.Handle(request, CancellationToken.None);
            Assert.True(response.Result.Count > 0);
        }

        [Fact]
        public void ThrowsNoDataFoundException()
        {
            DbContextMock<EmployeeDbcontext> dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptions);
            GetEmployeeHandler handler = new GetEmployeeHandler(dbContextMock.Object);
            List<EmployeeModel> employeeModels = new List<EmployeeModel>() { };
            dbContextMock.CreateDbSetMock(x => x.Employees, employeeModels);

            var request = new GetEmployee() { };
            var response = Record.ExceptionAsync(async () => await handler.Handle(request, CancellationToken.None));
            Assert.IsType<ExceptionModel.NoDataFoundException>(response.Result);
        }
    }
}
