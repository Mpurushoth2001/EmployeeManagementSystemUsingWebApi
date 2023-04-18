
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model.EmployeeModel
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        public char Gender { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }
        public string Designation { get; set; }

    }
}
