using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using static EmployeeManagement.Modules.EmployeeManagement.Command.Create.CreateEmployee;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Create
{
    public class CreateEmployeeShould
    {
        DbContextMock<EmployeeDbcontext> dbContextMock;
        CreateEmployeeHandler handler;
        List<EmployeeModel> EmployeeModels = new List<EmployeeModel>();
        DbContextOptions<EmployeeDbcontext> dbContextOptions { get; }= new DbContextOptionsBuilder<EmployeeDbcontext>().Options;   
        
        public CreateEmployeeShould()
        {
            dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptions);
            handler = new CreateEmployeeHandler(this.dbContextMock.Object);
            dbContextMock.CreateDbSetMock(x => x.Employees, EmployeeModels);
        }
        [Fact]
        public void PassValidation()
        {
            var request = new CreateEmployee() { FirstName="Vishnu",Lastname="Palani",Designation="Developer",DOB=Convert.ToDateTime("2001/10/23"),Gender='M'};
            var response= handler.Handle(request , CancellationToken.None);
            dbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run (() => { return 1; })).Verifiable();
            Assert.True(response.Result.ResponseId !=null);
            //Assert.NotNull(response.Result.ResponseId);
        }
        [Fact]
        public void Excption()
        {
            var request = new CreateEmployee() { };
            var response = Record.ExceptionAsync(async () => await handler.Handle(request, CancellationToken.None));
            Assert.IsType<NullReferenceException>(response.Result);
        }

    }
}
