using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Modules.EmployeeManagement.Command.Delete.DeleteEmployee;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Delete
{
    public class DeleteEmployeeShould
    {
        DbContextMock<EmployeeDbcontext> dbContextmock;
        DeleteEmployeeHandler handler;
        List<EmployeeModel> employeeModels = new List<EmployeeModel>() {new EmployeeModel { EmployeeId = 1, FirstName = "Anu", Lastname = "", DOB = Convert.ToDateTime("2000-11-26"), Designation = "Developer", Gender = 'm' } };
        DbContextOptions<EmployeeDbcontext> dbOptions { get; }=new DbContextOptionsBuilder<EmployeeDbcontext>().Options;
        public DeleteEmployeeShould()
        {
            dbContextmock = new DbContextMock<EmployeeDbcontext>(dbOptions);
            handler = new DeleteEmployeeHandler(this.dbContextmock.Object);
            dbContextmock.CreateDbSetMock(x => x.Employees, employeeModels);
        }
        [Fact]
        public void PassValidation()
        {
            var request = new DeleteEmployee { EmployeeId = 1 };
            dbContextmock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(()=>{ return 1; }));
            var Response = handler.Handle(request, CancellationToken.None);
            Assert.True(Response.Result.ResponseId == 1);
        }
        [Fact]
        public void ThrowsInvalidException()
        {
            var request = new DeleteEmployee { EmployeeId = 2};//invalid Id
            var response = Record.ExceptionAsync(async () => await handler.Handle(request, CancellationToken.None));
            Assert.IsType<ExceptionModel.EmployeeIDNotFoundException>(response.Result);
        }
    }
}
