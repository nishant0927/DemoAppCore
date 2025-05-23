using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using ASPCoreWebApp.Services;

namespace ASPCoreWebApp.clsCommon
{
    public class CommonHelper
    {       
        public static async Task<List<DepartmentViewModel>> GetDepartment(ICommonService<TblDepartment> _departmentService)
        {
            try
            {                          
                List<DepartmentViewModel> lstDepartment = (await _departmentService.getAllData()).Select
                                                           (dep => new DepartmentViewModel
                                                           {
                                                               DepartmentId = dep.DepartmentId,
                                                               DepartmentName = dep.DepartmentName
                                                           }).ToList();
                lstDepartment.Insert(0, new DepartmentViewModel { DepartmentId = 0, DepartmentName = "Select" });
                return  lstDepartment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<DesignationViewModel>> GetDesignation(ICommonService<TblDesignationMaster> _designationService)
        {
            try
            {               
                List<DesignationViewModel> lstDesignationViewModel = (await _designationService.getAllData()).Select(design =>
                                                                      new DesignationViewModel
                                                                      {
                                                                          DesignationId = design.DesignationId,
                                                                          DesignationName = design.DesignationName
                                                                      }).ToList();
                lstDesignationViewModel.Insert(0, new DesignationViewModel { DesignationId = 0, DesignationName = "Select" });
                return lstDesignationViewModel;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
