using System;
using System.Collections.Generic;

namespace ASPCoreWebApp.DB.Table;

public partial class TblDesignationMaster
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public virtual ICollection<TableEmployee> TableEmployees { get; set; } = new List<TableEmployee>();
}
