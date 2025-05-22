using System.ComponentModel.DataAnnotations;

namespace ASPCoreWebApp.Models
{
    public class EmployeeViewModel
    {
        public Guid EmpGuid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="Please enter Employee Name")]
        public string EmpName { get; set; } 

        public int EmpDesignation { get; set; }

        public int EmpDepartment { get; set; }

        public List<FileViewModel> lstFiles { get; set; } = new();
    }
}
