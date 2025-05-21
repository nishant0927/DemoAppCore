namespace ASPCoreWebApp.Models
{
    public class EmployeeViewModel
    {
        public Guid EmpGuid { get; set; }

        public string EmpName { get; set; } = null!;

        public int EmpDesignation { get; set; }

        public int EmpDepartment { get; set; }
    }
}
