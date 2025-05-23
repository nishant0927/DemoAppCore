using ASPCoreWebApp.clsCommon;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using ASPCoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            List<EmployeeViewModel> lst=await _iEmployee.GetAllEmployeeData();
            return View(lst);
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

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                EmployeeViewModel emp =await _iEmployee.GetEmployeeAsyc(Guid.Parse( id));
                if (emp == null)
                    return NotFound();

                ViewBag.Department = await CommonHelper.GetDepartment(_departmentService);
                ViewBag.Designation = await CommonHelper.GetDesignation(_designation);
                return View(emp);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
