using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using static EmployeeManagement.Modules.EmployeeManagement.Command.Update.UpdateEmployee;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployeeShould
    {
        DbContextMock<EmployeeDbcontext> dbContextMock;
        UpdateEmployeeHandler handler;
        List<EmployeeModel> employeeModels = new List<EmployeeModel>() { new EmployeeModel { EmployeeId=1,FirstName="Anu",Lastname="",DOB=Convert.ToDateTime("2000-11-26"),Designation="Developer",Gender='m'} };
        DbContextOptions<EmployeeDbcontext> dbContextOptions {  get;}=new DbContextOptionsBuilder<EmployeeDbcontext>().Options;
        public UpdateEmployeeShould()
        {
            dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptions);
            handler=new UpdateEmployeeHandler(this.dbContextMock.Object);
            dbContextMock.CreateDbSetMock(x => x.Employees, employeeModels);

        }
        [Fact]
        public void PassValidation()
        {
            var request = new UpdateEmployee() { EmployeeId = 1, FirstName = "Anu", Lastname = "", DOB = Convert.ToDateTime("2000-11-26"), Designation = "Developer", Gender = 'F' };
            dbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => { return 1; })).Verifiable();
            var response = handler.Handle(request, CancellationToken.None);
            Assert.True(response.Result.ResponseId == 1);
        }
        [Fact]
        public void ThrowsInvalidIDException()
        {
            var request = new UpdateEmployee() { EmployeeId = 2,FirstName="Anu",Lastname="John",Designation="Developer",DOB=Convert.ToDateTime("2000/10/26"),Gender='F' };
            var response = Record.ExceptionAsync(async () => await handler.Handle(request, CancellationToken.None));
            Assert.IsType<ExceptionModel.InvalidIDException>(response.Result);
        }
    }
}
