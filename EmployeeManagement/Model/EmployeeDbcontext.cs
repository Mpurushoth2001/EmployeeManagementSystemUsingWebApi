using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Model
{
    public class EmployeeDbcontext : DbContext
    {
        public EmployeeDbcontext(DbContextOptions options):base(options) { }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
