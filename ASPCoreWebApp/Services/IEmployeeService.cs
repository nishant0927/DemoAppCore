using ASPCoreWebApp.Models;

namespace ASPCoreWebApp.Services
{
    public interface IEmployeeService
    {
        Task<bool> SaveEmployeeData(EmployeeViewModel employee);
    }
}
