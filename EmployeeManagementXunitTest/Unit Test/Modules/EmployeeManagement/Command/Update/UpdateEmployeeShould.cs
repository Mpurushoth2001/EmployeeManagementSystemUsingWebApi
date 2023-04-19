using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Modules.EmployeeManagement.Command.Update.UpdateEmployee;

namespace EmployeeManagementXunitTest.Unit_Test.Modules.EmployeeManagement.Command.Update
{
    public class UpdateEmployeeShould
    {
        DbContextMock<EmployeeDbcontext> dbContextMock;
        UpdateEmployeeHandler handler;
        DbContextOptions<EmployeeDbcontext> dbContextOptions {  get;}=new DbContextOptionsBuilder<EmployeeDbcontext>().Options;
        public UpdateEmployeeShould()
        {
            dbContextMock = new DbContextMock<EmployeeDbcontext>(dbContextOptions);
            handler=new UpdateEmployeeHandler(this.dbContextMock.Object);
        }
        [Fact]
        public void PassVerification()
        {
            var request = new UpdateEmployee() { EmployeeId = 2,FirstName="Anu",Lastname="John",Designation="Developer",DOB=Convert.ToDateTime("2000/10/26"),Gender='F' };
            //var responce=handler handle(request,CancellationToken.None);

        }
    }
}
