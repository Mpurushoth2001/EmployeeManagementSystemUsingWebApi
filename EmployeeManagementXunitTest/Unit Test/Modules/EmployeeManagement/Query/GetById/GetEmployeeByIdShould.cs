using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Query.GetById;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Modules.EmployeeManagement.Query.GetById.GetEmployeeByID;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Query.GetById
{
    public class GetEmployeeByIdShould
    {
        DbContextMock<EmployeeDbcontext> dbContextMock;
        GetEmployeeByIDHandler handler;
        List<EmployeeModel> employeeModels = new List<EmployeeModel>() { new EmployeeModel { EmployeeId = 1, FirstName = "Anu", Lastname = "", DOB = Convert.ToDateTime("2000-11-26"), Designation = "Developer", Gender = 'm' } };
        DbContextOptions<EmployeeDbcontext> dbContextOptionsMock { get; }=new DbContextOptionsBuilder<EmployeeDbcontext>().Options;
        public GetEmployeeByIdShould()
        {
            dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptionsMock);
            handler=new GetEmployeeByIDHandler(this.dbContextMock.Object);
            dbContextMock.CreateDbSetMock(x => x.Employees, employeeModels);
        }
        [Fact]
        public void PassValidation()
        {
            var request = new GetEmployeeByID() { EmployeeId = 1 };
            var response = handler.Handle(request, CancellationToken.None);
            Assert.True(response.Result != null);
        }
        [Fact]
        public void ThrowsInvalidIDException()
        {
            var request = new GetEmployeeByID() { EmployeeId = 2 };
            var response = Record.ExceptionAsync(async () => await handler.Handle(request, CancellationToken.None));
            Assert.IsType<ExceptionModel.InvalidIDException>(response.Result);
        }
    }
}
