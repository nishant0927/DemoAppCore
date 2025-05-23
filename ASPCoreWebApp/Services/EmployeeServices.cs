using ASPCoreWebApp.clsCommon;
using ASPCoreWebApp.DB;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace ASPCoreWebApp.Services
{
    public class EmployeeServices : IEmployeeService
    {
        private readonly DBContex _context;
        private readonly ICommonService<TableEmployee> _TblEmployee;
        private readonly ICommonService<TblFile> _TblFile;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeServices(DBContex context, ICommonService<TableEmployee> tableEmployee, ICommonService<TblFile> tblFile, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _TblEmployee = tableEmployee;
            _TblFile = tblFile;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<EmployeeViewModel>> GetAllEmployeeData()
        {
            return await (from emp in _context.TableEmployees
                          join dep in _context.TblDepartments on emp.EmpDepartment equals dep.DepartmentId into deptJoin
                          from dep in deptJoin.DefaultIfEmpty()
                          join des in _context.TblDesignationMasters on emp.EmpDesignation equals des.DesignationId into desJoin
                          from des in desJoin.DefaultIfEmpty()
                          select new EmployeeViewModel
                          {
                              EmpGuid = emp.EmpGuid,
                              EmpName = emp.EmpName,
                              EmpDepartmentName = dep != null ? dep.DepartmentName : "",
                              EmpDesignationName =des !=null? des.DesignationName:""
                          }).ToListAsync();
        }

        public async Task<EmployeeViewModel> GetEmployeeAsyc(Guid id)
        {
            try
            {
                var employeeDatat = await _TblEmployee.GetFilterDataAsync(x => x.EmpGuid.Equals(id));
                var files = await _TblFile.GetAllFilterDataAsync(x => x.EmpGuid.Equals(id));
                if (employeeDatat == null)
                    return null;
                EmployeeViewModel employeeViewModel = new EmployeeViewModel
                {
                    EmpGuid = employeeDatat.EmpGuid,
                    EmpName = employeeDatat.EmpName,
                    EmpDepartment = employeeDatat.EmpDepartment,
                    EmpDesignation = employeeDatat.EmpDesignation,
                    lstFiles = files.Select(f => new FileViewModel
                    {
                        EmpGuid = f.EmpGuid,
                        FileGuid = f.FileGuid,
                        FileName = f.FileName,
                        Description = f.Description,
                        FilePath = f.FilePath

                    }).ToList()
                };
                return employeeViewModel;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> SaveEmployeeData(EmployeeViewModel employee)
        {
            using var transation=_context.Database.BeginTransaction();
            try
            {
                TableEmployee tableEmployee = new TableEmployee
                {
                    EmpGuid = employee.EmpGuid,
                    EmpDepartment = employee.EmpDepartment,
                    EmpName = employee.EmpName,
                    EmpDesignation= employee.EmpDesignation
                };
                tableEmployee=await _TblEmployee.Save(tableEmployee);
               await SaveFileData(employee.lstFiles, tableEmployee.EmpGuid);
                transation.Commit();
                return true;

            }
            catch(Exception ex)
            {
               await transation.RollbackAsync();
                throw new Exception(ex.Message);

            }

        }
        private async Task<bool> SaveFileData(List<FileViewModel> lstFileViewModal,Guid EmpGuid)
        {
            try
            {
                foreach (var fileVm in lstFileViewModal)
                {
                    if (fileVm.File != null && fileVm.File.Length>0)
                    {
                        string saveFile =await FileHelper.SaveFileAsync(fileVm.File, _hostingEnvironment.WebRootPath, "UploadedFiles", EmpGuid.ToString(), fileVm.FileGuid.ToString());
                        if (saveFile != null)
                        {
                            TblFile tblFile = new TblFile
                            {
                                EmpGuid=EmpGuid,
                                FileGuid=fileVm.FileGuid,
                                FileName=fileVm.FileName,
                                FilePath=saveFile,
                                Description=fileVm.Description,
                            };
                           await _TblFile.Save(tblFile);
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
