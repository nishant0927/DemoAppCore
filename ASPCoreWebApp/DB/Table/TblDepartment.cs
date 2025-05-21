using System;
using System.Collections.Generic;

namespace ASPCoreWebApp.DB.Table;

public partial class TblDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<TableEmployee> TableEmployees { get; set; } = new List<TableEmployee>();
}
