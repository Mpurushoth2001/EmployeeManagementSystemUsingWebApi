using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Model.EmployeeModel
{
    public class EmployeeDbcontext : DbContext
    {
        public EmployeeDbcontext(DbContextOptions options) : base(options) { }
        public virtual DbSet<EmployeeModel> Employees { get; set; }
    }
}
