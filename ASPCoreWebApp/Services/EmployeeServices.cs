using ASPCoreWebApp.clsCommon;
using ASPCoreWebApp.DB;
using ASPCoreWebApp.DB.Table;
using ASPCoreWebApp.Models;
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
        public async Task<bool> SaveFileData(List<FileViewModel> lstFileViewModal,Guid EmpGuid)
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
