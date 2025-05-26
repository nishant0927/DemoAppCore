using System;
using System.Collections.Generic;

namespace ASPCoreWebApp.DB.Table;

public partial class TableEmployee
{
    public int Id { get; set; }

    public Guid EmpGuid { get; set; }

    public string EmpName { get; set; } = null!;

    public int EmpDesignation { get; set; }

    public int EmpDepartment { get; set; }

    public virtual TblDepartment EmpDepartmentNavigation { get; set; } = null!;

    public virtual TblDesignationMaster EmpDesignationNavigation { get; set; } = null!;

    public virtual ICollection<TblFile> TblFiles { get; set; } = new List<TblFile>();
    public virtual ICollection<TblFileForDatatBase> TblFileForDatatBase { get; set; } = new List<TblFileForDatatBase>();
}
