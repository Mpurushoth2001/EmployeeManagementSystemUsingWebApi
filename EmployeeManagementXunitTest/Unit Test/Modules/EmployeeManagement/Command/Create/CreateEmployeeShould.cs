using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Create;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
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
            dbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => { return 1; })).Verifiable();
            var response= handler.Handle(request , CancellationToken.None);
            Assert.True(response.Result.ResponseId ==1);
                    }
        [Fact]
        public void FailsOnInvalidDetails()
        {
            var request = new CreateEmployee() { FirstName = "Vis3hnu", Lastname = "Palani", Designation = "Developer", DOB = Convert.ToDateTime("1938/10/23"), Gender = 'M' };
            dbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(() => Task.Run(() => { return 0; })).Verifiable();
            var response = handler.Handle(request, CancellationToken.None);
            Assert.True(response.Result.ResponseId == 0);
        }
    }
}
