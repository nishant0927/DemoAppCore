using ASPCoreWebApp.Models;

namespace ASPCoreWebApp.Services
{
    public interface IEmployeeService
    {
        Task<bool> SaveEmployeeData(EmployeeViewModel employee);
        Task<List<EmployeeViewModel>> GetAllEmployeeData();
        Task<EmployeeViewModel> GetEmployeeAsyc(Guid id);
        Task<EmployeeViewModel> GetEmployeeAdnFileFromDBAsyc(Guid id);
    }
}
