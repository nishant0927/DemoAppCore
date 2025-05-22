using ASPCoreWebApp.clsCommon;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using ASPCoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICommonService<TblDepartment> _departmentService;
        private readonly ICommonService<TblDesignationMaster> _designation;
        private readonly IEmployeeService _iEmployee;
        public EmployeeController(ICommonService<TblDepartment> department, ICommonService<TblDesignationMaster> designation, IEmployeeService employeeService)
        {
            _departmentService = department;
            _designation = designation;
            _iEmployee = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task< IActionResult> CreateEmployee()
        {
           
            ViewBag.Department = await CommonHelper.GetDepartment(_departmentService);
            ViewBag.Designation= await CommonHelper.GetDesignation(_designation);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iEmployee.SaveEmployeeData(employee);
                }
                return View();
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });
            }
        }
    }
}
