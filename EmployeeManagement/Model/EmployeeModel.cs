
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EmployeeManagement.Model
{
    public class EmployeeModel
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        
        [Display(Name ="Last Name")]
        public string? Lastname { get; set; }
        public char Sex { get; set;}
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        public string Designation { get; set; }
    }
}
